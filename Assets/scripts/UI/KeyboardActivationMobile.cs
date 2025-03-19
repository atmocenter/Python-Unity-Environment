using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class KeyboardActivationMobile : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_WEBGL

    //[DllImport("__Internal")]
    //private static extern void FocusUnityInputField(string elementID); 

    [DllImport("__Internal")]
    private static extern void FocusUnityInputField(string name, string funcName);

#endif
    public GameObject EditorInputObj;
    public TMP_InputField EditorInput;
    // Start is called before the first frame update

    public void onInputSelect()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        //FocusUnityInputField(EditorInput.gameObject.name);
        FocusUnityInputField(EditorInput.gameObject.name, "updateInputFieldText");
#endif
    }

    void updateInputFieldText(string val)
    {
        Debug.Log("input field text updating");
        EditorInput.text = val;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
