using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LMSItemSelected : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    httpRequest httpReqScr;
    int instance;
    string moduleName;
    bool selected = false;
    void Start()
    {
        
    }

    public void setData(int Instance, string modName, httpRequest httpReqScript )
    {
        //set data ie LMS item 
        //ref to httprequestscript obj
        //item instance (quiz instance is quiz id)

        //getpage
        instance = Instance;
        moduleName = modName;
        httpReqScr = httpReqScript;
        Debug.Log("set data: " +instance);
        Debug.Log("set data: " + moduleName);
        if( httpReqScript != null )
        {
            Debug.Log("set data: http script not null ");
        }



    }
    //selected - hightlights selected
    //unselected - un highlights unselected

    public void OnPointerClick(PointerEventData eventData)
    {
        //determine if quiz or page ... 
        //if quiz already in progress stop there
        //show quiz or page
        //change color to showcase its been selected
        Debug.Log("Item clicked");


        //if(selected == false)
        //{
            if(moduleName == "quiz")
            {
                //if quiz is in progress do nothing
                //call quiz function
                if(httpReqScr.getProgressStatus() == false)
                {
                    //send instance
                    httpReqScr.startQuizGetData(instance);
                    //change color
                    selected = true;
                }
               
            }
          else if(moduleName == "page")
            {
                //if page is already open replace with new page info
                //open page ui
                //change color
                selected = true;
            }

            
        //}
        //else
        //{
        //    //selected = false;
           
        //}
    }
    public void updateSelected()
    {
        if( selected == true )
        {
            selected = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
