using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;

    void Start()
    {
        LoginButton.onClick.AddListener(LoginRequest);
    }
    public class User
    {
        public string Login;
        public int Token;
    }
    IEnumerator LoginRequestSend()
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", UsernameInput.text);
        form.AddField("Password", PasswordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Users/Login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                User user = JsonUtility.FromJson<User>(www.downloadHandler.text);
                GameManager.Instance.Token = user.Token;
                if (user.Login == "successful")
                {
                    SceneManager.LoadScene("Game");
                }
                
            }
        }
    }
    
    public void LoginRequest()
    {
        StartCoroutine(LoginRequestSend());
    }
}