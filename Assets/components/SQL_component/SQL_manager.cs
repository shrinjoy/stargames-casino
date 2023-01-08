using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

using System;


public class SQL_manager : MonoBehaviour
{
   
   public  SqlConnection SQLconn;
        
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SQLconn = initSQL();
        print(DateTime.Today.ToString("yyyy-MM-dd"));
     
    }
    public void Update()
    {
       

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
    public int balance(int termid)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT lim from [star].[dbo].[g_master] where term_id="+termid;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if(sqlData.Read())
        {
            int bal = Convert.ToInt32(sqlData["lim"].ToString());
            sqlData.Close();
            return bal;
        }
        sqlData.Close();
        return 0;
    }
    public void updatebalanceindatabase(int termid)
    {
        int updatedbal = balance(termid);
      
    }
    public string betResult(string time,int id,string gamename)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        if (gamename == "joker")
        {
            sqlCmnd.CommandText = "SELECT * FROM [star].[dbo].[resultsTaa] WHERE g_time=" + "'" + time + "'"+" and g_date="+"'"+ DateTime.Today.ToString("yyyy-MM-dd")+"'";//this is the sql command we use to get data about user
        }
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            string result = "";
            if (gamename == "joker")
            {
                result = sqlData["result"].ToString();
                sqlData.Close();
            }
            
            sqlData.Close();
            return result;
        }
        sqlData.Close();
        return null;
    }
    public DateTime timeForNextGame()
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT * FROM [star].[dbo].[g_rule12] WHERE tag=1";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["g_time"] != null)
            {
                string time = sqlData["g_time"].ToString();
                DateTime date = DateTime.ParseExact(time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
               
                if(this.GetComponent<betManager>()!=null)
                {
                    this.GetComponent<betManager>().setResultData(time, Convert.ToInt32(sqlData["id"].ToString()));
                }
                sqlData.Close();
                return (date.ToUniversalTime());
            }
            else
            {
                print("no tag 1");
                sqlData.Close();
                return DateTime.Now;
            }

        }
        sqlData.Close();
        return DateTime.Now;
    }

}
