using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InGameCodeEditor;

public class KeyboardButtonController : MonoBehaviour
{
    [SerializeField] Image containerBorderImage;
    [SerializeField] Image containerFillImage;
    [SerializeField] Image containerIcon;
    [SerializeField] TextMeshProUGUI containerText;
    [SerializeField] TextMeshProUGUI containerActionText;
    [SerializeField] TMP_InputField  EditorText;
     CodeEditor codeEditorScript;
    
    private void Start() {
        SetContainerBorderColor(ColorDataStore.GetKeyboardBorderColor());
        SetContainerFillColor(ColorDataStore.GetKeyboardFillColor());
        SetContainerTextColor(ColorDataStore.GetKeyboardTextColor());
        SetContainerActionTextColor(ColorDataStore.GetKeyboardActionTextColor());
        GameObject codeObj = GameObject.Find("InGameCodeEditor_mobileVrson");
        codeEditorScript = codeObj.GetComponent<CodeEditor>();
        //codeEditorScript.InputField.ActivateInputField();
    }

    public void SetContainerBorderColor(Color color) => containerBorderImage.color = color;
    public void SetContainerFillColor(Color color) => containerFillImage.color = color;
    public void SetContainerTextColor(Color color) => containerText.color = color;
    public void SetContainerActionTextColor(Color color) { 
        containerActionText.color = color;
        containerIcon.color = color;
        
   
    }


    public void AddLetter()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddLetter(containerText.text);
            Debug.Log("ad letter not null");
        }
        else
        {
            Debug.Log(containerText.text + " is pressed");
            //codeEditorScript.InputField.ActivateInputField();
            //getpos
            int pos = codeEditorScript.InputField.caretPosition;

            int newpos = pos;

           string textStr = codeEditorScript.Text;

            if (pos>0)
            {
                //get substring from 0 to pos
                string sub1 = textStr.Substring(0, pos);
               string sub2 = textStr.Substring(pos);
                sub1 += containerText.text;
                pos = sub1.Length;
                textStr = sub1 + sub2;
                Debug.Log("sub1: " + sub1);
                Debug.Log("sub2: " + sub2);
                Debug.Log("textStr: " + textStr);
            }
            else
            {
                textStr = containerText.text + textStr;
                pos = 1;
            }

            //textStr += containerText.text;
            codeEditorScript.Text = textStr;
            //EditorText.text = text;
            //EditorText.Select();
            
            codeEditorScript.InputField.caretPosition = pos;
            //codeEditorScript.InputField.c
            //EditorText.ForceLabelUpdate();
            //EditorText.ActivateInputField();
            //EditorText.ForceLabelUpdate();
           
            //codeEditorScript.InputField.Select();
        }
    }
    public void DeleteLetter()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DeleteLetter();
        }
        else
        {
            Debug.Log("Last char deleted");


            string textStr = codeEditorScript.Text;
            textStr = textStr.Substring(0, textStr.Length - 1);
            codeEditorScript.Text = textStr;

        }
    }
    public void TabSelected()
    {
        Debug.Log("Tab Entered");
       
        string textStr = codeEditorScript.Text;
        int pos = codeEditorScript.InputField.caretPosition;

        int newpos = pos;
        if (pos > 0)
        {
            //get substring from 0 to pos
            string sub1 = textStr.Substring(0, pos);
            string sub2 = textStr.Substring(pos);
            sub1 += "\t";
            pos = sub1.Length;
            textStr = sub1 + sub2;
            Debug.Log("sub1: " + sub1);
            Debug.Log("sub2: " + sub2);
            Debug.Log("textStr: " + textStr);
        }
        else
        {
            textStr = "\t" + textStr;
            pos = 1;
        }




        textStr += "\t";
        codeEditorScript.Text = textStr;
        //codeEditorScript.InputField.caretPosition = codeEditorScript.InputField.text.Length;
        codeEditorScript.InputField.caretPosition = pos;
        Debug.Log("text: " + codeEditorScript.Text + " txt length: " + codeEditorScript.InputField.text.Length);
    }






    public void SubmitWord()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SubmitWord();
        }
        else
        {
            Debug.Log("Submitted successfully!");
            string textStr = codeEditorScript.Text;
            int pos = codeEditorScript.InputField.caretPosition;

            int newpos = pos;
            if (pos > 0)
            {
                //get substring from 0 to pos
                string sub1 = textStr.Substring(0, pos);
                string sub2 = textStr.Substring(pos);
                sub1 += "\n";
                pos = sub1.Length;
                textStr = sub1 + sub2;
                Debug.Log("sub1: " + sub1);
                Debug.Log("sub2: " + sub2);
                Debug.Log("textStr: " + textStr);
            }
            else
            {
                textStr = "\n" + textStr;
                pos = 1;
            }



            
            textStr += "\n";
            codeEditorScript.Text = textStr;
            //codeEditorScript.InputField.caretPosition = codeEditorScript.InputField.text.Length;
            codeEditorScript.InputField.caretPosition = pos;
            Debug.Log("text: "+codeEditorScript.Text +" txt length: "+codeEditorScript.InputField.text.Length);



        }
    }

}