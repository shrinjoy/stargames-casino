using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearbet : MonoBehaviour
{
    betButtons[] btns;
    private void Start()
    {
        btns = GameObject.FindObjectsOfType<betButtons>();
    }
    public void clearbets()
    {
        this.GetComponent<AudioSource>().Play();

        GameObject.FindObjectOfType<repeatButton>().addbetbuttondata();
            print("saved buttons");
        
        foreach (betButtons btn in GameObject.FindObjectsOfType<betButtons>())
        {
            btn.removebet();

        }
        print("cleared bets");
    }
}
