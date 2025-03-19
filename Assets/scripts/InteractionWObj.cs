using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SocialPlatforms;

public class InteractionWObj : NetworkBehaviour
{

    // Start is called before the first frame update

    public Animator chestAnimator;

    private void OnTriggerEnter(Collider collision)
    {
        
        if(isLocalPlayer == false)
        {
          
            return;
        }
       if (collision.gameObject.tag == "interactObj")
        {
            //toggle on trivia window
            Debug.Log("entered collision #1");
        }
        if (collision.gameObject.tag == "interactChest")
        {
            //toggle animation
            //toggle trivia window
            //toggle close animatoin after window is shut
            Debug.Log("entered collision chedgst");
        }
        if (collision.gameObject.tag == "InteractFredrick")
        {
            //toggle animation
            //toggle trivia window
            //toggle close animatoin after window is shut
            Debug.Log("entered collision Fredrick Douglas");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
