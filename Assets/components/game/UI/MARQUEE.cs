using Unity.VisualScripting;
using UnityEngine;

public class MARQUEE : MonoBehaviour
{
    Vector3 startpositon;
    Vector3 finalpos;
    public float speed;
    private void Start()
    {
        startpositon = this.GetComponent<RectTransform>().position;
        finalpos = (startpositon + new Vector3(450.0f, 0.0f, 0.0f));
    }
    private void Update()
    {
        this.GetComponent<RectTransform>().position = Vector3.Lerp(this.GetComponent<RectTransform>().position, finalpos,Time.deltaTime*speed);
     
        if (Vector3.Distance(this.GetComponent<RectTransform>().position, finalpos)<10.0f)
        {
            this.GetComponent<RectTransform>().position = startpositon;
        }
    }
}