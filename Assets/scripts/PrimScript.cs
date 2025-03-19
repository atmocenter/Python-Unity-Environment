using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PrimScript : NetworkBehaviour
{
    GameObject InventoryList;
    IventoryList InvListScript;
    public int Intensity = 2;
    //int toggle = 0;
    //public Material matRef;
    //SyncVar for Color
    //SyncVar for Name
    [SyncVar(hook = nameof(UpdateName))]
    string objName="";

    [SyncVar(hook = nameof(UpdateColor))]
    Color matColor = Color.white;

    GameObject Gobj;
    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("Start func");
        //Gobj = GetComponent<GameObject>(); InventoryList_Parent
        InventoryList = GameObject.Find("InventoryList_Parent");
        if(InventoryList == null)
        {
            Debug.Log("inventory list parent not found");
            return;
        }
        else
        {
            Debug.Log("setup obj");
            InvListScript = InventoryList.GetComponent<IventoryList>();
            if(InvListScript == null) 
            {
                Debug.Log("InvListScript is null");
            }
        }
    }
    [Server]
    void SetupObjColor(int toggle)
    {
        //Renderer ObjRenderer = GetComponent<Renderer>();
        //Material newMat = new Material(ObjRenderer.material);
        //if(toggle == 0)
        //{
        //    newMat.color = Color.blue;
           
        //}
        //else
        //{
        //    newMat.color = Color.red;
           
        //}
        
        //ObjRenderer.material = newMat;
        //Debug.Log("setup Material for : "+gameObject.name);
        //ObjRenderer.material.color = Color.red;


        matColor = Color.green;
    }

    public void CallCmdDeleteObj()
    {
        CmdDeleteObj();
    }
    [Command(requiresAuthority = false)]
    void CmdDeleteObj()
    {
        Destroy(gameObject);
    }

    [ClientRpc]
    void SetupObjColorCL(int toggle)
    {
        Renderer ObjRenderer = GetComponent<Renderer>();
        Material newMat = new Material(ObjRenderer.material);
        if (toggle == 0)
        {
            newMat.color = Color.blue;

        }
        else
        {
            newMat.color = Color.red;

        }

        ObjRenderer.material = newMat;
        Debug.Log("setup Material for : " + gameObject.name);
        //ObjRenderer.material.color = Color.red;
    }
    void UpdateColor(Color oldColor, Color newColor)
    {
        Renderer ObjRenderer = GetComponent<Renderer>();
        Material newMat = new Material(ObjRenderer.material);
        ObjRenderer.material = newMat;
        ObjRenderer.material.color = newColor;
        Debug.Log("Color Changed");
        Debug.Log("Color changed for : " + gameObject.name);
    }

    void UpdateName(string oldName, string newName)
    {
        gameObject.name = newName;
        Debug.Log("name Changed");
        Debug.Log("Name on client: "+gameObject.name);
    }

    [Server]
    public void updateNamefrmServer(string name, int toggle)
    {
        Debug.Log("update name on client");
        if(isServer)
        {
            gameObject.name = name;
        }
        
        //Debug.Log(gameObject.name);
        //rpcupdateName(name);
        objName = name;
        //Debug.Log(name);
        //gameObject.name = name;
        SetupObjColor(toggle);
        //SetupObjColorCL(toggle);
    }
    [ClientRpc]
    void rpcupdateName(string name)
    {
        gameObject.name = name;
    }

    [TargetRpc]
    public void TargetAddToInventory(NetworkConnection target,string name, int connID)
    {
        //get inventory item
        //get component
        //run func in script to 
        // provide connID to script

        if(InvListScript != null)
        {
            Debug.Log("inventoryList script obj is present");
            InvListScript.AddToList(name);
            InvListScript.setConnID(connID);
        }
        else
        {
            Debug.Log("inventoryList script obj is null");
            if(InventoryList == null)
            {
                InventoryList = GameObject.Find("InventoryList_Parent");


            }
            if(InventoryList != null && InvListScript == null)
            {
                InvListScript = InventoryList.GetComponent<IventoryList>();
                if (InvListScript != null)
                {
                    Debug.Log("inventoryList script obj is present");
                    InvListScript.AddToList(name);
                    InvListScript.setConnID(connID);
                }
            }





        }

       


    }
    public void rpcChangeColor(string objName, float r, float g, float b)
    {
       //ChangetheColor(r, g, b);
        Color newColor = new Color(r, g, b); 
        matColor = newColor;

    }
    [ClientRpc]
    public void ChangetheColor(float r, float g, float b)
    {
        Debug.Log("changeColor " + r  + g +b);
        //Renderer rend = GetComponent<Renderer>();
        
        //rend.material.color = newColor;
       
    }




    [Server]
    public void SrvrRotateObject(float x, float y, float z)
    {
        transform.Rotate(x, y, z, Space.World);
        //RotateObject(x, y, z);


    }

    [ClientRpc]
    void RotateObject(float x, float y , float z)
    {
        Debug.Log("rotating obj");
        transform.Rotate(x, y, z,Space.World);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //when obj is clicked in Inventory - highlight obj
    public void itemClickedHighlight()
    {
        Renderer rend = GetComponent<Renderer>();
        Color pstColor = rend.material.color;
       pstColor= pstColor* Intensity;
        Material material = rend.material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", pstColor);
        Debug.Log("running item CLickedHighlight");
    }
    public void itemDeselected()
    {
        Renderer rend = GetComponent<Renderer>();
      
        Material material = rend.material;
        material.DisableKeyword("_EMISSION");
        Debug.Log("item Deselected");
    }



}
