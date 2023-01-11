using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultinfosetter : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text resulttext;
    [SerializeField] TMPro.TMP_Text datetimetext;
    public void setdata(string datetime,string result)
    {
        resulttext.text = result;
        datetimetext.text = datetime;
    }
}
