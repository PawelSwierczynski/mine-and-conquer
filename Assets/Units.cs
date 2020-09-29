using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


[Serializable]
public class Unit
{
    public int Swordsman;
}
public class Units : MonoBehaviour
{
    public Button RecruitButton;
    public Button BackButton;
    public Text Swordsmans;
    public Text RequiredWood;
    public Text RequiredStone;
    public Text RequiredGold;
    private ResourceCountUpdater resourceCountUpdater;
    void Start()
    {
        resourceCountUpdater = FindObjectOfType<ResourceCountUpdater>();
        RecruitButton.onClick.AddListener(Recruit);
        BackButton.onClick.AddListener(BackRequest);
        StartCoroutine(UnitsRequest());
    }

    IEnumerator RecruitRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", GameManager.Instance.Token);
        form.AddField("Level", GameManager.Instance.Swordsmans + 1);


        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Units/Swordsman", form))
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
                GameManager.Instance.Swordsmans += 1;
                SetLevels();
            }
        }
    }
    public void BackRequest()
    {
        SceneManager.LoadScene("Barracks");
    }

    public void Recruit()
    {
        StartCoroutine(RecruitRequest());
    }
    public void SetLevels()
    {        
        Swordsmans.text = GameManager.Instance.Swordsmans.ToString();
    }
    IEnumerator UnitsRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", GameManager.Instance.Token);
        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Units", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Unit unit = JsonUtility.FromJson<Unit>(www.downloadHandler.text.Trim(new char[] { '[', ']' }));
                Swordsmans.text = unit.Swordsman.ToString();
                GameManager.Instance.Swordsmans = unit.Swordsman;

            }
        }
    }
}

