using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Networking;
public class Player : NetworkBehaviour
{
    [SyncVar (hook = nameof(onHelloCountChange))]
    int helloCount = 0;

    [SyncVar (hook = nameof(UpdateName))]
    string nameSync;

    string name;
    public TMP_Text Field;
    public TMP_Text Field2;
    public GameObject CamFollow;
   
    TMP_Text inField;
    CharacterController controller;
    GetSetName GetNameScript;
    [SerializeField] private Animator animatorComp;
    [SerializeField] float rotateSpeed = 70.0f;
    [SerializeField] float camSpeed = 20.0f;
    [SerializeField] float turnSpeed = 20.0f;
    [SerializeField] private GameObject NameOwnerOnlyView;
    [SerializeField] private GameObject NamenonOwnerView;
    [SerializeField] private float speed = 10f;
    private bool mobileDevice;

    private GameObject TokenHandlerObj;
    //public TokenHandler TokenHandlerScript;
    //private TokenGifter TokenGifterScript;
    private GameObject TokenGifter;
    private AutoStartClient autoScript;
    private GameObject AutoStartObj;
    //[SerializeField] private GameObject ff;
    private static FixedJoystick joystick_ = null;
    private FixedJoystick joystick_Cam;
    //public GameObject Tokenbtn;
    float jstkX = 0f;
    float jstkY = 0f;

    float jstkCamX = 0f;
    float jstkCamY = 0f;
   static GameObject  joystickObj;


    private int tokenIndex;
    private string connIDstr = "";
    private string uniqueID = "";
    private string tokenID = "";
    private string attemptID = "";
    private bool QuizInProgress = false;


    // for animation movement func
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingBackHash;
    int isLeftStrafHash;
    int isRightStrafHash;
    int isRightTurnHash;
    int isLeftTurnHash;

    // Start is called before the first frame update

    //[TargetRpc]
    //public void TargetToUser(NetworkConnectionToClient target)
    //{
    //    foreach (NetworkConnection conn in NetworkServer.connections.Values)
    //    {

    //        Debug.Log("past client target: " + target);
    //        if (conn != target)
    //        {
    //            Debug.Log("conn loop: " + conn);

    //        }
    //    }

    //}


    public void setTokenIndex(int index)
    {
        tokenIndex = index;
    }
    public int getTokenIndex() 
    { 
        return tokenIndex; 
    }

    public void testingnetClient ()
    {
        Debug.Log("test can run");
    }
    public void setconnID (string id)
    {
        connIDstr = id;
    }
    public void getconnID()
    {
        Debug.Log("conn id playerScript: "+connIDstr);
    }

    public void setQuizProgress (bool prog)
    {
        QuizInProgress = prog;
    }

    public bool getProgress ()
    {
        return QuizInProgress;
    }
    public void setAttemptID(string id)
    {
        attemptID = id;
    }

    public string getAttemptID()
    {
        return attemptID;
    }

    public void setUniqueID (string id)
    {
        uniqueID = id;
    }

    public string getUniqueID()
    {
        return uniqueID;    
    }
    public void setTokenID (string token)
    {
        tokenID = token;
    }

    public string getTokenID()
    {
        return tokenID;
    }

    public void getPlayerPosition()
    {
        Debug.Log(transform.position.ToString());
    }
    //-0.548 x
    //1.916 y
    //-4.61 z


    public override void OnStartLocalPlayer()
    {
        AutoStartObj = GameObject.Find("AutoStartClient");
        autoScript = AutoStartObj.GetComponent<AutoStartClient>();
        mobileDevice = autoScript.isMobiledev();
       
        TokenGifter = GameObject.Find("TokenGifter");
        //TokenGifterScript = TokenGifter.GetComponent<TokenGifter>();

        //TokenHandlerObj = GameObject.Find("TokenHandlerv1");
        //TokenHandlerScript = TokenHandlerObj.GetComponent<TokenHandler>();
        //TokenHandlerScript.newClientUpdateTokens();

        
       GameObject scriptObj = GameObject.Find("MyEventsManager");

        

        //GetNameScript = scriptObj.GetComponent<GetSetName>();
        //inField = GetComponent<TMP_Text>();
        //name = GetNameScript.GetName();
        //Debug.Log("name: " + name);
        //if(isLocalPlayer)
        //{
           
        //}
        //CmdChangeName(name);
        //Field.text = name;
        Debug.Log("boom player spawned");
        //Camera.main.transform.SetParent(transform);
        Camera.main.transform.SetParent(CamFollow.transform);
        //Camera.main.transform.position= CamFollow.transform.position;
        Camera.main.transform.localPosition = new Vector3(0f,0f, 0f);
        Camera.main.transform.LookAt(transform.position);
        

        
    }


