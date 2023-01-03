using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalbet : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] TMPro.TMP_Text displaytxt;
    public int totalbetamount;
    betButtons[] gbs;
    void Start()
    {
        gbs = GameObject.FindObjectsOfType<betButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(betButtons betbutn in gbs)
        {
            totalbetamount += betbutn.betplaced;
        }
        displaytxt.text = totalbetamount.ToString();    
        totalbetamount = 0;
    }
}
