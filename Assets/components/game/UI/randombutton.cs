using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class randombutton : MonoBehaviour
{
    public  int betsplaceable=0;
    List<betButtons> allbuttons;
    public List<int> generatednumbers;
    private void Start()
    {
        generatednumbers=new List<int>();
        allbuttons=new List<betButtons>();  
        allbuttons = GameObject.FindObjectsOfType<betButtons>().ToList();

    }
    public void placerandombets()
    {
        generatednumbers.Clear();
        for(int i=0;i<betsplaceable;i++)
        {
            allbuttons[getrandombutton()].placebet();
        }

    }

    int getrandombutton()
    {
        

        int randomnumber = Random.Range(0, allbuttons.Count);

        if(generatednumbers.Contains(randomnumber) == false)
        {
            generatednumbers.Add(randomnumber);
            return randomnumber;
        }

        return getrandombutton();

    }
}
