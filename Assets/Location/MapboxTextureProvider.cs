using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Location
{
    public class MapboxTextureProvider : MonoBehaviour
    {
        private const string API_URL_BEGINNING = "https://api.mapbox.com/styles/v1/kagalorn/ckc2bflbx6sju1is2s6m3t768/static/";
        private const string API_URL_END = ",17,0/450x800?access_token=pk.eyJ1Ijoia2FnYWxvcm4iLCJhIjoiY2tjMmIyMmlkMXR3MDJybGpqNmZkZTFieSJ9.kDc986jc3zuBGs2iclzcPA";

        public RawImage gpsMapRawImage;
        public GPS gps;

        void Update()
        {
            if (gps.IsLocationChanged())
            {
                StartCoroutine(RetrieveTextureFromAPI(gps.Longitude, gps.Latitude));
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
            Debug.Log(API_URL_BEGINNING + longitude.ToString() + "," + latitude.ToString() + API_URL_END);

            return API_URL_BEGINNING + longitude.ToString() + "," + latitude.ToString() + API_URL_END;
        }
    }
}