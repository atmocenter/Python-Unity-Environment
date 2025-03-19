using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeatping : NetworkBehaviour
{
    // Start is called before the first frame update
    private float pingInterval = 10f;
    private float timeSince = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if(isClient)
        {
            if (timeSince >= pingInterval)
            {
                //Debug.Log("10 sec ping sent");
                CmdHeartBeat();
                timeSince = 0f;
            }
            timeSince += Time.deltaTime;
        }
        

        
        
    }

    [Command(requiresAuthority = false)]
    void CmdHeartBeat()
    {
        //Debug.Log("ping recieved by server");
    }


}
