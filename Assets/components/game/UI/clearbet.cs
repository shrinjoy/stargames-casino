using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearbet : MonoBehaviour
{
    public void clearbets()
    {
        foreach (betButtons btn in GameObject.FindObjectsOfType<betButtons>())
        {
            btn.removebet();
        }
    }
}
