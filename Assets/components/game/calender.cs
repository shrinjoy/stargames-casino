using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calender : MonoBehaviour
{
    public GameObject calendergb;
    public TMPro.TMP_Dropdown days;
    public TMPro.TMP_Dropdown month;
    public TMPro.TMP_InputField calenderfield;
    public string date;
    private void Start()
    {
        date = DateTime.Today.ToString("yyyyMMdd");
    }
    public void setcalender()
    {

        date = DateTime.Now.ToString("yyyy")+"-"+month.value.ToString()+"-"+days.value.ToString();
        print(date);
        calenderfield.text = date;
        calendergb.gameObject.SetActive(false);
      
    }
}
