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
    public Button MapButton;
    public Button LumberjackButton;    
    public Button StonequarryButton;
    public Button GoldmineButton;
    public Button BarracksButton;
    public Button WallButton;
    public Button WatchtowerButton;
    public Text LumberjackLevel;
    public Text StonequarryLevel;
    public Text GoldmineLevel;
    public Text BarracksLevel;
    public Text WallLevel;
    public Text WatchtowerLevel;


    void Start()
    {
        MapButton.onClick.AddListener(OpenMap);
        LumberjackButton.onClick.AddListener(Lumberjack);
        StonequarryButton.onClick.AddListener(Stonequarry);
        GoldmineButton.onClick.AddListener(Goldmine);
        VillageRequest();
        
    }
    

    IEnumerator VillageRequestSend()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", GameManager.Instance.Token);
        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Villages", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {            
                Village village = JsonUtility.FromJson<Village>(www.downloadHandler.text.Trim(new char[] {'[',']' }));
                LumberjackLevel.text = village.Lumberjack.ToString();
                StonequarryLevel.text = village.Stonequarry.ToString();
                GoldmineLevel.text = village.Goldmine.ToString();
                BarracksLevel.text = village.Barracks.ToString();
                WallLevel.text = village.Wall.ToString();
                WatchtowerLevel.text = village.Watchtower.ToString();
                GameManager.Instance.LumberjackLevel = village.Lumberjack;
                GameManager.Instance.StonequarryLevel = village.Stonequarry;
                GameManager.Instance.GoldmineLevel = village.Goldmine;
                GameManager.Instance.BarracksLevel = village.Barracks;
                GameManager.Instance.WallLevel = village.Wall;
                GameManager.Instance.WatchtowerLevel = village.Watchtower;

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
    public void OpenMap()
    {
        SceneManager.LoadScene("Game");
    }
    public void Lumberjack()
    {
        SceneManager.LoadScene("Lumberjack");
    }
    public void Stonequarry()
    {
        SceneManager.LoadScene("Stonequarry");
    }
    public void Goldmine()
    {
        SceneManager.LoadScene("Goldmine");
    }
    public void Barracks()
    {
        SceneManager.LoadScene("Barracks");
    }
    public void Wall()
    {
        SceneManager.LoadScene("Wall");
    }
    public void Watchtower()
    {
        SceneManager.LoadScene("Watchtower");
    }
}