    //refreshtoken called from ondisconnect netmanager
    public void tokenUpdate(int conn)
    {
        //TokenHandlerScript.TokenRefreshCallCmd(1,conn);
    }

    [Command]
    private void CmdChangeName(string nme)
    {
        nameSync = nme;

        
    }
  

    [Command]
    private void DeviceType(string devicType)
    {
       
        Debug.Log("device type: " + devicType);
        Debug.Log("check is Mobile " + Application.isMobilePlatform);
       
    }
    
     void UpdateName(string oldName, string newName)
    {
        Debug.Log("Changed: " + newName);
        Field.text = newName;
        Field2.text = newName;
    }
    void Start()
    {
        
       
        if(isOwned == true)
        {
            Debug.Log(isOwned);
            controller = GetComponent<CharacterController>();
            NamenonOwnerView.SetActive(false);
            //joystickObj= GameObject.Find("Fixed Joystick");
            //GameObject joystickCamObj = GameObject.Find("Fixed Joystick Cam");
            //if (joystickObj != null)
            //{
            //    //joystickObj.SetActive(true);
            //    joystick_ = joystickObj.GetComponent<FixedJoystick>();
            //    joystick_Cam = joystickCamObj.GetComponent<FixedJoystick>();
            //}

            //animator = GetComponent<Animator>();
            isWalkingHash = Animator.StringToHash("isWalking");
            isWalkingBackHash = Animator.StringToHash("isWalkingBackwards");
            isRunningHash = Animator.StringToHash("isRunning");
            isLeftStrafHash = Animator.StringToHash("isLeftStraf");
            isRightStrafHash = Animator.StringToHash("isRightStraf");
            isRightTurnHash = Animator.StringToHash("rightTurn");
            isLeftTurnHash = Animator.StringToHash("leftTurn");




        }
        else
        {
            NameOwnerOnlyView.SetActive(false);
            //make name tags always face player
        }
    }
    // Update is called once per frame
    void Update()
    {
        //MovementandAnimations();
        HandleMovement();
        Look();

        if (isLocalPlayer == true && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("sending cmd to server");
            cmdHello();
        }
    }

    [Command]
    public void cmdHello()
    {
        helloCount += 1;
        Debug.Log("Recieved hello from client");
    
    }


    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    void HandleMovement()
    {
      

       
        if(isLocalPlayer == true)
        {
            

            if(mobileDevice == true)
            {
                
                
            }
            bool isJumping = Input.GetKeyDown(KeyCode.Space);
            Camera.main.transform.LookAt(transform.position);
            float joyStickValueVert = 0;


            //joystick setup

            //if (joystickObj == null)
            //{
            //    joystickObj = GameObject.Find("Fixed Joystick");
            //}
            //else if (joystick_ == null)
            //{
            //    joystick_ = joystickObj.GetComponent<FixedJoystick>();
            //    jstkY = joystick_.Vertical;
            //}
            //else
            //{
            //    jstkY = joystick_.Vertical;
            //}


            //if (joystickObj == null)
            //{
            //    jstkY = 0;
            //}
            //else if (joystick_ == null)
            //{
            //    joystick_ = joystickObj.GetComponent<FixedJoystick>();
            //    jstkY = joystick_.Vertical;
            //}
            //else
            //{
            //    jstkY = joystick_.Vertical;
            //}

            if (joystickObj != null)
            {
                if (joystickObj.activeSelf == true)
                {
                    jstkY = joystick_.Vertical;
                }
                else
                {
                    jstkY = 0;
                }
            }

            //Debug.Log("joystick " + joystick_.Horizontal);
            //float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float moveCombo = moveVertical + jstkY;
            Vector3 CamForward = Camera.main.transform.forward;
            //Vector3 yZero = new Vector3(1f, 0f, 1f);
            CamForward.y = 0;
            CamForward = CamForward.normalized;
            //Vector3 CamLeft = Camera.main.transform.right;
            //Vector3 LRMove = CamLeft * moveHorizontal;
            Vector3 movement = CamForward * moveCombo ;
            Vector3 MoveCharac = new Vector3(movement.x, -9f, movement.z);
            //Vector3 movement = new Vector3 (0f,0f,0f)* CamForward * moveVertical;
            controller.Move(MoveCharac * Time.deltaTime * speed);
            //Debug.Log("movement: " + MoveCharac);
           //if idle _> jump
          
            if (moveCombo > 0f)
            {
                animatorComp.SetBool("isWalkingBackwards", false);
                animatorComp.SetBool("isWalking", true);
                
            }
            else if(moveCombo < 0f)
            {
                animatorComp.SetBool("isWalkingBackwards", true);
                animatorComp.SetBool("isWalking", false);

            }
            else
            {
                animatorComp.SetBool("isWalking", false);
                animatorComp.SetBool("isWalkingBackwards", false);
            }
        }
    }

