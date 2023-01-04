using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class HistoryPanelManager : MonoBehaviour
{
    [SerializeField]GameObject resultprefab;
    [SerializeField] GameObject content;
    public void addHistory()
    {
        GameObject gb = (GameObject)Instantiate(resultprefab);
       // gb.transform.position = content.transform.position;
       // gb.transform.rotation = Quaternion.identity;
        gb.transform.parent = content.transform;
        //
       
        gb.GetComponent<resultHistory>().setresultdata(GameObject.FindObjectOfType<betManager>().gameResultTime, GameObject.FindObjectOfType<jokerGameManager>().result);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) { 
        addHistory();
        }
    }
}
