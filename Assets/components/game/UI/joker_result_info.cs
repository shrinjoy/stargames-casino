using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

using System;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Globalization;

public class joker_result_info : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject dattimeprefab;
    [SerializeField] GameObject historyprefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject historycontent;
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
              
            }
        }
       
        fetchdata(cal.date);
    }
    public void gethistory()
    {

        foreach (Transform gb in historycontent.GetComponentsInChildren<Transform>())
        {
            if (gb.transform.gameObject != historycontent.transform.gameObject)
            {
                Destroy(gb.gameObject);

            }
        }
        fetchdata(cal.date,1);
    }
    public void fetchdata(string date,int mode=0)
    {
        this.GetComponentInParent<AudioSource>().Play();

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        if (mode == 0)
        {
            sqlCmnd.CommandText = "SELECT  * FROM [star].[dbo].[resultsTaa] WHERE g_date='" + date + "'";//this is the sql command we use to get data about user
        } 
        if(mode == 1)
        {
            sqlCmnd.CommandText = "SELECT [star].[dbo].[tasp].bar,[star].[dbo].[tasp].g_id,[star].[dbo].[tasp].clm,[star].[dbo].[tasp].tot,[star].[dbo].[tasp].status,[star].[dbo].[tasp].g_time,[star].[dbo].[tasp].p_time,[star].[dbo].[resultsTaa].result as gameresult FROM [star].[dbo].[tasp],[star].[dbo].[resultsTaa] WHERE resultsTaa.g_date=tasp.g_date and resultsTaa.g_time=tasp.g_time and ter_id="+GameObject.FindObjectOfType<userManager>().getUserData().id+"order by g_id desc";
        }
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
    
        while(sqlData.Read())
        {
            if (mode == 0)
            {
                GameObject gb = (GameObject)Instantiate(dattimeprefab, content.transform.position, Quaternion.identity, content.transform);
                string xdate = sqlData["g_date"].ToString();
                string xtime = sqlData["g_time"].ToString();
                gb.GetComponent<resultinfosetter>().setdata(xdate + " " + xtime, jkm.serverresulttogameresultconverter(sqlData["result"].ToString())+"-"+sqlData["status"].ToString());
            }
            if(mode == 1) {

                GameObject gb = (GameObject)Instantiate(historyprefab, historycontent.transform.position, Quaternion.identity, historycontent.transform);
              
                gb.GetComponent<jokerhistoryobject>().setData(sqlData["bar"].ToString(), sqlData["bar"].ToString(), sqlData["g_id"].ToString(), sqlData["tot"].ToString(), sqlData["clm"].ToString(), sqlData["status"].ToString(), jkm.serverresulttogameresultconverter(sqlData["gameresult"].ToString()), sqlData["g_time"].ToString(),sqlData["p_time"].ToString());
               
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }

}
public struct  result_Date_time
{
    public string date;
    public string time;
    public string result;
}