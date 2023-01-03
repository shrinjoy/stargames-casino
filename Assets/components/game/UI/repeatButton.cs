using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatButton : MonoBehaviour
{

    betButtons[] data = new betButtons[12];
    int index;
    public void repeatbets()
    {

    }
    public void addbetbuttondata(betButtons bt,int value)
    {
       
    
    }
}
[System.Serializable]
public struct betbuttondata
{
    public int betplaced;
    public betButtons btn;
}