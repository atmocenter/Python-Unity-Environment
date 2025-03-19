using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reconnection : MonoBehaviour
{
    [SerializeField] NetworkManager myNetworkManager;
    [SerializeField] MyNetworkManager ManagerScript;
    //[SerializeField] GameObject ManagerScriptObj;
    bool connection = false;
    bool ReconClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        //ManagerScript = GetComponent<MyNetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //connection = ManagerScript.GetConnectionStatus();
        //if (connection == true && ReconClicked == true) 
        //{
        //    gameObject.SetActive(false);
            
        //}
    }

    public void ReconnectBtnPressed()
    {
        
        myNetworkManager.StartClient();
        //ReconClicked = true;
        //myNetworkManager.StartClient();
    }
}
