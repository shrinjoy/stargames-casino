using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userManager : MonoBehaviour
{
    [SerializeField]public userData data;
    public void setUserData(string id, string name, string password, string macid, string xcommpoint)
    {
        userData xdata = new userData();
        xdata.id = id;
        xdata.name = name;
        xdata.password = password;
        xdata.macid = macid;
        xdata.commpoint = xcommpoint;
        data = xdata;
    }
    private void Start()
    {
        userData udi = new userData();
        udi.id = "1517001";
        udi.password = "12345";
        udi.name = "testuser";
        udi.macid= "12345";
        data = udi;
    }
    
    public userData getUserData()
    {
        return data;
    }
}
[System.Serializable] 
public struct userData
{
    public string id;
    public string name;
    public string password;
    public string macid;
    public string commpoint;
}