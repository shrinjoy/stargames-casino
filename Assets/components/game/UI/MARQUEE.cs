using Unity.VisualScripting;
using UnityEngine;

public class MARQUEE : MonoBehaviour
{
    Vector3 startpositon;
    Vector3 finalpos;
    private void Start()
    {
        startpositon = this.GetComponent<RectTransform>().position;
        finalpos = (startpositon + new Vector3(500.0f, 0.0f, 0.0f));
    }
    private void Update()
    {
        this.GetComponent<RectTransform>().position = Vector3.Lerp(this.GetComponent<RectTransform>().position, finalpos,Time.deltaTime*2.0f);
     
        if (Vector3.Distance(this.GetComponent<RectTransform>().position, finalpos)<2.0f)
        {
            this.GetComponent<RectTransform>().position = startpositon;
        }
    }
}