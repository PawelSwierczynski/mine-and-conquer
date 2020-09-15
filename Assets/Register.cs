using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public InputField EmailInput;
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button RegisterButton;
    public Button BackButton;

    void Start()
    {
        RegisterButton.onClick.AddListener(RegisterRequest);
        BackButton.onClick.AddListener(BackRequest);
    }
    IEnumerator RegisterRequestSend()
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", EmailInput.text);
        form.AddField("Login", UsernameInput.text);
        form.AddField("Password", PasswordInput.text);
        form.AddField("LastLogged", System.DateTime.Now.ToString());
        form.AddField("Token", 0);

        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Users", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                SceneManager.LoadScene("Login");                
            }
        }
    }
    
    public void RegisterRequest()
    {
        StartCoroutine(RegisterRequestSend());
    }
    public void BackRequest()
    {
        SceneManager.LoadScene("Login");
    }
}