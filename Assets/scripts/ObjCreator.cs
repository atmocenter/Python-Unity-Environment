using Mirror;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class ObjCreator : NetworkBehaviour
{

   public class userObjects
    {
        public SyncList<string> ObjectNames = new SyncList<string>();
    }

     
    //track objects via Key: name and value: connID
    //search via object name check if connectionID is related to that particular key
    public readonly SyncDictionary<string, List<string>> objNames = new SyncDictionary<string, List<string> >();

    public readonly SyncList<string> ObjName_List = new SyncList<string>();

   
    int Toggle = 0;
    int addCount = 0;
    //syncVarDictionary...
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checkDictionary();
    }

    //remove items from SyncDictionary


    public void addingObjWBtn()
    {
       

       
    }

    // add items to syncDictionary

    //add objects 
    //check if obj exists, check if it is assigned to userID value
    //if so don't add
    //otherwise add
    //if new obj name add
    [Command(requiresAuthority = false)]
    void addObject(int connID, string objectName, NetworkConnectionToClient sender = null)
    {
       string ObjNameuserID = objectName+"_" +connID.ToString();
        if (ObjName_List.Contains(ObjNameuserID))
        {
            //
            return;
            //bool check = objNames[objectName].Contains(objectName);
            //if (check == true)
            //{
            //    //return false;
            //}
            //else
            //{
            //    objNames[connID.ToString()].Add(objectName);
            //    checkDictionaryItems();
            //    //return true;
            //}
        }
        else
        {
            objNames[connID.ToString()] = new List<string>();
            objNames[connID.ToString()].Add(objectName);
            checkDictionaryItems() ;
            //return true;
        }
        
    }

    [ClientRpc]
    public void AddItemToList (string ConnID, string name, int option)
    {
        if(option == 0)
        {
            objNames[ConnID].Add(name);
        }
        if(option == 1)
        {
            objNames[ConnID.ToString()] = new List<string>();
            objNames[ConnID.ToString()].Add(name);
        }
    }
    //[Command(requiresAuthority = false)]
    [ClientRpc]
    public void checkDictionaryItems()
    {
        Debug.Log("dictionary print out: ");
        foreach (string key in objNames.Keys) 
        { 
            Debug.Log("key: "+key);
            Debug.Log("count: " + objNames[key].Count);
            foreach (string value in objNames[key])
            { 
                {
                    Debug.Log(value);
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void AddObj(NetworkConnectionToClient sender = null)
    {
        addObjOnClient(sender.connectionId);
    }

    [Server]
    public void runAddObj(int connID)
    {
        addObjOnClient(connID);
    }

    // addObjOnClient() //
    // purpose was to set up a gameobject which all objects spawned by user would be a child to //
    [ClientRpc]
    void addObjOnClient(int ConnID)
    {
        string objName = "Conn_" + ConnID.ToString();
        GameObject obj = new GameObject(objName);
        Debug.Log("rpc client add folder");
        Debug.Log(objName);
        GameObject invF = GameObject.Find("Inventory_Folders");
        if (invF != null)
        {
            obj.transform.SetParent(invF.transform);
        }
        //objNames.Add(objName, new SyncList<string>());
        Toggle = 1;
    }
   

   
}
