using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Watchtower : MonoBehaviour
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
    private ResourceCountUpdater resourceCountUpdater;
    void Start()
    {
        resourceCountUpdater = FindObjectOfType<ResourceCountUpdater>();
        UpgradeButton.onClick.AddListener(Upgrade);
        BackButton.onClick.AddListener(BackRequest);
        SetLevels();
    }

    IEnumerator UpgradeRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", GameManager.Instance.Token);
        form.AddField("Level", GameManager.Instance.WatchtowerLevel + 1);

        if (GameManager.Instance.WoodCount >= int.Parse(RequiredWood.text) && GameManager.Instance.StoneCount >= int.Parse(RequiredStone.text) && GameManager.Instance.GoldCount >= int.Parse(RequiredGold.text))
        {
            using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Villages/Watchtower", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    StartCoroutine(UpdateResources());
                }
            }
        }
    }
    IEnumerator UpdateResources()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("woodUsed", int.Parse(RequiredWood.text));
        form2.AddField("stoneUsed", int.Parse(RequiredStone.text));
        form2.AddField("goldUsed", int.Parse(RequiredGold.text));
        form2.AddField("userToken", GameManager.Instance.Token);
        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/api/UserResources/use", form2))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                resourceCountUpdater.UpdateCounts(www.downloadHandler.text);
                GameManager.Instance.WatchtowerLevel += 1;
                GameManager.Instance.PlaySound(5);

                SetLevels();
            }
        }
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
        CurrentLevel.text = GameManager.Instance.WatchtowerLevel.ToString();
        CurrentProduction.text = "+" + (GameManager.Instance.WatchtowerLevel*10).ToString() + "%";
        NextLevel.text = (GameManager.Instance.WatchtowerLevel + 1).ToString();
        NextProduction.text = "+" + ((GameManager.Instance.WatchtowerLevel + 1) * 10).ToString() + "%";
        RequiredWood.text = (GameManager.Instance.WatchtowerLevel * 10).ToString();
        RequiredStone.text = (GameManager.Instance.WatchtowerLevel * 5).ToString();
        RequiredGold.text = Math.Ceiling(GameManager.Instance.WatchtowerLevel * 2.5).ToString();
    }

}
