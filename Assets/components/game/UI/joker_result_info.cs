using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

using System;

public class joker_result_info : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject dattimeprefab;
    [SerializeField] GameObject content;
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
    public void fetchdata(string date)
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT  [g_date],[g_time],[result] FROM [star].[dbo].[resultsTaa] WHERE g_date='2022-12-03'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while(sqlData.Read())
        {
         GameObject gb=   (GameObject)Instantiate(dattimeprefab,content.transform.position,Quaternion.identity,content.transform);
            gb.GetComponent<resultinfosetter>().setdata(sqlData["g_date"].ToString() + " " + sqlData["g_time"].ToString(), sqlData["result"].ToString());
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