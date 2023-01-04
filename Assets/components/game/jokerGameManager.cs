using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class jokerGameManager : timeManager
{
    public TMPro.TMP_Text timer;
    public TMPro.TMP_Text resulttext;
    public GameObject resultmarker;
    [SerializeField] TMPro.TMP_Text bettext;
    public bool showresult = false;
    public int coinselected = 0;
    [SerializeField] betButtons[] btns;
    bool resultsent = false;
    public string result;
    private void Update()
    {

        timer.text = GameObject.FindObjectOfType<timeManager>().realtime.ToString();
        if(realtime>10)
        {
            bettext.text = "place your bets";
        }
        if (realtime <= 10 && resultsent ==false)
        {
            bettext.text = "no more bets please ";
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
        result = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<betManager>().getResult("joker");
        string status = "Print";
        string gm = "gm";
        string barcode = GameObject.FindObjectOfType<userManager>().getUserData().id+DateTime.Today.ToString().Replace("/"," ").Replace(" ","")+DateTime.UtcNow.ToString().Replace("/"," ").Replace(" ","");
        print(barcode);
        string command = "INSERT INTO [taas].[dbo].[tasp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11," +
            "tot,qty," +
            "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values (" 
            + btns[0].betplaced + "," + btns[1].betplaced + "," + btns[2].betplaced + "," + btns[3].betplaced + "," + btns[4].betplaced + "," + btns[5].betplaced + "," + btns[6].betplaced + "," + btns[7].betplaced + "," + btns[8].betplaced + "," + btns[9].betplaced + "," + btns[10].betplaced + "," + btns[11].betplaced 
            + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + "," 
            + "'" + DateTime.Today + "'" + "," +"'"+status+"'" + "," + GameObject.FindObjectOfType<userManager>().getUserData().id + "," + GameObject.FindObjectOfType<betManager>().gameResultId + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + ","+"'" + DateTime.Now + "'" + "," + "'"+barcode+"'" +","+ "'" + gm + "'"+","+1+")";
        print(command);
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqldata=null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;
       sqldata= sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        sqldata.Read();
        sqldata.Close();
        

    }
    public override void GameSequence()
    {
        showresult = true;
     
        print("game sequnce started");
       

       

    }
    string serverresulttogameresultconverter(string betresulttext)
    {
        string outdata=null;
        if(betresulttext=="NR00")
        {
            outdata = "JC";
        }
        if (betresulttext == "NR01")
        {
            outdata = "JD";
        }

        if (betresulttext == "NR02")
        {
            outdata = "JS";
        }

        if (betresulttext == "NR03")
        {
            outdata = "JH";
        }

        if (betresulttext == "NR04")
        {
            outdata = "QC";
        }
        if (betresulttext == "NR05")
        {
            outdata = "QD";
        }
        if (betresulttext == "NR06")
        {
            outdata = "QS";
        }
        if (betresulttext == "NR07")
        {
            outdata = "QH";
        }

        if (betresulttext == "NR08")
        {
            outdata = "KC";
        }

        if (betresulttext == "NR09")
        {
            outdata = "KD";
        }

        if (betresulttext == "NR10")
        {
            outdata = "KS";
        }

        if (betresulttext == "NR11")
        {
            outdata = "KH";
        }



        return outdata;
    }
    int resulttoNumber(string betresult)
    {
     
        int sector = 0;
        //jh0 qc1 kd2 jc3 qd4 js5 kh6 qs7 qh8 kc9 jd10 ks11 
        if(betresult=="KS")
        {
            sector = 0;
        }
        if (betresult == "JH")
        {
            sector = 1;
        }
        if (betresult == "QC")
        {
            sector = 2;
        }
        if (betresult == "QD")
        {
            sector = 3;
        }
        if (betresult == "JS")
        {
            sector = 4;
        }
        if (betresult == "KH")
        {
            sector = 5;
        }
        if (betresult == "JC")
        {
            sector = 6;
        }
        if (betresult == "KD")
        {
            sector = 7;
        }
        if (betresult == "QS")
        {
            sector = 8;
        }
        if (betresult == "QH")
        {
            sector = 9;
        }
        if (betresult == "JD")
        {
            sector = 10;
        }
        if (betresult == "KC")
        {
            sector = 11;
        }
        
        return sector;
    }
    IEnumerator showresulttext()
    {
        showresult = false;
        timer.enabled = false;
        GameObject.FindObjectOfType<FortuneWheelManager>().TurnWheel(resulttoNumber(serverresulttogameresultconverter(result)));
        result = serverresulttogameresultconverter(result);
        resulttext.text = result;
        GameObject.FindObjectOfType<HistoryPanelManager>().addHistory();
        print("before while loop");
        while (GameObject.FindObjectOfType<FortuneWheelManager>().isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }



        print("after while loop");


        StartCoroutine(markeranimation());


        StartCoroutine(showresulttextanimation());


        print("result shown");
        resultsent = false;
        resetTimer();
        timer.enabled = true;
        resultmarker.SetActive(false);
        yield return null;
    }
    IEnumerator showresulttextanimation()
    {
        resulttext.enabled = true;
        yield return new WaitForSecondsRealtime(5.0f);
        resulttext.enabled = false;
    }
    IEnumerator markeranimation()
    {
        resultmarker.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        resultmarker.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        resultmarker.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        resultmarker.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        resultmarker.SetActive(true);
      
    }
}
