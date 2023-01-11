using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_setter : MonoBehaviour
{
    public GameObject panel_owner;
    [SerializeField]panel_manager pm;
    public void setpanel()
    {
     pm.targetpanel= panel_owner;
     pm.enablepanel();
    }
}
