using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class LeaderBoard : NetworkBehaviour
{

    public GameObject UICanvas;
    public GameObject UIRank;
    public TMP_Text rankTextUICanvas;
    public TMP_Text UIRankText;

    public readonly SyncList<int> Topranks = new SyncList<int>();
     
    [SyncVar]
    public int latestRank = 0;

    public int userRank =0;

    public void CmdRankUpdateRun()
    {
        CmdRankUpdate();
    }

    [Command(requiresAuthority = false)]
    private void CmdRankUpdate(NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        NetworkConnection cli = NetworkServer.connections[netId.connectionId];
        Debug.Log("count of rank: " + Topranks.Count);
        Topranks.Add(latestRank + 1);
        latestRank += 1;
        RpcGetRank(cli,latestRank);



    }

    [TargetRpc]
    private void RpcGetRank(NetworkConnection target, int rank)
    {
        userRank = rank;
        Debug.Log("this is your rank: "+ userRank);
        rankTextUICanvas.text = "Congrats You Ranked # " + rank.ToString();
        UICanvas.SetActive(true);
        UIRankText.text = "Rank: "+ rank.ToString();
        //show rank top right
        // activate rank screen

    }

    public void CloseRankUI()
    {
        UICanvas.SetActive(false);
        UIRank.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
