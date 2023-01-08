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
       
           
        totalbetamount = gbs[0].betplaced+ gbs[1].betplaced+ gbs[2].betplaced+ gbs[3].betplaced+ gbs[4].betplaced+gbs[5].betplaced+gbs[6].betplaced + gbs[7].betplaced + gbs[8].betplaced + gbs[9].betplaced + gbs[10].betplaced + gbs[11].betplaced
    displaytxt.text = totalbetamount.ToString();
    }
}
