using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class calender : MonoBehaviour
{
    public GameObject calendergb;
    public TMPro.TMP_Dropdown days;
    public TMPro.TMP_Dropdown month;
    public TMPro.TMP_InputField calenderfield;
    public TMPro.TMP_Dropdown year;
    public string date;

    List<string> years=new List<string>();
    int startingyear = 2022;
    private void Start()
    {
        date = DateTime.Today.ToString("yyyy/MM/dd");
        calenderfield.text = date.ToString();
        for(int i =0;i<100;i++)
        {
            startingyear = startingyear + 1;
            years.Add(startingyear.ToString()); 

        }
        year.AddOptions(years);
       
            

    }
    public void setcalender()
    {

        date = year.GetComponentInChildren<TMPro.TMP_Text>().text+"-"+ month.GetComponentInChildren<TMPro.TMP_Text>().text + "-"+ days.GetComponentInChildren<TMPro.TMP_Text>().text;
        print(date);
        calenderfield.text = date;
        calendergb.gameObject.SetActive(false);
      
    }
}
