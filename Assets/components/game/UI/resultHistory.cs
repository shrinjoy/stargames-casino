using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultHistory : MonoBehaviour
{
    [SerializeField]Sprite[] icons = new Sprite[4];
    [SerializeField]TMPro.TMP_Text result;
    [SerializeField] TMPro.TMP_Text timertext;
    [SerializeField] Image iconimage;
    public void setresultdata(string xtime,string xresult)
    {
        result.text = xresult;
        timertext.text = DateTime.Parse(xtime).ToString("HH:mm::ss");
        if (xresult == "KS" || xresult == "QS" || xresult == "JS")
        {
            iconimage.sprite = icons[0];

        }
        if (xresult == "KH" || xresult == "QH" || xresult == "JH")
        {
            iconimage.sprite = icons[1];
        }
        if (xresult == "KD" || xresult == "QD" || xresult == "JD")
        {
            iconimage.sprite = icons[2];
        }
        if (xresult == "KC" || xresult == "QC" || xresult == "JC")
        {
            iconimage.sprite = icons[3];
        }
        //KS QS JS KC QC JC black
        if (xresult=="KS" || xresult=="QS" || xresult == "JS" || xresult == "KC" || xresult == "QC" || xresult == "JC")
        {
            result.color = Color.black;
        }
        if (xresult == "KH" || xresult == "QH" || xresult == "JH" || xresult == "KD" || xresult == "QD" || xresult == "JD")
        {
            result.color = Color.red;
        }
    }
}
