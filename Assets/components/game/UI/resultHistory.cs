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
        timertext.text = xtime;
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
    }
}
