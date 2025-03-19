using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Runtime.InteropServices;
using TMPro;
//using Unity.VisualScripting;

public class AutoStartClient : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL 
    [DllImport("__Internal")]
    private static extern bool isMobile();

    [DllImport("__Internal")]
    private static extern void Detailsch(); 
    
#endif

   

    public bool isMobiledev()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
return isMobile();
#endif
        return false;
    }

    public void detailsDevice()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
 Detailsch();
#endif
        
            }
   [SerializeField] NetworkManager myNetworkManager;
    [SerializeField] TMP_Text loginTxt;
    // Start is called before the first frame update
    void Start()

        
    {

        

        if (!Application.isBatchMode)
        {
           
            myNetworkManager.StartClient();
            
            //not needed since we want to auto start each time, no matter the deployed



        }
    }

 
}
