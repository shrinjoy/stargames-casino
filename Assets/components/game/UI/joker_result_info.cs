using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

using System;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class joker_result_info : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject dattimeprefab;
    [SerializeField] GameObject content;
    jokerGameManager jkm = new jokerGameManager();
    [SerializeField]calender cal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            fetchdata("");
        }
    }
    public void getresult()
    {
        foreach (Transform gb in content.GetComponentsInChildren<Transform>())
        {
            if (gb.transform.gameObject != content.transform.gameObject)
            {
                Destroy(gb.gameObject);
                print(gb.name);
            }
        }
        fetchdata(cal.date);
    }
    public void fetchdata(string date)
    {
        

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT  [g_date],[g_time],[result] FROM [star].[dbo].[resultsTaa] WHERE g_date='"+date+"'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while(sqlData.Read())
        {
            GameObject gb=(GameObject)Instantiate(dattimeprefab,content.transform.position,Quaternion.identity,content.transform);
            string xdate = sqlData["g_date"].ToString();
            string xtime = sqlData["g_time"].ToString();
            gb.GetComponent<resultinfosetter>().setdata(xdate + " " +xtime , jkm.serverresulttogameresultconverter(sqlData["result"].ToString()));
        }
        sqlData.Close();
    }

}
public struct  result_Date_time
{
    public string date;
    public string time;
    public string result;
}