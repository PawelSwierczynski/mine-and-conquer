using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Location
{
    public class ResourceLocationProvider : MonoBehaviour
    {
        private const string RESOURCE_LOCATIONS_URI = "https://rest-api-nodejs-mysql-server.herokuapp.com/api/ResourceLocations";

        private ResourceLocationHolder resourceLocationHolder;

        void Start()
        {
            resourceLocationHolder = new ResourceLocationHolder();

            RequestResourceLocations();
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
            }
        }

        public void RequestResourceLocations()
        {
            StartCoroutine(RetrieveResourceLocations());
        }
    }
}