using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showcalender : MonoBehaviour
{
    GameObject gb;
    private void Start()
    {
        gb = this.GetComponent<calender>().calendergb;
    }
    public void showcal()
    {

        gb.SetActive(true);
    }
}
