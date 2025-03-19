using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamReconnect : MonoBehaviour
{
    Canvas ObjCanvas;
    GameObject newCamera;
    // Start is called before the first frame update
    void Start()
    {
        ObjCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjCanvas != null)
        {
           if ( ObjCanvas.worldCamera == null )
            {
               newCamera = GameObject.Find("Main Camera");
                if ( newCamera != null )
                {
                    ObjCanvas.worldCamera = newCamera.GetComponent<Camera>();
                }
            }

        }
    }
}
