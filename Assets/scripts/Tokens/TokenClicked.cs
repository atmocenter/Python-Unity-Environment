using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class TokenClicked : MonoBehaviour, IPointerClickHandler
{

    public int tokenIndex;
    private int availability = 1;
    public TokenHandler TokenHandlerScript;
    [SerializeField] private TMP_Text textColor;
    public GameObject TokenCanvas;

    public void OnPointerClick(PointerEventData pointereventdata)
    {

       
        
        //if token is available then click
        //if not then turn gray and is not clickable this might happen in token handler
        Debug.Log(pointereventdata);
        if(availability == 1)
        {
            TokenHandlerScript.TokenUpdatedCallCmd(tokenIndex, 0);

        }
       

    }

    //update the availabilty 
    public void AvailabilityUpdate(int value)
    {
        availability = value;
    }

    public void ChangeColor()
    {
        if(availability == 1)
        {
            
            textColor.color = Color.white;

        }
        if(availability == 0)
        {
            textColor.color = Color.gray;
        }
       
    }


    // Start is called before the first frame update
    void Start()
    {
        TokenCanvas = GameObject.Find("TokenMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
