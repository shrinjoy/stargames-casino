using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using Unity.VisualScripting;
using System;
using System.Threading.Tasks;

public class SQL_manager : MonoBehaviour
{
   
     SqlConnection SQLconn;
        
    private void Start()
    {
        DontDestroyOnLoad(this);
        SQLconn = initSQL();
       
        
    }
    //initSQL() inits the sql connection and opens it for other methods dependend on sql to run 
    public SqlConnection initSQL()
    {
        string sqlConnectionString = @"Data Source=103.76.228.21\SA,1433;User ID = sa; Password=cron@123#;Initial Catalog = star";
        SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
        sqlConnection.Open();
        return sqlConnection;
    }
    //canLogin() checks if user with certain id and pass is present in data base if not then it will return false other wise it will return true
    public bool canLogin(string id,string pass)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData=null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT * FROM[star].[dbo].[g_master] WHERE term_id ="+id+" and pass ="+pass;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["term_id"] != null)
            {
                if(this.GetComponent<userManager>())
                {
                    this.GetComponent<userManager>().setUserData(sqlData["term_id"].ToString(),sqlData["term_name"].ToString(), sqlData["pass"].ToString(), sqlData["macid"].ToString());
                }
                sqlData.Close();
                return true;
            }
            else
            {
                sqlData.Close();
                return false;
            }

        }
        sqlData.Close();    
        return false;
    }
   
}
