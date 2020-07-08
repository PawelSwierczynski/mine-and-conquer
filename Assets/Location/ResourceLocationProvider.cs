using Assets.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Location
{
    public class ResourceLocationProvider : MonoBehaviour
    {
        private const string RESOURCE_LOCATIONS_URI = "https://rest-api-nodejs-mysql-server.herokuapp.com/api/ResourceLocations";

        public ResourceSpawner resourceSpawner;

        private ResourceLocationHolder resourceLocationHolder;
        private float currentPlayerLongitude;
        private float currentPlayerLatitude;

        void Start()
        {
            resourceLocationHolder = new ResourceLocationHolder();
        }

        private IEnumerator RetrieveResourceLocations()
        {
            UnityWebRequest resourceLocationsRequest = UnityWebRequest.Get(RESOURCE_LOCATIONS_URI);
            yield return resourceLocationsRequest.SendWebRequest();

            if (resourceLocationsRequest.isNetworkError || resourceLocationsRequest.isHttpError)
            {
                Debug.LogError(resourceLocationsRequest.error);
            }
            else
            {
                resourceLocationHolder = JsonUtility.FromJson<ResourceLocationHolder>("{ \"ResourceLocations\": " + resourceLocationsRequest.downloadHandler.text + "}");

                SpawnResourcesOnMap(currentPlayerLongitude, currentPlayerLatitude);
            }
        }

        public void RequestResourceLocations(float playerLongitude, float playerLatitude)
        {
            currentPlayerLongitude = playerLongitude;
            currentPlayerLatitude = playerLatitude;

            StartCoroutine(RetrieveResourceLocations());
        }

        private IEnumerable<ResourceLocation> RetrieveResourcesInLineOfSight(MapBounds mapBounds)
        {
            IList<ResourceLocation> resourcesInLineOfSight = new List<ResourceLocation>();

            foreach (var resource in resourceLocationHolder.ResourceLocations)
            {
                if (mapBounds.IsInBounds(resource))
                {
                    resourcesInLineOfSight.Add(resource);
                }
            }

            return resourcesInLineOfSight;
        }

        public void SpawnResourcesOnMap(float playerLongitude, float playerLatitude)
        {
            MapBounds mapBounds = new MapBounds(playerLongitude, playerLatitude);
            IEnumerable<ResourceLocation> resourcesInLineOfSight = RetrieveResourcesInLineOfSight(mapBounds);

            IList<ResourceSpawnParameters> resourceSpawnParameters = new List<ResourceSpawnParameters>();

            foreach (var resourceInLineOfSight in resourcesInLineOfSight)
            {
                resourceSpawnParameters.Add(new ResourceSpawnParameters(resourceInLineOfSight.Identifier, 0, mapBounds.CalculateResourcePosition(resourceInLineOfSight)));
            }

            resourceSpawner.SpawnResources(resourceSpawnParameters);
        }
    }
}