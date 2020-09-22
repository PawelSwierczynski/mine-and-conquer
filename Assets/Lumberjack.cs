using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lumberjack : MonoBehaviour
{
    public Button UpgradeButton;
    public Button BackButton;
    public Text CurrentLevel;
    public Text CurrentProduction;
    public Text NextLevel;
    public Text NextProduction;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeButton.onClick.AddListener(UpgradeRequest);
        BackButton.onClick.AddListener(BackRequest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeRequest()
    {
        CurrentLevel.text = NextLevel.text;
        CurrentProduction.text = NextProduction.text;
        NextLevel.text = (int.Parse(NextLevel.text) + 1).ToString();
        NextProduction.text = "+" + (int.Parse(NextProduction.text) + 2).ToString();
    }
    public void BackRequest()
    {
        SceneManager.LoadScene("Village");
    }
}
