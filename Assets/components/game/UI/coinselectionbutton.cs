using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinselectionbutton : MonoBehaviour
{
    public int buttonvalue;
    public void setbetamount()
    {
        GameObject.FindObjectOfType<jokerGameManager>().coinselected = buttonvalue;
    }
}
