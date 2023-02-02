using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    // Start is called before the first frame update
    DateTime timetillnextgame;
    DateTime servertime;
    [HideInInspector]
    public int gameTime;
    [HideInInspector]
    protected double realtime=0;
    bool isGameSequenceRunning = false;
   
    public void Start()
    {
        timetillnextgame =GameObject.FindObjectOfType<SQL_manager>().timeForNextGame();//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);
       
        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        print("time till next game:"+timetillnextgame);
        print("server time:" +servertime );

        double ts =(timetillnextgame - servertime).TotalSeconds;
        realtime = ts;  
        print(realtime);
        StartCoroutine(timeloop());
       
    }

    // Update is called once per frame
   public IEnumerator timeloop() 
    {

            while (true)
            {
                if (realtime <= 0.0f)
                {
                  

                    GameSequence();

                }
                else
                {

                realtime -= 1;
             

                }
                yield return new WaitForSeconds(1.0f);
            }

    }
    public void resetTimer()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame();//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        print("time till next game:" + timetillnextgame);
        print("server time:" + servertime);

        double ts = (timetillnextgame - servertime).TotalSeconds;
        realtime = ts;
        print(realtime);
    }

    public virtual void GameSequence() { }
    

}
