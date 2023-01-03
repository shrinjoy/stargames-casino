using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timenow : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] TMPro.TMP_Text realtimetext;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        realtimetext.text = GameObject.FindObjectOfType<betManager>().gameResultTime;
    }
}
