using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

[Serializable]
public class Village
{
    public int Lumberjack;
    public int Stonequarry;
    public int Goldmine;
    public int Barracks;
    public int Watchtower;
    public int Wall;
}
public class Buildings : MonoBehaviour
{
    public Button LumberjackButton;
    public Button StonequarryButton;
    public Button GoldmineButton;
    public Text LumberjackLevel;
    public Text StonequarryLevel;
    public Text GoldmineLevel;


    void Start()
    {
        LumberjackButton.onClick.AddListener(Lumberjack);
        StonequarryButton.onClick.AddListener(Stonequarry);
        GoldmineButton.onClick.AddListener(Goldmine);
        VillageRequest();
        
    }
    

    IEnumerator VillageRequestSend()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", 17);
        //GameManager.Instance.Token
        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Villages", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {            
                Debug.Log(www.downloadHandler.text);
                Village village = JsonUtility.FromJson<Village>(www.downloadHandler.text);

                Debug.Log(village.Lumberjack);
                LumberjackLevel.text = village.Lumberjack.ToString();
                StonequarryLevel.text = village.Stonequarry.ToString(); ;
                GoldmineLevel.text = village.Goldmine.ToString(); ;
}
        }
    }

    public void VillageRequest()
    {
        StartCoroutine(VillageRequestSend());
    }

    void Update()
    {
        
    }
    public void Lumberjack()
    {
        SceneManager.LoadScene("Lumberjack");
    }
    public void Stonequarry()
    {
        //SceneManager.LoadScene("Stonequarry");
    }
    public void Goldmine()
    {
        //SceneManager.LoadScene("Goldmine");
    }
}
