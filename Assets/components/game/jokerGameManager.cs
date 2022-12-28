using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class jokerGameManager : timeManager                     
{
    public TMPro.TMP_Text timer;
    public TMPro.TMP_Text resulttext;
    public bool showresult = false;

    private void Update()
    {
        
        timer.text = GameObject.FindObjectOfType<timeManager>().realtime.ToString();
        timer.enabled = !GameObject.FindObjectOfType<FortuneWheelManager>().isspinning;
        if(showresult==true)
        {
            StartCoroutine(showresulttext());
        }
    }
    public override void GameSequence()
    {
        showresult= true;
        GameObject.FindObjectOfType<FortuneWheelManager>().TurnWheel(2);
        print("game sequnce started");
        resulttext.text = GameObject.FindGameObjectWithTag("SQLmanager").GetComponent<betManager>().getResult("joker");

        resetTimer();

    }
   IEnumerator showresulttext()
    {
        showresult =false;
       
       
        print("before while loop");
        while (GameObject.FindObjectOfType<FortuneWheelManager>().isspinning==true)
        {
            yield return new WaitForEndOfFrame();
        }



        print("after while loop");


      
        resulttext.enabled = true;
        yield return new WaitForSecondsRealtime(5.0f);
        resulttext.enabled = false;

       
       

        print("result shown");
        yield return null;
    }
}
