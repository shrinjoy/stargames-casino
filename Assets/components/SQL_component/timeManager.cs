using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    // Start is called before the first frame update
    DateTime timetillnextgame;
    [HideInInspector]
    public int gameTime;
    [HideInInspector]
    public int realtime;
    bool isGameSequenceRunning = false;
   
    public void Start()
    {
        timetillnextgame =GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<SQL_manager>().timeForNextGame();//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);
        realtime = (int)(timetillnextgame - DateTime.Now.ToUniversalTime()).TotalSeconds;

    }

    // Update is called once per frame
    protected void FixedUpdate()
    {

        if (realtime <= 0.0f && isGameSequenceRunning==false)
        {
            isGameSequenceRunning = true;
          
            GameSequence();
           
        }
        else
        {

            realtime = (int)(timetillnextgame - DateTime.Now.ToUniversalTime()).TotalSeconds;
            gameTime = realtime + 10;
            
          
        }
        

    }
    public void resetTimer()
    {
        timetillnextgame = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<SQL_manager>().timeForNextGame();//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);       
        realtime = (int)(timetillnextgame - DateTime.Now.ToUniversalTime()).TotalSeconds;
        print("clock reseted new game in :" + realtime);
        isGameSequenceRunning = false;
    }

    public virtual void GameSequence() { }
    
}
