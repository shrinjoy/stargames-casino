using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Claimall : MonoBehaviour
{
    public void claimbets()
    {
        
        this.GetComponentInParent<AudioSource>().Play();
        string command= "SELECT SUM(clm) as totalclaim  FROM [star].[dbo].[tasp]  WHERE  ter_id=" + GameObject.FindObjectOfType<userManager>().getUserData().id+" and status = 'Prize'";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection =GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int betamountwon = 0 ;
        if(sqlData.Read())
        {

            betamountwon = Convert.ToInt32(sqlData["totalclaim"].ToString());
            
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(Convert.ToInt32(GameObject.FindObjectOfType<userManager>().getUserData().id), betamountwon);
        GameObject.FindObjectOfType<topbarinfopanel>().updatedata();
        removestat();

    }
    void removestat()
    {
        string command = "UPDATE [star].[dbo].[tasp] set status='Claimed' WHERE  ter_id=" + GameObject.FindObjectOfType<userManager>().getUserData().id + " and status = 'Prize'";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read()) { }

        sqlData.Close();
        sqlData.DisposeAsync();
    }
}
