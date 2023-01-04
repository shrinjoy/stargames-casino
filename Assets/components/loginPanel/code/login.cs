using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField username;
    [SerializeField]TMPro.TMP_InputField  password;
    [SerializeField] TMPro.TMP_Text warningtext;
    SQL_manager sqlm;
    private void Start()
    {
        sqlm = GameObject.FindWithTag("SQLmanager").GetComponent<SQL_manager>();
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
    }
}
