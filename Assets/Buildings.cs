using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buildings : MonoBehaviour
{
    public Button LumberjackButton;
    public Button StonequarryButton;
    public Button GoldmineButton;

    void Start()
    {
        LumberjackButton.onClick.AddListener(Lumberjack);
        StonequarryButton.onClick.AddListener(Stonequarry);
        GoldmineButton.onClick.AddListener(Goldmine);
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
