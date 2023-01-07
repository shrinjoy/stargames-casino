using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatButton : MonoBehaviour
{

    [SerializeField]List<betbuttondata> data = new List<betbuttondata>();
    public void resetolddata()
    {
        data.Clear();
     
    }
    public void repeatbets()
    {
        foreach(betbuttondata d in data)
        {
            if (d.betplaced > 0)
            {
                d.btn.panel.SetActive(true);
                d.btn.text.text = d.betplaced.ToString();
                d.btn.betplaced = d.betplaced;
            }
        }
        addbetbuttondata();
    }
  
    public void addbetbuttondata()
    {
        GameObject.FindObjectOfType<repeatButton>().resetolddata();
        foreach (betButtons bts in GameObject.FindObjectsOfType<betButtons>())
        {
            if (bts.betplaced > 0)
            {
                betbuttondata betdata = new betbuttondata();
                betdata.btn = bts;
                betdata.betplaced = bts.betplaced;
                data.Add(betdata);
            }
        }
        print("saved data");
    }
}
[System.Serializable]
public struct betbuttondata
{
    public int betplaced;
    public betButtons btn;
}