    //setup joystick
    public static void setupJoystick()
    {
        if(joystickObj == null)
        {
            joystickObj = GameObject.Find("Fixed Joystick");
            joystick_ = joystickObj.GetComponent<FixedJoystick>();
        }
        

    }
    void MovementandAnimations()
    {
        if (isLocalPlayer == true)
        {
            bool isWalking = animatorComp.GetBool(isWalkingHash);
            bool isRunning = animatorComp.GetBool(isRunningHash);
            bool isWalkingBack = animatorComp.GetBool(isWalkingBackHash);
            bool isLeftStraf = animatorComp.GetBool(isLeftStrafHash);
            bool isRightStraf = animatorComp.GetBool(isRightStrafHash);

            float inpVertical = Input.GetAxisRaw("Vertical");
            float inpHorizontal = Input.GetAxisRaw("Horizontal");

            float inpVertfloat = Input.GetAxis("Vertical");
            float inpHorizfloat = Input.GetAxis("Horizontal");
            jstkX = joystick_.Vertical;
            jstkY = joystick_.Horizontal;



            float checkXAxis = joystick_.Vertical + inpVertfloat;
            float checkYAxis = joystick_.Horizontal + inpHorizfloat;

            if(inpVertfloat != 0)
            {
                checkXAxis = -inpVertfloat;
            }
            if(inpHorizfloat != 0)
            {
                checkYAxis = inpHorizfloat;
            }

            Vector3 CamForward = Camera.main.transform.forward;
            Vector3 CamRight = Camera.main.transform.right;
            CamForward.y = 0;
            CamRight.y = 0;
            // Normalize to keep consistent movement speed
            CamForward = CamForward.normalized;
            CamRight = CamRight.normalized;


            //Vector3 movePlayer = -CamForward * Mathf.Abs(inpVertfloat) * inpVertical + CamRight * inpHorizontal * Mathf.Abs(inpHorizfloat);
            Vector3 movePlayer = CamForward * checkXAxis*speed + CamRight * checkYAxis*turnSpeed;

            CamLook();

            if (checkXAxis >0 && isWalking == false)
            {
                //Debug.Log("vert: " + inpVertical);
                animatorComp.SetBool("isWalking", true);

                if (isWalkingBack)
                {
                    animatorComp.SetBool("isWalkingBackwards", false);

                }
            }
            if (checkXAxis < 0 && isWalkingBack == false)
            {
                //Debug.Log("vert: " + inpVertical);
                animatorComp.SetBool("isWalkingBackwards", true);

                if (isWalking)
                {
                    animatorComp.SetBool("isWalking", false);

                }
            }
            else if (checkXAxis == 0)
            {
                //Debug.Log("vert: " + inpVertical);
                if (isWalking)
                {
                    animatorComp.SetBool("isWalking", false);
                }
                if (isWalkingBack)
                {
                    animatorComp.SetBool("isWalkingBackwards", false);
                }
            }

            if (!isRightStraf && checkYAxis > 0)
            {
                animatorComp.SetBool("isRightStraf", true);
                if (isLeftStraf)
                {
                    animatorComp.SetBool("isLeftStraf", false);
                }
            }
            if (!isLeftStraf && checkYAxis < 0)
            {
                animatorComp.SetBool("isLeftStraf", true);
                if (isRightStraf)
                {
                    animatorComp.SetBool("isRightStraf", false);
                }

            }
            if (checkYAxis == 0)
            {
                if (isRightStraf)
                {
                    animatorComp.SetBool("isRightStraf", false);
                }
                if (isLeftStraf)
                {
                    animatorComp.SetBool("isLeftStraf", false);
                }

            }

            // vertical movement
            controller.Move(new Vector3(movePlayer.x, -9f, movePlayer.z) * Time.deltaTime );
            // vertical animation
        }



    }


