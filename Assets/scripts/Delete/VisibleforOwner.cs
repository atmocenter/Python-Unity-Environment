using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class VisibleforOwner : NetworkBehaviour
{
    [SerializeField]
    private GameObject NameCanvasOwnerOnly;
    // Start is called before the first frame update
    void Start()
    {
       if(isLocalPlayer == true)
        {
            Debug.Log("local player Canvas");
        }
        else
        {
            NameCanvasOwnerOnly.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
