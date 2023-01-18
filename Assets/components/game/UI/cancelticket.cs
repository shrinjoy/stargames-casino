using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using System;

public class cancelticket : MonoBehaviour
{
    public void cancel()
    {
       
        this.GetComponent<AudioSource>().Play();
        int totalbetplaced=0;
        string command = "UPDATE [star].[dbo].[tasp] set status='Canceled' WHERE status='Print' and ter_id="+GameObject.FindObjectOfType<userManager>().getUserData().id+"and g_id="+GameObject.FindObjectOfType<claimmanager>().gameid+";";
        string command2 = "SELECT [tot] from [star].[dbo].[tasp] where status='Canceled' and ter_id=" + GameObject.FindObjectOfType<userManager>().getUserData().id + "and g_id=" + GameObject.FindObjectOfType<claimmanager>().gameid + ";";
        string finalcommand = command + command2;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = finalcommand;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            print(sqlData["tot"]);
            totalbetplaced = Convert.ToInt32(sqlData["tot"].ToString());
        }

        sqlData.Close();
        sqlData.Dispose();
        print(totalbetplaced);
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(Convert.ToInt32(GameObject.FindObjectOfType<userManager>().getUserData().id), totalbetplaced);
        GameObject.FindObjectOfType<topbarinfopanel>().updatedata();
    }
}
