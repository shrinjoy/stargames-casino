using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topbarinfopanel : MonoBehaviour
{
    [SerializeField]TMPro.TMP_Text balancetext;
    [SerializeField] TMPro.TMP_Text userid;
    // Start is called before the first frame update
    void Start()
    {
        int id = Convert.ToInt32(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balancetext.text = GameObject.FindObjectOfType<SQL_manager>().balance(id).ToString();
        userid.text = GameObject.FindObjectOfType<userManager>().getUserData().id;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
