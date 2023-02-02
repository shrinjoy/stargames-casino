using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sametypebetplaced : MonoBehaviour
{
    [SerializeField] betButtons[] btns;
    public void onsametypebuttonpressed()
    {
        foreach(betButtons bt in btns)
        {
            bt.placebet();
        }
    }
}
