using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topbarinfopanel : MonoBehaviour
{
    [SerializeField]public TMPro.TMP_Text balancetext;
    [SerializeField] public TMPro.TMP_Text fakebalance;
    [SerializeField] TMPro.TMP_Text userid;
    [SerializeField] TMPro.TMP_Text gameid;
    // Start is called before the first frame update
    void Start()
    {
        updatedata();  
    }
    public void updatedata()
    {
        int id = Convert.ToInt32(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balancetext.text = GameObject.FindObjectOfType<SQL_manager>().balance(id).ToString();
      
  
    }
    // Update is called once per frame
    void Update()
    {
        userid.text = GameObject.FindObjectOfType<userManager>().getUserData().id;
        gameid.text = GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
        fakebalance.text = Math.Clamp((Convert.ToInt32(balancetext.text) - Convert.ToInt32(GameObject.FindObjectOfType<totalbet>().totalbetamount)), 0, 99999999).ToString();

    }
}
