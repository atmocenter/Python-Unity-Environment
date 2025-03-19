using InGameCodeEditor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mobileManger : MonoBehaviour
{
    bool isMobileOn = false;

    bool MobileOn = false;
    bool isKeyboardNeeded = false;
    public GameObject joystick;
    public GameObject KeyboardObj;
    public GameObject CodeEditor;
    public GameObject InstructionsObj;
    public GameObject mobileCodeEditor;
    public CodeEditor codeEditorScript;
    public GameObject InventoryUI;
    public TMP_Text onDeviceBtn;
    [SerializeField] GameObject LMSPanel;



    public void onMobileClick()
    {
        MobileOn = !MobileOn;
        if(MobileOn)
        {
            
            //set active
            joystick.SetActive(true);
            Player.setupJoystick();
            //isKeyboardNeeded = true;
            
#if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.mobileKeyboardSupport so the built-in mobile keyboard support is disabled.
        WebGLInput.mobileKeyboardSupport = true;
#endif
            onDeviceBtn.text = "onComputer";
        }
        else
        {
            //set inactive
            //isKeyboardNeeded= false;
            joystick.SetActive(false);
#if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.mobileKeyboardSupport so the built-in mobile keyboard support is disabled.
        WebGLInput.mobileKeyboardSupport = false;
#endif
            onDeviceBtn.text = "onMobile";
        }
    }

    public void CodeEdClick()
    {
        if (isMobileOn == true)
        {
            if (mobileCodeEditor.activeSelf)
            {
                mobileCodeEditor.SetActive(false);
            }
            else
            {
                mobileCodeEditor.SetActive(true);
            }
        }
        else
        { 
            if (CodeEditor.activeSelf)
            {
                CodeEditor.SetActive(false);
            }
            else
            {
                CodeEditor.SetActive(true);
            }
        }
    }
    public void onKeyboardClick() {
        isKeyboardNeeded = !isKeyboardNeeded;
        if(isKeyboardNeeded)
        {
            KeyboardObj.SetActive(true);
        }
        else
        {
            KeyboardObj.SetActive(false);
        }
    }

    public void onInstructionClick()
    {
        if(InstructionsObj.activeSelf)
        {
            InstructionsObj.SetActive(false);
        }
        else
        {
            InstructionsObj.SetActive(true);
        }
    }

    public void ExitInstructions()
    {
        InstructionsObj.SetActive(false);
    }
    public bool getIsMobile()
    {
        return isMobileOn;

    }

    public void ToggleInventory()
    {
        if(InventoryUI.activeSelf)
        {
            InventoryUI.SetActive(false);
        }
        else 
        { 
            InventoryUI.SetActive(true); 
        
        }
    }
    public string getTextCodeEditor()
    {
        return codeEditorScript.Text;
    }

    public void ToggleLMS()
    {
        if(LMSPanel.activeSelf == true)
        {
            LMSPanel.SetActive(false);
        }
        else
        {
            LMSPanel.SetActive(true);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
