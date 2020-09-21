using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResourceCounts
{
    public int WoodCount;
    public int StoneCount;
    public int GoldCount;
}

public class ResourceCountUpdater : MonoBehaviour
{
    public Text woodCount;
    public Text stoneCount;
    public Text goldCount;

    void Start()
    {
        StartCoroutine(RetrieveResourcesCounts());
    }

    private IEnumerator RetrieveResourcesCounts()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get("https://rest-api-nodejs-mysql-server.herokuapp.com/api/UserResources?userToken=" + GameManager.Instance.Token.ToString()))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.Log(unityWebRequest.error);
            }
            else
            {
                UpdateCounts(unityWebRequest.downloadHandler.text);
            }
        }
    }

    public void UpdateCounts(string resourceCountsJson)
    {
        ResourceCounts resourceCounts = JsonUtility.FromJson<ResourceCounts>(resourceCountsJson.Substring(1, resourceCountsJson.Length - 2));

        woodCount.text = resourceCounts.WoodCount.ToString();
        stoneCount.text = resourceCounts.StoneCount.ToString();
        goldCount.text = resourceCounts.GoldCount.ToString();
    }
}