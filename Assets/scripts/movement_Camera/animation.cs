using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class animation : MonoBehaviour
{


    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingBackHash;
    int isLeftStrafHash;
    int isRightStrafHash;
   public  CharacterController controller;
    public float moveSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
       // controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackHash = Animator.StringToHash("isWalkingBackwards");
        isRunningHash = Animator.StringToHash("isRunning");
        isLeftStrafHash = Animator.StringToHash("isLeftStraf");
        isRightStrafHash = Animator.StringToHash("isRightStraf");
    }                                                     

    // Update is called once per frame
    void Update()
    {
        //movement();
        MovementandAnimations();


    }
    void MovementandAnimations()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkingBack = animator.GetBool(isWalkingBackHash);
        bool isLeftStraf = animator.GetBool(isLeftStrafHash);
        bool isRightStraf = animator.GetBool(isRightStrafHash);

        float inpVertical = Input.GetAxisRaw("Vertical");
        float inpHorizontal = Input.GetAxisRaw("Horizontal");

        float inpVertfloat = Input.GetAxis("Vertical");
        float inpHorizfloat = Input.GetAxis("Horizontal");

        Vector3 CamForward = Camera.main.transform.forward;
        Vector3 CamRight = Camera.main.transform.right;
        CamForward.y = 0;
        CamRight.y = 0;
        // Normalize to keep consistent movement speed
        CamForward = CamForward.normalized;
        CamRight = CamRight.normalized;

        
        Vector3 movePlayer = -CamForward* Mathf.Abs(inpVertfloat) * inpVertical + CamRight*inpHorizontal* Mathf.Abs(inpHorizfloat);
        
          if (inpVertical == 1 && isWalking == false)
        {
            Debug.Log("vert: " + inpVertical);
            animator.SetBool("isWalking", true);

            if(isWalkingBack)
            {
                animator.SetBool("isWalkingBackwards", false);

            }
        }
        if (inpVertical == -1 && isWalkingBack == false)
        {
            Debug.Log("vert: " + inpVertical);
            animator.SetBool("isWalkingBackwards", true);
           
            if (isWalking)
            {
                animator.SetBool("isWalking", false);

            }
        }
        else if (inpVertical == 0)
        {
            Debug.Log("vert: " + inpVertical);
            if (isWalking)
            {
                animator.SetBool("isWalking", false);
            }
            if (isWalkingBack)
            {
                animator.SetBool("isWalkingBackwards", false);
            }
        }

        if(!isRightStraf && inpHorizontal == 1)
        {
            animator.SetBool("isRightStraf", true);
            if(isLeftStraf)
            {
                animator.SetBool("isLeftStraf", false);
            }
        }
        if (!isLeftStraf && inpHorizontal == -1)
        {
            animator.SetBool("isLeftStraf", true);
            if(isRightStraf)
            {
                animator.SetBool("isRightStraf",false);
            }

        }
        if(inpHorizontal == 0)
        {
            if(isRightStraf)
            {
                animator.SetBool("isRightStraf", false);
            }
            if (isLeftStraf)
            {
                animator.SetBool("isLeftStraf", false);
            }

        }

        // vertical movement
        controller.Move(new Vector3(movePlayer.x,-9f,movePlayer.z) * Time.deltaTime *moveSpeed);
        // vertical animation
      



    }

   
    void movement()
    {
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        //start walking
        if (forwardPressed == true && isWalking == false)
        {
            animator.SetBool("isWalking", true);


        }
        //stop walking
        if (forwardPressed == false && isWalking == true)

        {
            animator.SetBool("isWalking", false);
        }
        //
        if (forwardPressed == true && runPressed == true && isRunning == false)
        {
            animator.SetBool("isRunning", true);

        }

        //Stop running
        if (isRunning == true)
        {
            if (forwardPressed == false || runPressed == false)
            {
                animator.SetBool("isRunning", false);
            }
        }

    }
}
