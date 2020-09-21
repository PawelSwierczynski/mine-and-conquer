using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.UI
{
    public class ResourceClickHandler : MonoBehaviour
    {
        public int ResourceType;

        private IEnumerator ClaimResource()
        {
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("resourceIdentifier", int.Parse(transform.name));
            wwwForm.AddField("userToken", GameManager.Instance.Token);

            using (UnityWebRequest unityWebRequest = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/api/ResourceLocations/claim", wwwForm))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                {
                    Debug.Log(unityWebRequest.error);
                }
                else
                {
                    Debug.Log(unityWebRequest.downloadHandler.text);

                    Destroy(transform.gameObject);
                }
            }
        }

        public void ProcessClick()
        {
            StartCoroutine(ClaimResource());
        }
    }
}