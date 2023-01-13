using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class details_setter : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text ks;
    [SerializeField] TMPro.TMP_Text kh;
    [SerializeField] TMPro.TMP_Text kd;
    [SerializeField] TMPro.TMP_Text kc;
    [SerializeField] TMPro.TMP_Text qs;
    [SerializeField] TMPro.TMP_Text qh;
    [SerializeField] TMPro.TMP_Text qd;
    [SerializeField] TMPro.TMP_Text qc;
    [SerializeField] TMPro.TMP_Text js;
    [SerializeField] TMPro.TMP_Text jh;
    [SerializeField] TMPro.TMP_Text jd;
    [SerializeField] TMPro.TMP_Text jc;

    private void OnEnable()
    {
        if (GameObject.FindObjectOfType<claimmanager>().gameid != null)
        {


            SqlCommand sqlCmnd = new SqlCommand();
            SqlDataReader sqlData = null;
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = "SELECT * FROM [star].[dbo].[tasp] where ter_id="+GameObject.FindObjectOfType<userManager>().getUserData().id+" and g_id="+GameObject.FindObjectOfType<claimmanager>().gameid;//this is the sql command we use to get data about user
            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            if(sqlData.Read())
            {
                setresult(sqlData["a00"].ToString(), sqlData["a01"].ToString(), sqlData["a02"].ToString(), sqlData["a03"].ToString(), sqlData["a04"].ToString(), sqlData["a05"].ToString(), sqlData["a06"].ToString(), sqlData["a07"].ToString(), sqlData["a08"].ToString(), sqlData["a09"].ToString(), sqlData["a10"].ToString(), sqlData["a11"].ToString());
            }
            sqlData.Close();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void closethispanel()
    {
        this.gameObject.SetActive(false);
    }
    public void setresult(string a00,string a01,string a02,string a03,string a04,string a05,string a06,string a07,string a08,string a09,string a10,string a11)
    {
        ks.text = a00;
        kh.text = a01;
        kd.text = a02;
        kc.text = a03;
        qs.text = a04;
        qh.text = a05;
        qd.text = a06;
        qc.text = a07;
        js.text = a08;
        jh.text = a09;    
        jd.text = a10;
        jc.text = a11;
    }
}
