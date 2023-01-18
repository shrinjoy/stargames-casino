using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] datapanels;
    public GameObject targetpanel;
  
    public void enablepanel()
    {
        print("enabled a panel");
       
        foreach (GameObject gb in datapanels)
        {
            if (gb != targetpanel)
            {
                gb.SetActive(false);
                print("turned off panel");
            }
            else if(gb == targetpanel)
            {
                this.GetComponentInParent<AudioSource>().Play();
                gb.SetActive(true);
            }
        }
        
    }
    
}
