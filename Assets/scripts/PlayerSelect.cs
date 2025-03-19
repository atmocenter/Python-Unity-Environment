using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Mirror;
public class PlayerSelect :MonoBehaviour
{ 
    [SerializeField] private GameObject[] AvatarSelectedBorder;
    [SerializeField] private GameObject[] AvatarImages;
    [SerializeField] private GameObject[] Avatars;
    [SerializeField] private GameObject joystickMobile;
    [SerializeField] private AutoStartClient AutoStartScript;
    [SerializeField] private TokenGifter TokenGifterScript;
    [SerializeField] private GameObject TokenMenuObj;
    [SerializeField] private GameObject ObjInstBtnUI;
    int nDemo = 1;
    private int selectedAvatar;
    public ChangeDefaultPlayer DefaultPlayerScript;
    public GetSetName nameScript;
    public TMP_InputField inField;
    public GameObject menuBtns;
    public MyNetworkManager myNetworkManagerScript;

    // Start is called before the first frame update

    //public override void OnStartLocalPlayer()
    //{
    //    base.OnStartLocalPlayer();
    //}

   
    public void  AvatarSelectedClicked(int index)
    {
        onSelectChange(index);
        Debug.Log(index);
    }

    void onSelectChange(int index)
    {
        //check if index has changed by looking at avatar border active
        if (AvatarSelectedBorder[index].activeSelf != true)
        { 
            //new border
            AvatarSelectedBorder[index].SetActive(true);
            //past border
            AvatarSelectedBorder[selectedAvatar].SetActive(false);
           
            selectedAvatar = index;
        }
    }

    // enterWorldClicked() //
    // when avatar is selected and enter button is pressed on avatar selection window //
    // queues avatar to be spawned //

    public void enterWorldClicked()
    {
        //string field = inField.text;
        //nameScript.AddName(field);
       
        if (NetworkClient.isConnected == false)
        {
            myNetworkManagerScript.StartClient();
            Debug.Log("connection: "+NetworkClient.isConnected);
            GameObject selectedprefab = Avatars[selectedAvatar];
            DefaultPlayerScript.RunCmdChangePlayer(selectedAvatar);
            gameObject.SetActive(false);
            joystickMobile.SetActive(true);
            ObjInstBtnUI.SetActive(true);
            gameObject.SetActive(false);
            ObjInstBtnUI.SetActive(true);
            menuBtns.SetActive(true);

            //needed to toggle tokenmenu, but currently not using a menu
            //if (nDemo == 0)
            //{
            //    TokenMenuObj.SetActive(true);
            //}
        }
        else
        {
            GameObject selectedprefab = Avatars[selectedAvatar];
            DefaultPlayerScript.RunCmdChangePlayer(selectedAvatar);
            gameObject.SetActive(false);
            joystickMobile.SetActive(true);
            ObjInstBtnUI.SetActive(true);
            gameObject.SetActive(false);
            ObjInstBtnUI.SetActive(true);
            menuBtns.SetActive(true);

            //needed to toggle tokenmenu, but currently not using a menu
            //if (nDemo == 0)
            //{
            //    TokenMenuObj.SetActive(true);
            //}

        }



        //CmdAvatarInstantiate(selectedprefab);
        //spawn avatar // remove canvas //
        //gameObject.SetActive(false);
        //ObjInstBtnUI.SetActive(true);
        //command recieve token
        //TokenGifterScript.CallTokenRequestCmd();
        //menuBtns.SetActive(true);


        //if (nDemo == 0)
        //{
        //    TokenMenuObj.SetActive(true);
        //}
        
        //check if mobile
        if (AutoStartScript.isMobiledev()== true)
        {
            //joystickMobile.SetActive(true);

        }



        
    }

    // most likely not needed
    //public void isMobileKeyboard()
    //{
    //    Debug.Log("isMobile");
    //    if (AutoStartScript.isMobiledev() == true)
    //    {
    //        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            
    //    }
    //}
    //[Command]
    //private void CmdAvatarInstantiate(GameObject avi)
    //{
    //GameObject selectedprefab = Instantiate(avi);
    //NetworkServer.AddPlayerForConnection(connectionToClient, selectedprefab);
    //}

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
