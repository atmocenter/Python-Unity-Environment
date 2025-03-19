using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using System;

public class TNVirtualKeyboard : MonoBehaviour
{
	
	public static TNVirtualKeyboard instance;
	
	public string words = "";
	
	public GameObject vkCanvas;
	
	public TMP_InputField targetText;

  
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
		HideVirtualKeyboard();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void KeyPress(string k){
		if(k=="xyz")
		{
			if( words.Length > 0)
			{
			words = words.Substring(0, words.Length - 1);
			targetText.text = words;
			return;
			}
			else
			{
				return;
			}
			
			
		}
		if (k=="enter")
		{
			words += "\n";
            targetText.text = words;
			return;
        }
		words += k;
		targetText.text = words;	
	}
	
	public void Del(){
		words = words.Remove(words.Length - 1, 1);
		targetText.text = words;	
	}
	
	public void ShowVirtualKeyboard(){
		vkCanvas.SetActive(true);
	}
	
	public void HideVirtualKeyboard(){
		vkCanvas.SetActive(false);
	}
}
