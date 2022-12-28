using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betManager : MonoBehaviour
{
 [SerializeField]   string gameResultTime;
  [SerializeField]  int gameResultId;
  
    SQL_manager sqm;
   
    private void Start()
    {
        sqm = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<SQL_manager>();
        
    }
    public void setResultData(string gameresulttime,int gameid)
    {
        gameResultTime= gameresulttime;
        gameResultId= gameid;
    }
    public string getResult(string gamemode)
    {
      string  gameResult = sqm.betResult(gameResultTime, gameResultId,gamemode);
      
        return gameResult;
    }
}
