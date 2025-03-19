using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

using System;


public class TokenHandler : NetworkBehaviour
{

    // ------------------//
    //about Vars
    //if value is 1 than token is avaialabe 
    // change value by giving token index with its status 0/1

    // token array holds all token values


    //token index = -1 than no token is selected

  

    private string[] tokens = { " " };
 
    //[SyncVar]
    //private bool[] availableTokens = { true, true, true, true, true };
    private string myToken = "";
    //private readonly SyncList<int> availableTokens = new SyncList<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } ;
    private readonly SyncList<int> availableTokens = new SyncList<int>() { 1, 1, 1};
    private readonly SyncList<int> availableTokensConn = new SyncList<int>() { -1, -1, -1 };
    //private readonly SyncList<int> availableTokensConn = new SyncList<int>() { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    public TokenClicked[] tokenListItems;
    [SerializeField] private GameObject TokenMenu;
    [SerializeField] private httpRequest httpRequestScript;


    public int tokenIndex = -1;



    //command to change availabilty
    //sync hook to make changes to list
    //update available tokens with new availability for token at index


   
    public void setOwnership ()
    {
      
    }

    [Command(requiresAuthority = false)]
    private void tokenSelectedRefresh( int availability, int conn)
    {
       
        int index = availableTokensConn.IndexOf(conn);
        
        if(index == -1)
        {
            return;
        }
        availableTokens[index] = availability;
       
        if (availability == 1)
        {
            availableTokensConn[index] = -1;
        }

        RpcUpdateTokenText(index);
        Debug.Log("availible tokens: ");
        for (int i = 0; i < availableTokens.Count; i++)
        {
            Debug.Log(availableTokens[i]);
        }
    }

    public void TokenRefreshCallCmd(int availability, int conn)
    {
        tokenSelectedRefresh(availability, conn);
    }


    public void availTokens()
    {
        //Camera.main.transform
    }
    // rpc -> changed toekns from list -> update color and change availability



    public void TokenUpdatedCallCmd(int index, int availability)
    {
        if(availability == 0 )
        {
            //lets handle this on the server using a rpctarget
            //tokenIndex = index;
            //myToken = tokens[tokenIndex];
        }
        if (availability == 1 )
        {
            myToken = "";
            tokenIndex = -1;
        }
        tokenSelectedUpdate(index, availability);
    }

    [ClientRpc]
    public void RpcUpdateTokenText(int index)
    {
        Debug.Log(TokenMenu);
        if ( TokenMenu.activeSelf == true )
        {
           

            //playerScript.testingnetClient();
            tokenListItems[index].AvailabilityUpdate(availableTokens[index]);
            tokenListItems[index].ChangeColor();
        }
        else
        {
            //Debug.Log("token active: " + TokenMenu.activeSelf);
        }

        
    }

    [Command(requiresAuthority = false)]
    private void tokenSelectedUpdate(int index, int availability, NetworkConnectionToClient sender = null)
    {

        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        NetworkConnection cli = NetworkServer.connections[netId.connectionId];
        
        NetworkConnection g = netId;
        //check availability
        //if avaialable i can change it

        if (availableTokens[index] == 1 )
        {
            if(availability== 0)
            {
                availableTokens[index] = availability;
                availableTokensConn[index] = netId.connectionId;
                RpcUpdateTokenText(index);
                UpdateTokentoPlayer(cli,index);
                return;

            }
            else
            {
                
                return;
            }
        }
     

        if (availableTokens[index] == 0 )
        {
            if(availability== 1)
            {
                availableTokens[index] = availability;
                availableTokensConn[index] = -1;
                RpcUpdateTokenText(index);
                UpdateTokentoPlayer(cli, index);
                return; 

            } 
            else
            {
                Debug.Log("its actually not available");
                return; 
            } 
        }
        
        //availableTokens[index] = availability;
        //if (availability == 0)
        //{
        //    availableTokensConn[index] = netId.connectionId;
        //}
        //if (availability == 1)
        //{
        //    availableTokensConn[index] = -1;
        //}

        //RpcUpdateTokenText(index);
        //UpdateTokentoPlayer(cli);
        //UpdateTokentoPlayer();

    }

    [TargetRpc]
    //NetworkConnectionToClient target
    public void UpdateTokentoPlayer( NetworkConnection target, int index)
    {
        myToken = tokens[index];
        Debug.Log("myToken: " + myToken);
        TokenMenu.SetActive(false);
        httpRequestScript.GetTokenFromHandler(myToken);
    }


    public int getAvailabilityVal(int index)
    {
        Debug.Log("Availability: " + availableTokens[index]);
        return availableTokens[index];
    }

    [Command(requiresAuthority = false)]
    public void CmdTokenRelease(int index, NetworkConnectionToClient sender = null)
    {
        var netId = sender.identity.connectionToClient;
        //NetworkConnection x = sender.connectionId;
        foreach (NetworkConnection conn in NetworkServer.connections.Values )
        {
            
            availableTokens[index] = 1;
            if (conn != netId)
            {
                Debug.Log("conn loop: s"+ conn);
                RpcReleaseTokenOnQuit(conn, index);
            }
        }
    }

    [TargetRpc]
    public void RpcReleaseTokenOnQuit(NetworkConnection target, int index)
    {
        tokenListItems[index].AvailabilityUpdate(availableTokens[index]);
        tokenListItems[index].ChangeColor();
    }
   



    [Server]
    public void testAttempt( int conn, int availability)
    {
        Debug.Log("testing token handler running on server");

        int index = availableTokensConn.IndexOf(conn);

        if (index == -1)
        {
            Debug.Log("no Index");
            return;
        }
        availableTokens[index] = availability;

        if (availability == 1)
        {
            availableTokensConn[index] = -1;
        }

        RpcUpdateTokenText(index);
        Debug.Log("availible tokens: ");
        for (int i = 0; i < availableTokens.Count; i++)
        {
            Debug.Log(availableTokens[i]);
        }




    }

    [Server]
    public void testAttemptLastClient(int conn, int availability)
    {
        Debug.Log("testing token handler running on server");

        int index = availableTokensConn.IndexOf(conn);

        if (index == -1)
        {
            Debug.Log("no Index");
            return;
        }
        availableTokens[index] = availability;

        if (availability == 1)
        {
            availableTokensConn[index] = -1;
        }
    }



    public void newClientUpdateTokens()
    {
        Debug.Log("Connected to Server Token Handler boom");
        int index = 0;
        foreach (var item in availableTokens)
        {
            
            tokenListItems[index].AvailabilityUpdate(availableTokens[index]);
            tokenListItems[index].ChangeColor();
            index = index + 1;
        }
            
    }

    private void Start()
    {
        Debug.Log("tokens #: " +tokens.Length);
    }





}
