using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sound : MonoBehaviour
{
    [SerializeField] Sprite soundonicon;
    [SerializeField] Sprite soundofficon;
    [SerializeField] bool soundon = true;

    private void Awake()
    {
        foreach (AudioSource al in GameObject.FindObjectsOfType<AudioSource>())
        {
            al.playOnAwake =false;
        }
    }

    public void soundonoff_toggle()
    {
        if (soundon == true)
        {
            soundon = false;
            this.GetComponent<Image>().sprite = soundofficon;
            foreach (AudioSource al in GameObject.FindObjectsOfType<AudioSource>())
            {
                al.enabled = soundon;
            }
        }
        else if (soundon == false)
        {
            soundon = true;
            this.GetComponent<Image>().sprite = soundonicon;
            foreach (AudioSource al in GameObject.FindObjectsOfType<AudioSource>())
            {
                {
                    al.enabled = soundon;
                }
            }
            //
        }

    }
}
