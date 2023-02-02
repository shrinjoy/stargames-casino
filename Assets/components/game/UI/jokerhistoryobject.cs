using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jokerhistoryobject : MonoBehaviour
{
  [SerializeField]  TMPro.TMP_Text tid;
    [SerializeField] TMPro.TMP_Text gid;
    [SerializeField] TMPro.TMP_Text play;
    [SerializeField] TMPro.TMP_Text win;
    [SerializeField] TMPro.TMP_Text claim;
    [SerializeField] TMPro.TMP_Text result;
    [SerializeField] TMPro.TMP_Text drawtime;
    [SerializeField] TMPro.TMP_Text tickettime;
    public string barcode;
    public void setData(string bar,string ticket,string game_id,string played,string won,string claimed,string results,string drawingtime,string ticket_time)
    {
       tid.text = ticket;
        gid.text = game_id;
        play.text = played;
         win.text = won;
        if(claimed == "Print")
        {
            claimed = "N/W";
        }
        else if (claimed == "Prize")
        {
            claimed = "PZ";
        }
        else if (claimed == "Claimed")
        {
            claimed = "Claimed";
        }
        claim.text = claimed;   
        result.text = results;  
        drawtime.text = drawingtime;
        tickettime.text = DateTime.Parse(ticket_time).ToString("HH:mm:ss tt");
        barcode = bar;
    }

}
