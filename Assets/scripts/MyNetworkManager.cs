using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Threading;
using System.Security.AccessControl;

public class MyNetworkManager : NetworkManager
{

    private string uniqueID = "";
    private string tokenID = "";
    private string attemptID = "";
    private bool QuizInProgress = false;
    private bool ConnectionStatus = false;
    private Camera newMainCamera;
    [SerializeField] GameObject CameraPrefab;
    //public TokenHandler TokenHandlerScript;
    [SerializeField] private QdataSync QdataSyncScript;
    [SerializeField] private GameObject ReconnScreen;
    [SerializeField] private GameObject PlayerSelectScreen;
    [SerializeField] private GameObject disconnectCanvas;
    [SerializeField] private IventoryList InventoryScript;
    [SerializeField] private GameObject toggleButtons;
    [SerializeField] private GameObject codeEditor;
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject LoginUI;

    //[SerializeField] private GameObject tknMenu;
    //dictionary 
    //public GameObject MenuPrefab;


    //public static bool hostSelected = false;
    public override void OnStartServer()
    {

        //hostSelected=true;
        
        Debug.Log("server has started fam");
        //RunThePython.SetupPy();
        //RunThePython.SetupPy2();
       
        base.OnStartServer();   
    }

  
    
    public override void OnStopServer()
    {
        Debug.Log("server has stopped fam");
        
    }

 
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log("server has been connected to fam");
        Debug.Log("boom connection");
       
       
        base.OnServerConnect(conn);
        Debug.Log(NetworkServer.connections.Values.Count);
        Debug.Log("conection ID: " + conn.connectionId);
        //GameObject menuObject = Instantiate(MenuPrefab);
        //NetworkServer.Spawn(menuObject);
        //menuObject.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        //create obj parent of all their objs
        GameObject newObj = new GameObject(conn.connectionId + "_ObjParent");



    }



    public override void OnClientDisconnect()
    {
        ConnectionStatus = false;
        //camera should be prefab
        Debug.Log("client is disconnected from server, now what");
        GameObject cam = GameObject.Find("Main Camera");
        if(cam != null)
        {
            Destroy(cam);
        }
        GameObject camera  = GameObject.Instantiate(CameraPrefab);
        camera.name = "Main Camera";
        //newMainCamera = new GameObject("Main Camera").AddComponent<Camera>();
        //newMainCamera = Camera
        //newMainCamera.tag = "MainCamera";
        //disconnectCanvas.worldCamera = newMainCamera;
        //if(tknMenu.activeSelf == true)
        //{
        //    tknMenu.SetActive(false);
        //}
        ReconnScreen.SetActive(true);
        PlayerSelectScreen.SetActive(true);

        InventoryScript.RemoveItemsOnList();
        InventoryScript.DeactivateObj();
        //deactivate other ui joystick, code editor, buttons
        toggleButtons.SetActive(false);
        codeEditor.SetActive(false);
        joyStick.SetActive(false);
        //LoginUI.SetActive(true);
        instructions.SetActive(false);
        base.OnClientDisconnect();
    }
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { 
        
        CheckQuizAttempt(conn);
        Debug.Log("a client has disconnected");
        int ConnID = conn.connectionId;

        //not needed, since scripts are all run in containers //
        //if (RunThePython.userScripts.ContainsKey(ConnID.ToString()) == true)
        //    {
        //    CancellationTokenSource cts = RunThePython.userScripts[ConnID.ToString()];
        //    cts.Cancel();

        //    bool success = RunThePython.userScripts.TryRemove(ConnID.ToString(), out CancellationTokenSource val);
        //    if (success == true)
        //    {
        //        Debug.Log(ConnID.ToString() + " Successfully Removed");
        //    }
        //    else
        //    {
        //        Debug.Log(ConnID.ToString() + " not removed");
        //        Debug.Log(RunThePython.userScripts);
        //    }
        //    }
        Player player = conn.identity.GetComponent<Player>();

       
        //Snippet Explanation: Destroy all user created game objects //
        GameObject parentObjItems = GameObject.Find(conn.connectionId + "_ObjParent");
        if(parentObjItems != null)
        {
            Destroy(parentObjItems);
        }



        if (player != null)
        {
            
            NetworkServer.Destroy(player.gameObject);
        }
        base.OnServerDisconnect(conn);

    }


    [Server]
    public void CheckQuizAttempt(NetworkConnection conn)
    {
        int connectId = conn.connectionId;
        QdataSyncScript.QuizAttemptCheck(connectId);
    }



    [Server]
    public void ClientRpcTest(NetworkConnection conn)
    {
        

       
        
            if (NetworkServer.connections.Values.Count > 0) { 
        Debug.Log("testing rpc from disconnect");
        Debug.Log(conn.ToString());
        int connectId = conn.connectionId;

            //TokenHandlerScript.testAttempt(connectId,1);
        Debug.Log("Connect ID: " + connectId);
            //foreach (NetworkConnection connect in NetworkServer.connections.Values)
            //{

            //    Debug.Log("conn loop: " + connect.connectionId);

            //    Player playerConn = connect.identity.GetComponent<Player>();
            //    playerConn.tokenUpdate(connectId);
            //    break;


            //}
        }
        else
        {
            Debug.Log("okay its 0");
            int connectId = conn.connectionId;

            //TokenHandlerScript.testAttempt(connectId, 1);
        }
    }
     public override void OnClientConnect()
    {
        ConnectionStatus = true;
        if(ReconnScreen.activeSelf== true) 
        {
            ReconnScreen.SetActive(false);
            LoginUI.SetActive(true);
        }
        
        base.OnClientConnect();
    }

    [Client]
    public bool GetConnectionStatus()
    {
        return ConnectionStatus;
    }





    //[TargetRpc]
    //public void TargetToUser(NetworkConnectionToClient target)
    //{
    //    foreach (NetworkConnection conn in NetworkServer.connections.Values)
    //    {

    //        Debug.Log("past client target: " + target);
    //        if (conn != target)
    //        {
    //            Debug.Log("conn loop: " + conn);

    //        }
    //    }

    //}



}
