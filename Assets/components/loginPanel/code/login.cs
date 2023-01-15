
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField username;
    [SerializeField]TMPro.TMP_InputField  password;
    [SerializeField] TMPro.TMP_Text warningtext;
    SQL_manager sqlm;
    [SerializeField] Toggle rememberme;
    private void Start()
    {
        sqlm = GameObject.FindWithTag("SQLmanager").GetComponent<SQL_manager>();
        if(PlayerPrefs.GetString("storedata") =="yes")
        {
            rememberme.isOn = true;
        }
        if (PlayerPrefs.GetString("storedata") == "no")
        {
            rememberme.isOn = false;
        }

        if (rememberme.isOn==true)
        {
            if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
            {
                username.text = PlayerPrefs.GetString("username");
                password.text = PlayerPrefs.GetString("password");
            }
        }
    }                                                                              
    public void loginuser()
    {
        if(sqlm.canLogin(username.text.ToString(),password.text.ToString()))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            warningtext.text = "invalid username or password";
        }
        if(rememberme.isOn) {
            PlayerPrefs.SetString("storedata", "yes");
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetString("password", password.text);
        }
       

    }
    private void Update()
    {
        if (rememberme.isOn)
        {
            PlayerPrefs.SetString("storedata", "yes");
        }
        if (rememberme.isOn != true)
        {
            PlayerPrefs.SetString("storedata", "no");
        }
    }
}
