using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claimsetter : MonoBehaviour
{
    [SerializeField]TMPro.TMP_Text gameid;
   public void setclaimdata()
    {
        GameObject.FindObjectOfType<claimmanager>().gameid = gameid.text;
        GameObject.FindObjectOfType<claimmanager>().barcode = this.GetComponent<jokerhistoryobject>().barcode;
    }
}
