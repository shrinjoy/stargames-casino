using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using static UnityEditor.ShaderData;

public class jokerGameManager : timeManager
{
    public TMPro.TMP_Text timer;
    public TMPro.TMP_Text resulttext;
    public bool showresult = false;
    public int coinselected = 0;
    [SerializeField] betButtons[] btns;
    bool resultsent = false;
    private void Update()
    {

        timer.text = GameObject.FindObjectOfType<timeManager>().realtime.ToString();
        timer.enabled = !GameObject.FindObjectOfType<FortuneWheelManager>().isspinning;
        if (realtime <= 10 && resultsent ==false)
        {
            sendResult();
            resultsent= true;
        }
        if (showresult == true)
        {
            StartCoroutine(showresulttext());
        }
    }
    public void sendResult()
    {
        string status = "Print";
        string gm = "gm";
        string command = "INSERT INTO [taas].[dbo].[tasp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11," +
            "tot,qty," +
            "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values (" 
            + btns[0].betplaced + "," + btns[1].betplaced + "," + btns[2].betplaced + "," + btns[3].betplaced + "," + btns[4].betplaced + "," + btns[5].betplaced + "," + btns[6].betplaced + "," + btns[7].betplaced + "," + btns[8].betplaced + "," + btns[9].betplaced + "," + btns[10].betplaced + "," + btns[11].betplaced 
            + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + "," 
            + "'" + DateTime.Today + "'" + "," +"'"+status+"'" + "," + 69696 + "," + 660 + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + ","+"'" + DateTime.Now + "'" + "," + 01010 +","+ "'" + gm + "'"+","+1+")";
        print(command);
        SqlCommand sqlCmnd = new SqlCommand();
  
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;
        sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
      
        

    }
    public override void GameSequence()
    {
        showresult = true;
        GameObject.FindObjectOfType<FortuneWheelManager>().TurnWheel(2);
        print("game sequnce started");
        resulttext.text = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<betManager>().getResult("joker");

        resetTimer();

    }
    IEnumerator showresulttext()
    {
        showresult = false;


        print("before while loop");
        while (GameObject.FindObjectOfType<FortuneWheelManager>().isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }



        print("after while loop");



        resulttext.enabled = true;
        yield return new WaitForSecondsRealtime(5.0f);
        resulttext.enabled = false;




        print("result shown");
        resultsent = false;
        yield return null;
    }
}
