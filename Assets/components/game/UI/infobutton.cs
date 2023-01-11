using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infobutton : MonoBehaviour
{
    public GameObject infopanel;
    public void turninfoon()
    {
        infopanel.SetActive(true); 
    }
    public void turninfooff()
    {
        infopanel.SetActive(false);
    }
}
