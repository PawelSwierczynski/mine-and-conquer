using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public GameObject UsernameInput;
    public GameObject PasswordInput;
    public GameObject LoginButton;
    private string Username;
    private string Password;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.UI.Button button = LoginButton.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(LoginRequest);
    }
    // Update is called once per frame
    void Update()
    {
        Username = UsernameInput.GetComponent<UnityEngine.UI.InputField>().text;
        Password = PasswordInput.GetComponent<UnityEngine.UI.InputField>().text;
    }
    IEnumerator LoginRequestSend()
    {
        WWWForm form = new WWWForm();
        form.AddField(Username, Password);
        
        string requeststring = "{\"Login\":" + "\"" + Username + "\"" + ",\"Password\":" + "\"" + Password + "\"}";
        Debug.Log(requeststring);
        using (UnityWebRequest www = UnityWebRequest.Post("https://rest-api-nodejs-mysql-server.herokuapp.com/Users/Login", requeststring))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    
    public void LoginRequest()
    {
        StartCoroutine(LoginRequestSend());
    }
}
