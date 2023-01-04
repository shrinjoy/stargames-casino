using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class topbarinfopanel : MonoBehaviour
{
    [SerializeField]public TMPro.TMP_Text balancetext;
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
    }
}
