using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class claimmanager : MonoBehaviour
{
    public string gameid;
    public string barcode;
    public void claim()
    {
        string command = "SELECT * FROM [star].[dbo].[tasp]  WHERE  ter_id="+GameObject.FindObjectOfType<userManager>().getUserData().id+" and g_id=" +gameid+ " and status='Prize' and bar='"+barcode+"'";
        print(command);
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int betamountwon = 0;
        if (sqlData.Read())
        {

            betamountwon = Convert.ToInt32(sqlData["clm"].ToString());

        }
        sqlData.Close();
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(Convert.ToInt32(GameObject.FindObjectOfType<userManager>().getUserData().id), betamountwon);
        GameObject.FindObjectOfType<topbarinfopanel>().updatedata();
        
        removestat();

    }
    void removestat()
    {
        string command = "UPDATE [star].[dbo].[tasp] set status='Claimed' WHERE  ter_id=" + GameObject.FindObjectOfType<userManager>().getUserData().id +"and g_id="+gameid+ " and status = 'Prize' and bar='"+barcode+"'";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read()) { }

        sqlData.Close();
    }
}
