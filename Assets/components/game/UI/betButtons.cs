using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class betButtons : MonoBehaviour,IPointerClickHandler
{
    public GameObject panel;
    public TMPro.TMP_Text text;
    public int betplaced;
    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onMiddle;
    private void Start()
    {
        text = GetComponentInChildren<TMPro.TMP_Text>();
        panel = GetComponentsInChildren<Image>()[1].gameObject;
        panel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        GameObject.FindObjectOfType<jokerGameManager>().winamounttext.text =" ";
=======
        GameObject.FindObjectOfType<jokerGameManager>().winamounttext.text ="Win:0";
>>>>>>> parent of d701e08 (update)
=======
        GameObject.FindObjectOfType<jokerGameManager>().winamounttext.text ="Win:0";
>>>>>>> parent of d701e08 (update)
=======
        GameObject.FindObjectOfType<jokerGameManager>().winamounttext.text ="Win:0";
>>>>>>> parent of d701e08 (update)
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            placebet();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            removebet();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            onMiddle.Invoke();
        }
    }

   public void updatebutton()
    {
        text.text = betplaced.ToString();
    }


    public void placebet()
    {
        this.GetComponentInParent<AudioSource>().Play();
           
            panel.SetActive(true);
            betplaced = betplaced + GameObject.FindObjectOfType<jokerGameManager>().coinselected;
            text.text = betplaced.ToString();
       
     //   GameObject.FindObjectOfType<repeatButton>().addbetbuttondata();




    }
    public void removebet()
    {
        
        betplaced = 0;
        panel.SetActive(false);

     
      

        text.text = betplaced.ToString();
        //GameObject.FindObjectOfType<repeatButton>().addbetbuttondata();
    }
}
