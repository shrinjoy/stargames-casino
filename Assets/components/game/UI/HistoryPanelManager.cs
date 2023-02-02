using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using UnityEngine;


public class HistoryPanelManager : MonoBehaviour
{
    [SerializeField]GameObject resultprefab;
    [SerializeField] GameObject content;
    bool donesetuphistorypanel = false;
    jokerGameManager jkm = new jokerGameManager();
    public void addHistory()
    {
        GameObject gb = (GameObject)Instantiate(resultprefab);
       // gb.transform.position = content.transform.position;
       // gb.transform.rotation = Quaternion.identity;
        gb.transform.parent = content.transform;
        //
       
        gb.GetComponent<resultHistory>().setresultdata(GameObject.FindObjectOfType<betManager>().gameResultTime, GameObject.FindObjectOfType<jokerGameManager>().result);
    }
    private void Update()
    {
        if(donesetuphistorypanel==false)
        {
            StartCoroutine(addlast6gameresults()); 

            donesetuphistorypanel= true;
        }
    }
    IEnumerator  addlast6gameresults()
    {
        string endtime=GameObject.FindObjectOfType<betManager>().gameResultTime;
        string starttime = DateTime.Parse(endtime).AddMinutes(-12).ToString("hh:mm:ss tt");

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "  SELECT  [result],[g_time] FROM [star].[dbo].[resultsTaa] where  g_time between '" + starttime + "' and '" + endtime + "' and g_date='" + DateTime.Today.ToString("dd-MMM-yyyy")+" "+ "00:00:00.000'";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while(sqlData.Read())
        {
            GameObject gb = (GameObject)Instantiate(resultprefab);
            // gb.transform.position = content.transform.position;
            // gb.transform.rotation = Quaternion.identity;
            gb.transform.parent = content.transform;
            //
            
            gb.GetComponent<resultHistory>().setresultdata(DateTime.Parse(sqlData["g_time"].ToString()).ToString(), jkm.serverresulttogameresultconverter(sqlData["result"].ToString()));
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        yield return null;
    }
}
