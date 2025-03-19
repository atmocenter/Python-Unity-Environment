using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class PrintCubeCPy : NetworkBehaviour


{
    public static GameObject plane;
    public static GameObject PrimCube;
    public GameObject PrimSphere;
    private int ConnectionID = 0;
    public RunThePython CompActivityObj;
    public static GameObject Inventory;
    static int toggle = 0;
   
  
 
    public static Vector3 getPlayerData(int connID)
    {
        NetworkConnection cli = NetworkServer.connections[connID];
        var ply = cli.identity;
        Debug.Log(ply.transform.position);
        return ply.transform.position;
    }

    //moveObject with Forces
    [Server]
    public static string addForce(string name, int ConnID, float x, float y, float z)
    {
        string err = "";
        try
        {
            string name_Id = name+"_"+ConnID.ToString();
            Debug.Log(name_Id);

            GameObject CubeObj = GameObject.Find(name_Id);
            if(CubeObj == null)
            {
                Debug.Log("err message cube obj == null");
                return "The given name was not found or the userID was not the user's   ";
            }
            else
            {
                Debug.Log(CubeObj.name);
            }
            
            //PrimScript CObj = CubeObj.GetComponent<PrimScript>();
            Rigidbody CubeRb = CubeObj.GetComponent<Rigidbody>();
            CubeRb.AddForce(x, z, y, ForceMode.Impulse);

        }
        catch (Exception e) 
        {
            err = e.Message;
            return err;
        }
        return err;
    }
 
    public static string removeFromListFunc(string name, int ConnID)
    {
        
        string err = "";
        try
        {
            NetworkConnection connectionID = NetworkServer.connections[ConnID];
            if (connectionID != null)
            {
                Debug.Log("good Conn ID " + ConnID);
                GameObject compObj = GameObject.Find("Computer_Activity");
                if (compObj != null)
                {
                    Debug.Log("Comp Activity Found ");
                    RunThePython RunPyScript = compObj.GetComponent<RunThePython>();
                    RunPyScript.RemoveFromListAfterDeletion(connectionID, name);
                }
            }
            else
            {
                Debug.Log("null connection ID");
            }
            //find gameobject
            //delete object
            
           

        }
        catch (Exception e)
        {
            err = e.Message + "\n" + e.StackTrace;
            return err;
        }
        return err;

    }

    public static string delObjectFromUI(string name, int ConnID)
    {
        string err = "";
        try
        {
            //find gameobject
            //delete object
            string name_Id = name + "_" + ConnID.ToString();
            GameObject CubeObj = GameObject.Find(name_Id);
            if (CubeObj != null)
            {
                Debug.Log("was obj found?");
                Debug.Log(CubeObj.name);
                Destroy(CubeObj);


            }
            else
            {
                Debug.Log("game obj was null");
            }

        }
        catch (Exception e)
        {
            err = e.Message + "\n" + e.StackTrace;
            return err;
        }
        return err;
    }

    public static string  delObject(string name, int ConnID)
    {
        string err = "";
        try
        {
            //find gameobject
            //delete object
            string name_Id = name + "_" + ConnID.ToString();
            GameObject CubeObj = GameObject.Find(name_Id);
            if (CubeObj != null)
            {
                Debug.Log("was obj found?");
                Debug.Log(CubeObj.name);
                Destroy(CubeObj);
                removeFromListFunc(name,ConnID);


            }
            else
            {
                Debug.Log("game obj was null");
            }

        }
        catch (Exception e)
        {
            err = e.Message + "\n" + e.StackTrace;
            return err;
        }
        return err;
    }
    


    //[TargetRpc]
    //private void removeFromList(NetworkConnection target, string name, int ConnID)
    //{
    //    GameObject InvParent = GameObject.Find("InventoryList_Parent");
    //    if (InvParent != null)
    //    {
    //       IventoryList InventoryListScript = InvParent.GetComponent<IventoryList>();
    //        if (InventoryListScript != null)
    //        {       
    //            Debug.Log("InvListScript is null new ConnID = " + ConnID);
               
    //        }
    //    }
    //}


    // moving object
    public static string moveObject(string name, int ConnID, float x, float y, float z)
    {
        string err = "";
        try
        {
            
            
            string name_Id = name + "_" + ConnID.ToString();
            GameObject CubeObj = GameObject.Find(name_Id);
            if (CubeObj != null)
            {
              
            Debug.Log(CubeObj.name);
            
            Rigidbody CubeRb = CubeObj.GetComponent<Rigidbody>();
            Vector3 PosCubeObj = CubeObj.transform.position;
            Vector3 movePos = new Vector3(x, z, y);
            CubeRb.MovePosition(PosCubeObj + movePos);

            }
            else
            {
                Debug.Log("game obj was null");
            }
            

        }
        catch (Exception e)
        {
            err = e.Message + "\n" + e.StackTrace;
            return err;
        }
        return err;
        

    }


    //print cube near user
    
    [Server]
    public static string printThatCube(string name, int ConnID)
    {
        string err = "";
        try
        {

            NetworkServer.connections.TryGetValue(ConnID, out var conn);
            if(conn == null)
            {
                return "this user ID doesn't exist";
            }
            NetworkConnection cli = NetworkServer.connections[ConnID];
            NetworkIdentity plyrID= cli.identity;

            string strObjName_ConnID = name+"_"+ConnID.ToString();

            //check if object name exists already
            GameObject checkObj = GameObject.Find(strObjName_ConnID);
            if (checkObj != null) 
            {
                return "object by this name already exists, please create a new name";
            }


            Vector3 PosInFront = plyrID.transform.position + (plyrID.transform.forward * 3);
 
            PrimCube = InstanPrefabs.switchObj();

            GameObject prim = Instantiate(PrimCube, PosInFront, Quaternion.identity);

        
            prim.name = strObjName_ConnID;
            NetworkServer.Spawn(prim);
            
            PrimScript pS = prim.GetComponent<PrimScript>();

            GameObject parentObj = GameObject.Find(ConnID + "_ObjParent");
            if (parentObj != null)
            {
                prim.transform.parent = parentObj.transform;
            }
        pS.updateNamefrmServer(strObjName_ConnID,toggle);
            if(toggle ==0)
            {
                toggle = 1;
            }
            else {
                toggle = 0;
            }
            // add item to inventory list via target rpc
            pS.TargetAddToInventory(cli,name, ConnID);

             



        }
        catch (Exception  e)
        {
            Debug.Log(e.Message + "\n" + e.StackTrace);
           err = e.ToString();
            return err;

        }
        return err;
        
         
    }
    public static string changeCubeColor(string objName,int ConnID, float r, float g, float b)
    {
        string err = "";
        try
        {

            string name_Id = objName + "_" + ConnID.ToString();
            Debug.Log("change cube color server: "+objName);
          
            Debug.Log("rpcchange color: " + name_Id);
            GameObject CubeObj = GameObject.Find(name_Id);
            if (CubeObj == null) 
            {
                return "object by this name: " + name_Id + " does not exist";
            }
            PrimScript CObj = CubeObj.GetComponent<PrimScript>();
            CObj.rpcChangeColor(name_Id, r, g, b);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
            err = e.ToString();
            return err;
        }
        return err;
       

    }

    


    public static string RotateObject(string name, int ConnID, float x, float y, float z)
    {
        string err = "";
        try
        {
            string name_Id = name + "_" + ConnID.ToString();
            //rpcRotateObject(name, x, y, z);
            Debug.Log("rpc rotation: " + name_Id);
            GameObject CubeObj = GameObject.Find(name_Id);
            if (CubeObj == null)
            {
                return "object by this name: " + name_Id + " does not exist";
            }
            PrimScript CObj = CubeObj.GetComponent<PrimScript>();
            CObj.SrvrRotateObject( x, y, z);

        }
        catch (Exception e ) 
        {
            Debug.Log(e.ToString());
            err = e.ToString();
            return err;
        }
        return err;
        
    }

 
    [ClientRpc]
     void rpcChangeObjName(string name, GameObject obj)
    {
        obj.name = name;
    }

    [TargetRpc]
    public  void TargetAddToInventory(NetworkConnection target)
    {
        //get inventory item
        //get component
        //run func in script to 
        

    }
}
