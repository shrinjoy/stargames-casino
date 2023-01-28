using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claimsetter : MonoBehaviour
{
    [SerializeField]TMPro.TMP_Text gameid;
    [SerializeField] TMPro.TMP_Text barcode;
    public void setclaimdata()
    {
        GameObject.FindObjectOfType<claimmanager>().gameid = gameid.text;
        GameObject.FindObjectOfType<claimmanager>().barcode = barcode.text;
    }
}
