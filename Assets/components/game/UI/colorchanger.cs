using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material material;
    void Start()
    {
        this.GetComponent<Renderer>().material = material;
    }

   
}
