using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

using System;




public class SQL_manager : MonoBehaviour
{
   
   public  SqlConnection SQLconn;
   public TMPro.TMP_Text warningtext;
    public DateTime server_day;
    private void OnEnable()
    {
       // Screen.SetResolution(1024, 768,FullScreenMode.Windowed);
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SQLconn = initSQL();
        print(DateTime.Today.ToString("yyyy-MM-dd"));
     
    }
    public void Update()
    {
       

    }
    public DateTime get_time()
    {
        DateTime dt = new DateTime();
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT GETDATE() as CurrentTime";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if(sqlData.Read())
        {
            dt = DateTime.Parse(sqlData["CurrentTime"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        server_day = DateTime.Parse(dt.ToString("dd-MMM-yyyy"));
        dt = DateTime.Parse(dt.ToString("hh:mm:ss tt"));
        return dt;
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
    public bool canLogin(string id,string pass,string macid)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData=null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT * FROM[star].[dbo].[g_master] WHERE term_id =" + id + " and pass =" +pass;//this is the sql command we use to get data about user
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["pass"].ToString() == pass && sqlData["term_id"].ToString()==id)
            {
                print("pass found with id");
                if (sqlData["macid"].ToString() == macid)
                {
                    print("mac id found");
                    if (this.GetComponent<userManager>())
                    {
                        this.GetComponent<userManager>().setUserData(sqlData["term_id"].ToString(), sqlData["term_name"].ToString(), sqlData["pass"].ToString(), sqlData["macid"].ToString(), sqlData["comm"].ToString());
                        sqlData.Close();
                        sqlData.DisposeAsync();

                        return true;
                    }
                }
                if (sqlData["macid"].ToString() != macid)
                {
                    print("invalid mac id");
                    warningtext.text = "Please wait for admin approval";
                    sqlData.Close();
                    sqlData.DisposeAsync();
                    addmacid(macid,id);
                    return false;
                }
              
                
            }
            if (sqlData["pass"].ToString() != pass || sqlData["term_id"].ToString() != id|| sqlData["pass"].ToString()==null || sqlData["term_id"].ToString() == null)
            {
                
                warningtext.text = "Invalid ID or Password";
                sqlData.Close();
                sqlData.DisposeAsync();
                return false;
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return false;
    }
    public void addmacid(string macid,string termid)
    {
        string command = "UPDATE [star].[dbo].[g_master] set newmacid='" + macid + "',flag=3 WHERE term_id=" + termid;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText =command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
        }
        sqlData.Close();
        sqlData.DisposeAsync();
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
            sqlData.DisposeAsync() ;
            return bal;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return 0;
    }
    public void updatebalanceindatabase(int termid,int totalbetplaced)
    {
        int mainbal = balance(termid);
        int updatedbal = mainbal-totalbetplaced ;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "UPDATE [star].[dbo].[g_master]  SET lim="+updatedbal+" WHERE term_id="+termid;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        sqlData.Read();
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public void addubalanceindatabase(int termid, int claim)
    {
        int mainbal = balance(termid);
        int updatedbal = mainbal + claim;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "UPDATE [star].[dbo].[g_master]  SET lim=" + updatedbal + " WHERE term_id=" + termid;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        sqlData.Read();
        sqlData.Close();
        sqlData.DisposeAsync();
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
            sqlCmnd.CommandText = "SELECT * FROM[star].[dbo].[resultsTaa] where g_date = '"+server_day.ToString("dd-MMM-yyyy 00:00:00.000")+"' and g_time = '"+GameObject.FindObjectOfType<betManager>().gameResultTime+"'";//this is the sql command we use to get data about user
        }
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            string result = "";
            if (gamename == "joker")
            {
                result = sqlData["result"].ToString()+sqlData["status"].ToString();
                print(result);
                sqlData.Close();
                sqlData.DisposeAsync();
            }
            
            sqlData.Close();
            sqlData.DisposeAsync();
            return result;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return null;
    }
    public DateTime timeForNextGame()
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT * FROM [star].[dbo].[g_rule12] WHERE tag=1;";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["g_time"] != null)
            {
                string time = sqlData["g_time"].ToString();
                DateTime date = DateTime.Parse(DateTime.Parse(time).ToString("hh:mm:ss tt"));
               
                if(this.GetComponent<betManager>()!=null)
                {
                    this.GetComponent<betManager>().setResultData(time, Convert.ToInt32(sqlData["id"].ToString()));
                }
                sqlData.Close();
                sqlData.DisposeAsync();
                return (date);
            }
            else
            {
                print("no tag 1");
                sqlData.Close();
                sqlData.DisposeAsync();
                return DateTime.Now;
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return DateTime.Now;
    }

}