    void CamLook()
    {
        bool isWalking = animatorComp.GetBool(isWalkingHash);
        bool isRunning = animatorComp.GetBool(isRunningHash);
        bool isWalkingBack = animatorComp.GetBool(isWalkingBackHash);
        bool isLeftStraf = animatorComp.GetBool(isLeftStrafHash);
        bool isRightStraf = animatorComp.GetBool(isRightStrafHash);
        bool isRight = animatorComp.GetBool(isRightTurnHash);
        bool isLeft = animatorComp.GetBool(isLeftTurnHash);

        float CamInputHorizontal = Input.GetAxis("lookHorizontal");
        float CamInputVertical = Input.GetAxis("lookVertical");

        jstkCamX = joystick_Cam.Horizontal;
        jstkCamY = joystick_Cam.Vertical;

        float horizontalCheck = CamInputHorizontal + jstkCamX;
        CamInputHorizontal += jstkCamX;
        Vector3 rotate = new Vector3(0f, CamInputHorizontal * camSpeed, 0f);
        transform.eulerAngles = transform.eulerAngles + rotate * Time.deltaTime;

        if(!isWalking && !isRunning && !isWalkingBack && !isLeftStraf && !isRightStraf )
        {
            if(horizontalCheck >0f )
            {
                Debug.Log("hori positive" + horizontalCheck);
                if (isLeft == true)
                {
                    animatorComp.SetBool("leftTurn", false);
                }
                animatorComp.SetBool("rightTurn", true);
                

            }
            if (horizontalCheck < 0f)
            {
                //left turn
                Debug.Log("hori negative" + horizontalCheck);
                if (isRight == true)
                {
                    animatorComp.SetBool("rightTurn", false);
                }
                animatorComp.SetBool("leftTurn", true);

            }

            if(horizontalCheck == 0f)
            {
                if (isRight == true)
                {
                    animatorComp.SetBool("rightTurn", false);
                }
                if (isLeft == true)
                {
                    animatorComp.SetBool("leftTurn", false);
                }

            }

        }
        else if(isRight == true || isLeft == true)
        {
            if (isRight == true)
            {
                animatorComp.SetBool("rightTurn", false);
            }
            if (isLeft == true)
            {
                animatorComp.SetBool("leftTurn", false);
            }
        }
        

       
    }
   

