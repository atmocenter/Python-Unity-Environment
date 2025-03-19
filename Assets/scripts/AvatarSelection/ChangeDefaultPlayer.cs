using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class ChangeDefaultPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject[] Avatars;
    [SerializeField] private GameObject InvFolder;
    ObjCreator creatorScript;
   
    static int selectedint = -1;

    // RunCmdChangePlayer() //
    // when avatar is selected the index is set to selectedint //
    public void RunCmdChangePlayer(int index)
    {
            selectedint = index;     
    }


    // CmdChangePlayer //
    // when avatar is selected on avatar selection window //
    // this function sets the avatar model as the user's avatar //
    [Command]
    private void CmdChangePlayer( int index, NetworkConnectionToClient sender = null)
    {
        
        int connID = sender.connectionId;
        GameObject selectedprefab = Instantiate(Avatars[index]);
        NetworkServer.Spawn(selectedprefab);
        GameObject oldPlayerObject = gameObject;
        NetworkServer.Destroy(oldPlayerObject);
        selectedprefab.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        NetworkServer.ReplacePlayerForConnection(connectionToClient, selectedprefab);
       
        // not needed currently //
        if(creatorScript != null)
        {
            Debug.Log("creator run Obj");
            creatorScript.runAddObj(connID);
        }
    }

 
    void Start()
    {
      
    }


    void Update()
    {

        // checks if avatar has been selected //
        if(selectedint > -1 && isLocalPlayer == true)
        {
           CmdChangePlayer(selectedint);
            selectedint = -1;
        }
    }
}
