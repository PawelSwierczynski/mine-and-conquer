using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Goldmine : MonoBehaviour
{
    public Button UpgradeButton;
    public Button BackButton;
    public Text CurrentLevel;
    public Text CurrentProduction;
    public Text NextLevel;
    public Text NextProduction;
    public Text RequiredWood;
    public Text RequiredStone;
    public Text RequiredGold;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeButton.onClick.AddListener(Upgrade);
        BackButton.onClick.AddListener(BackRequest);
        SetLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpgradeRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", GameManager.Instance.Token);
        form.AddField("Level", GameManager.Instance.GoldmineLevel + 1);
        

        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Villages/Goldmine", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("ok");
            }
        }
        GameManager.Instance.GoldmineLevel += 1;
        SetLevels();
    }
    public void BackRequest()
    {
        SceneManager.LoadScene("Village");
    }
    public void Upgrade()
    {
        StartCoroutine(UpgradeRequest());
    }
    public void SetLevels()
    {
        CurrentLevel.text = GameManager.Instance.GoldmineLevel.ToString();
        CurrentProduction.text = "+" + (GameManager.Instance.GoldmineLevel*10).ToString() + "%";
        NextLevel.text = (GameManager.Instance.GoldmineLevel + 1).ToString();
        NextProduction.text = "+" + ((GameManager.Instance.GoldmineLevel + 1) * 10).ToString() + "%";
        RequiredWood.text = (GameManager.Instance.GoldmineLevel * 10).ToString();
        RequiredStone.text = (GameManager.Instance.GoldmineLevel * 5).ToString();
        RequiredGold.text = Math.Ceiling(GameManager.Instance.GoldmineLevel * 2.5).ToString();
    }

}