    void Look()
    {
        

        if (isLocalPlayer == true)
        {
            if (mobileDevice == true)
            {
                 
            }
            /// joystickObj.SetActive(true);


            //joystick_ = joystickObj.GetComponent<FixedJoystick>();

            //joystick setup
            //if (joystickObj == null )
            //{
            //    joystickObj = GameObject.Find("Fixed Joystick");
            //    Debug.Log("lookg get joystick obj");
            //}
            //else if(joystick_ == null)
            //{

            //    joystick_ = joystickObj.GetComponent<FixedJoystick>();
            //    jstkY = joystick_.Horizontal;
            //    Debug.Log("assign val to jstky");
            //}
            //else
            //{
            //    jstkY= joystick_.Horizontal;
            //    Debug.Log("joystick obj and component there already just assign val");
            //}


            //if (joystickObj == null)
            //{
            //    Debug.Log("if obj still is null after attempting to find");
            //    jstkY =0;
            //}
            //else if (joystick_ == null)
            //{
            //    Debug.Log("found joystick obj now get component and assign val");
            //    joystick_ = joystickObj.GetComponent<FixedJoystick>();
            //    jstkY = joystick_.Horizontal;
            //}
            //else
            //{
            //    Debug.Log("found obj, joystick comp already was saved now just assign val");
            //    jstkY = joystick_.Horizontal;
            //}

            if (joystickObj != null)
            {
                if(joystickObj.activeSelf == true)
                {
                    jstkY = joystick_.Horizontal;
                }
                else
                {
                    jstkY = 0;
                }
            }


                float moveHorizontal = Input.GetAxis("Horizontal");
            float moveCombo = moveHorizontal + jstkY;

            //float moveVertical = Input.GetAxis("Vertical");

            //float CamHorizontal = Input.GetAxis("Horizontal_Camera");
            
            //float CamVertical = Input.GetAxis("Vertical_Camera");
            //float scroll = Input.GetAxis("Mouse ScrollWheel");
            //if (scroll != 0)
            //{
                
            //}
            //Camera.transform
            Vector3 rotate = new Vector3(0, moveCombo * rotateSpeed, 0f);
             transform.eulerAngles = transform.eulerAngles + rotate * Time.deltaTime;
            if (moveCombo != 0f && animatorComp.GetBool("isWalking") == false)
            {
                //TokenGifterScript.TokenCheck();

                animatorComp.SetBool("rightTurn", true);
            }
            else
            {
                animatorComp.SetBool("rightTurn", false);
            }


            
        }
    }
    void onHelloCountChange(int oldCount, int newCount)
        {
        //Debug.Log("old Count: " + oldCount + " new Count: " + newCount);
        }

    void isMobile()
    {
        //check if user is using mobile
        //activate joystick

    }

    //rpc or command that is called every 30 sec to 1 min to keep websockets up
    void heartbeat()
    {

    }

    //public void submitQuizClose()
    //{ 
    //    Debug.Log("enter wrapper");
    //    submitQuiz();
       
    //}
    //[Command(requiresAuthority = false)]
    //public async void submitQuiz()
    //{
    //    Debug.Log("enter submit");
    //    if (QuizInProgress == true )
    //    {
    //        Debug.Log("quiz in progress and about to sub");
    //         
    //        int numQuestions = 3;
    //        string uniID = uniqueID;

    //        int checkAns = 0;

    //        int itemCounter = 0;
    //        int questCounter = 0;

    //        string apiAdd = "";


    //        if (checkAns == 1)
    //        {

    //            for (int i = 0; i < numQuestions; i++)
    //            {
    //                //need uniqueID
    //                //int uniqueID_ = 905;


    //                questCounter++;
    //                string Flag_1 = "&data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
    //                itemCounter++;

    //                string Flag_2 = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
    //                itemCounter++;
    //                string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]=" + "0" + "&";
    //                itemCounter++;
    //                string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
    //                itemCounter++;


    //                apiAdd = apiAdd + Flag_1 + Flag_2 + questAns + seqChekc;

    //            }






    //            string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]=1,2,3&finishattempt=1";



    //            apiAdd = apiAdd + slots;

    //            apiSubmitUrl = apiSubmitUrl + tokenID + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=" + attemptID + apiAdd;
    //            Debug.Log(apiSubmitUrl);

    //            using var www = UnityWebRequest.Get(apiSubmitUrl);
    //            www.SetRequestHeader("Content-Type", "application/json");
    //            www.SetRequestHeader("Authorization", "fd");
    //            var operation = www.SendWebRequest();

    //            while (!operation.isDone)
    //                await Task.Yield();

    //            if (www.result == UnityWebRequest.Result.Success)
    //            {

    //                Debug.Log("submit before quit: " + www.result);


    //            }
    //            else
    //            {
    //                Debug.Log("submit before quit: " + www.error);
    //            }
    //        }

    //    }
    //    else
    //    {
    //        Debug.Log("quiz was completed, quiz not in progress");
    //    }
        




    //}

}
