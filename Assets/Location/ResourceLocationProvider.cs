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
        private MapCalculator mapCalculator;
        private float currentPlayerLongitude;
        private float currentPlayerLatitude;

        void Start()
        {
            resourceLocationHolder = new ResourceLocationHolder();
            mapCalculator = new MapCalculator();
        }

        private IEnumerator RetrieveResourceLocations(float playerLatitude, float playerLongitude)
        {
            UnityWebRequest resourceLocationsRequest = UnityWebRequest.Get(RESOURCE_LOCATIONS_URI + "?latitude=" + mapCalculator.RetrieveLatitudeZoneIdentifier(playerLatitude) + "&longitude=" + mapCalculator.RetrieveLongitudeZoneIdentifier(playerLongitude));
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

            StartCoroutine(RetrieveResourceLocations(playerLatitude, playerLongitude));
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
                resourceSpawnParameters.Add(new ResourceSpawnParameters(resourceInLineOfSight.Identifier, resourceInLineOfSight.TypeIdentifier, mapBounds.CalculateResourcePosition(resourceInLineOfSight)));
            }

            resourceSpawner.SpawnResources(resourceSpawnParameters);
        }
    }
}