using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinselectionbutton : MonoBehaviour
{
    public int buttonvalue;
    Vector3 scalebtn;
    private void Start()
    {
        scalebtn = this.transform.localScale;
    }
    public void makemebigger()
    {
        foreach(coinselectionbutton btn  in GameObject.FindObjectsOfType<coinselectionbutton>())
        {
            btn.resetsize();
        }
        this.transform.localScale = this.transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
    }
    public void resetsize()
    {
        this.transform.localScale = scalebtn;
    }


    public void setbetamount()
    {
        makemebigger();
        
        this.GetComponentInParent<AudioSource>().Play();
        GameObject.FindObjectOfType<jokerGameManager>().coinselected = buttonvalue;
    }
}
