using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class jokerGameManager : timeManager
{
    public TMPro.TMP_Text timer;
    public TMPro.TMP_Text resulttext;
    public TMPro.TMP_Text shadowresult;
    public TMPro.TMP_Text winamounttext;
    public GameObject resultmarker;
    [SerializeField] TMPro.TMP_Text bettext;
    public bool showresult = false;
    public int coinselected = 0;
    [SerializeField] betButtons[] btns;
    bool resultsent = false;
    public string result;
    public GameObject starticonanimation;
    private void Start()
    {
        base.Start();
        GameObject.FindObjectOfType<topbarinfopanel>().updatedata();
        GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<betManager>().getResult("joker");
        resulttext.enabled = true;

    }
    private void Update()
    {

        int minutes = Mathf.FloorToInt(realtime / 60F);
        int seconds = Mathf.FloorToInt(realtime - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        shadowresult.text = resulttext.text;
        shadowresult.enabled = resulttext.isActiveAndEnabled;
        timer.text = niceTime;
        if(realtime>10)
        {
            
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

    public void changebetplacedtext() {
        if (Convert.ToInt32(GameObject.FindObjectOfType<topbarinfopanel>().balancetext.text) >= GameObject.FindObjectOfType<totalbet>().totalbetamount)
        {
            if (realtime > 10.0f)
            {
                StartCoroutine(betplacetextanim());
            }
        }
        else
        {
            StartCoroutine(notenoughamountanim());
        }
    }
    IEnumerator betplacetextanim()
    {
        bettext.text = "bet accepted";
        yield return new WaitForSeconds(2);
        bettext.text = "place your bets";
    }
    IEnumerator notenoughamountanim()
    {
        bettext.text = "not enough balance";
        yield return new WaitForSeconds(2);
        if (realtime < 10)
        {
            bettext.text = "no more bets please";
        }
        else
        {
            bettext.text = "place your bets";
        }
    }
    public void sendResult()
    {
        if (Convert.ToInt32(GameObject.FindObjectOfType<topbarinfopanel>().balancetext.text) >= GameObject.FindObjectOfType<totalbet>().totalbetamount)
        {      
            string status = "Print";
            string gm = "gm";
            string barcode = GameObject.FindObjectOfType<userManager>().getUserData().id + DateTime.Today.ToString().Replace("/", " ").Replace(" ", "") + DateTime.UtcNow.ToString().Replace("/", " ").Replace(" ", "");
            print(barcode);
            string command = "INSERT INTO [star].[dbo].[tasp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11," +
                "tot,qty," +
                "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values ("
                + btns[0].betplaced + "," + btns[1].betplaced + "," + btns[2].betplaced + "," + btns[3].betplaced + "," + btns[4].betplaced + "," + btns[5].betplaced + "," + btns[6].betplaced + "," + btns[7].betplaced + "," + btns[8].betplaced + "," + btns[9].betplaced + "," + btns[10].betplaced + "," + btns[11].betplaced
                + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + "," + GameObject.FindObjectOfType<totalbet>().totalbetamount + ","
                + "'" + DateTime.Today + "'" + "," + "'" + status + "'" + "," + GameObject.FindObjectOfType<userManager>().getUserData().id + "," + GameObject.FindObjectOfType<betManager>().gameResultId + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + barcode + "'" + "," + "'" + gm + "'" + "," + 2 + ")";
            print(command);
            SqlCommand sqlCmnd = new SqlCommand();
            SqlDataReader sqldata = null;
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = command;
            sqldata = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            
            sqldata.Read();
            sqldata.Close();
            GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<SQL_manager>().updatebalanceindatabase(Convert.ToInt32(GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<userManager>().getUserData().id), GameObject.FindObjectOfType<totalbet>().totalbetamount);
            GameObject.FindObjectOfType<repeatButton>().resetolddata();
            GameObject.FindObjectOfType<repeatButton>().addbetbuttondata();
            
        }
        else
        {
            StartCoroutine(notenoughamountanim());
        }

    }
    public override void GameSequence()
    {
        showresult = true;
     
        print("game sequnce started");
       

       

    }
    public string serverresulttogameresultconverter(string betresulttext)
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
        if(betresult=="JH")
        {
            sector = 0;
        }
        if (betresult == "QC")
        {
            sector = 1;
        }
        if (betresult == "QD")
        {
            sector = 2;
        }
        if (betresult == "JS")
        {
            sector = 3;
        }
        if (betresult == "KH")
        {
            sector = 4;
        }
        if (betresult == "JC")
        {
            sector = 5;
        }
        if (betresult == "KD")
        {
            sector = 6;
        }
        if (betresult == "QS")
        {
            sector = 7;
        }
        if (betresult == "QH")
        {
            sector = 8;
        }
        if (betresult == "JD")
        {
            sector = 9;
        }
        if (betresult == "KC")
        {
            sector = 10;
        }
        if (betresult == "KS")
        {
            sector = 11;
        }
        
        return sector;
    }
    IEnumerator showresulttext()
    {
        result = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<betManager>().getResult("joker");
        print(result);
        starticonanimation.SetActive(true);
        resulttext.enabled = false;
        resultmarker.SetActive(false);
        showresult = false;
        timer.enabled = false;
        GameObject.FindObjectOfType<FortuneWheelManager>().TurnWheel(resulttoNumber(serverresulttogameresultconverter(result)));
        
        resulttext.text = serverresulttogameresultconverter(result);
    
        print("before while loop");
        while (GameObject.FindObjectOfType<FortuneWheelManager>().isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }



        print("after while loop");

        starticonanimation.SetActive(false);
        StartCoroutine(markeranimation());


        resulttext.enabled = true;


        print("result shown");
        result = serverresulttogameresultconverter(result);
        GameObject.FindObjectOfType<HistoryPanelManager>().addHistory();
        resultsent = false;
        //fetch win amount here
        getwinamount();
        resetTimer();
        timer.enabled = true;
        resultmarker.SetActive(false);
        GameObject.FindObjectOfType<topbarinfopanel>().updatedata();
        GameObject.FindObjectOfType<clearbet>().clearbets();
        bettext.text = "place your bets";
        foreach(betButtons btn in btns)
        {
            btn.removebet();
        }
        yield return null;
    }
    string getwinamount()
    {
        string winamount = "0";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [clm_tm] FROM [star].[dbo].[tasp] where g_id="+GameObject.FindObjectOfType<betManager>().gameResultId +" and g_time='"+ GameObject.FindObjectOfType<betManager>().gameResultTime+"'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            winamount = sqlData["clm_tm"].ToString();
           winamounttext.text = winamount;
            if(winamount=="NULL")
            {
               
            }
        }
        sqlData.Close();
        return winamount;
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
