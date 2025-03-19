using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Execute : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private TMP_Text textOutput;
    // Start is called before the first frame update
    void Start()
    {
        field.onSubmit.AddListener(OnInputSubmitted);
    }

    private void OnInputSubmitted(string inputText)
    {
        // This function will be called when the "Enter" key is pressed
        Debug.Log("Submitted: " + inputText);

        // if input field 

        string input = inputText.Replace(" ", "");

        //catch common err

        try
        {
            if(input.Contains("."))
            {
                throw new Exception("unexpected value : Line 1: x = "+ inputText);
            }
            int inputNum = int.Parse(input);
            textOutput.text = textOutput.text + "\nStatement ran successfully!";
        }

        catch (Exception e)
        {
            Debug.Log(e.ToString());
            textOutput.text = textOutput.text + "\nunexpected value : Line 1: x = " + inputText;
        }
        // Clear the input field for the next input
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(field.isFocused);
        //if (field.isFocused == true)
        //{
            
        //    if (Input.GetKeyDown(KeyCode.Return)) 
        //    {
        //        Debug.Log("execute and show feedback on command" + field.text);
        //        //execute code

        //    }
        //}
        
    }

    
}
