using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class cancelticket : MonoBehaviour
{
    public void cancel()
    {
        string command = "UPDATE [star].[dbo].[tasp] set status='Canceled' WHERE  ter_id=1517001 and g_id="+GameObject.FindObjectOfType<claimmanager>().gameid;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {

        }

        sqlData.Close();
    }
}
