
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VillageButton : MonoBehaviour
{
    public Button Village;
    // Start is called before the first frame update
    void Start()
    {
        Village.onClick.AddListener(OpenVillage);
    }
    public void OpenVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
