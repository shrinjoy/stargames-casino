using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_bet : MonoBehaviour
{
    public void doublethebets()
    {
         foreach(betButtons btn in GameObject.FindObjectsOfType<betButtons>())
        {
            this.GetComponent<AudioSource>().Play();
            btn.betplaced = btn.betplaced + btn.betplaced;
            btn.updatebutton();
        }
    }
}
