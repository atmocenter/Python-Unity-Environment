using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSetName : MonoBehaviour
{
    static private string myName ="";
    static private string uniID;
    // Start is called before the first frame update
   
    public string GetName()
    {
        return myName;
    }

    public void AddName(string name)
    {
        myName = name;
    }

    public void setUniID(string uniqueID)
    {
        uniID = uniqueID;
    }

    public string GetUniID()
    {
        return uniID;
    }
}
