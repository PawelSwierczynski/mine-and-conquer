using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Location
{
    public class MapProvider : MonoBehaviour
    {
        private const string API_URL_BEGINNING = "https://api.mapbox.com/styles/v1/kagalorn/ckc2bflbx6sju1is2s6m3t768/static/";
        private const string API_URL_ROTATION = ",0/";
        private const string API_URL_END = "?access_token=pk.eyJ1Ijoia2FnYWxvcm4iLCJhIjoiY2tjMmIyMmlkMXR3MDJybGpqNmZkZTFieSJ9.kDc986jc3zuBGs2iclzcPA";

        public RawImage gpsMapRawImage;
        public GPS gps;
        public ResourceLocationProvider resourceLocationProvider;

        private MapCalculator mapCalculator;
        private CultureInfo apiCulture;

        void Start()
        {
            mapCalculator = new MapCalculator();
            apiCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        void Update()
        {
            if (gps.IsLocationChanged())
            {
                StartCoroutine(RetrieveTextureFromAPI(gps.Longitude, gps.Latitude));
                resourceLocationProvider.RequestResourceLocations(gps.Longitude, gps.Latitude);
            }
        }

        private IEnumerator RetrieveTextureFromAPI(float longitude, float latitude)
        {
            UnityWebRequest mapTextureRequest = UnityWebRequestTexture.GetTexture(GenerateAPIRequestAddress(longitude, latitude));
            yield return mapTextureRequest.SendWebRequest();

            if (mapTextureRequest.isNetworkError || mapTextureRequest.isHttpError)
            {
                Debug.LogError(mapTextureRequest.error);
            }
            else
            {
                gpsMapRawImage.texture = ((DownloadHandlerTexture)mapTextureRequest.downloadHandler).texture;
            }
        }

        private string GenerateAPIRequestAddress(float longitude, float latitude)
        {
            return API_URL_BEGINNING + longitude.ToString(apiCulture) + "," + latitude.ToString(apiCulture) + "," + mapCalculator.CalculateMapZoom(latitude).ToString(apiCulture) + API_URL_ROTATION + mapCalculator.RetrieveMapResolution() + API_URL_END;
        }
    }
}