using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class PlayerTest : NetworkBehaviour
{
    [SyncVar (hook = nameof(onHelloCountChange))]
    int helloCount = 0;

    [SyncVar (hook = nameof(UpdateName))]
    string nameSync;

    string name;
    public TMP_Text Field;
    public TMP_Text Field2;
    public GameObject CamFollow;
    [SerializeField] float rotateSpeed = 60.0f;
    TMP_Text inField;
    CharacterController controller;
    GetSetName GetNameScript;
    [SerializeField] private Animator animatorComp;
    // Start is called before the first frame update


    public override void OnStartLocalPlayer()
    {
       GameObject scriptObj = GameObject.Find("MyEventsManager");
        GetNameScript = scriptObj.GetComponent<GetSetName>();
        //inField = GetComponent<TMP_Text>();
        name = GetNameScript.GetName();
        Debug.Log("name: " + name);
        //if(isLocalPlayer)
        //{
           
        //}
        CmdChangeName(name);
        //Field.text = name;
        Debug.Log("boom player spawned");
        //Camera.main.transform.SetParent(transform);
        Camera.main.transform.SetParent(CamFollow.transform);
        Camera.main.transform.localPosition = new Vector3(0,3, 8.5f);
        Camera.main.transform.LookAt(transform.position);
    }

    [Command]
    private void CmdChangeName(string nme)
    {
        nameSync = nme;

        
    }
    
     void UpdateName(string oldName, string newName)
    {
        Debug.Log("Changed: " + newName);
        Field.text = newName;
    }
    void Start()
    {
        
       Debug.Log(isOwned);
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Look();
        if(isLocalPlayer == true && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("sending cmd to server");
            cmdHello();
        }
    }

    [Command]
    void cmdHello()
    {
        helloCount += 1;
        Debug.Log("Recieved hello from client");
    }

   
    void HandleMovement()
    {
        if(isLocalPlayer == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            //Vector3 movement = new Vector3 ( moveHorizontal, 0f,  moveVertical);
            Vector3 movement = new Vector3(0f,0f,  moveVertical );
            controller.Move(movement * Time.deltaTime * 15f);
            //transform.position += movement*Time.deltaTime *20f;
            Debug.Log(movement);
            //if (moveVertical != 0f)
            //{
            //    animatorComp.SetBool("isWalking", true);
            //}
            //else
            //{
            //    animatorComp.SetBool("isWalking", false);
            //}
        }
    }

    void Look()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 rotate = new Vector3(0, moveHorizontal * rotateSpeed, 0f);
        transform.eulerAngles = transform.eulerAngles + rotate * Time.deltaTime;
        //if (moveHorizontal != 0f && animatorComp.GetBool("isWalking") == false)
        //{
        //    animatorComp.SetBool("rightTurn", true);
        //}
        //else
        //{
        //    animatorComp.SetBool("rightTurn", false);
        //}
    }
    void onHelloCountChange(int oldCount, int newCount)
        {
        Debug.Log("old Count: " + oldCount + " new Count: " + newCount);
        }
}
