using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userManager : MonoBehaviour
{
    [SerializeField] userData data;
    public  void setUserData(string id,string name,string password,string macid)
    {
        userData xdata = new userData();
        xdata.id = id;
        xdata.name = name;
        xdata.password = password;
        xdata.macid = macid;
        data = xdata;
    }
}
[System.Serializable] 
public struct userData
{
    public string id;
    public string name;
    public string password;
    public string macid;

}