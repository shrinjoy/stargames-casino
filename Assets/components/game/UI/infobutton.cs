using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infobutton : MonoBehaviour
{
    public GameObject infopanel;
    public void turninfoon()
    {
        this.GetComponent<AudioSource>().Play();    
        infopanel.SetActive(true); 
    }
    public void turninfooff()
    {
        infopanel.SetActive(false);
    }
}
