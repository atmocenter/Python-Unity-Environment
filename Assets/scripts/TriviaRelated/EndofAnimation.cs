using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndofAnimation : MonoBehaviour
{
    public GameObject TriviaUI;
    public TMP_Text TriviaText;
    public ToggleOffTrivia ToggleOffTriviaScript;

     
     public void EndofAni()
    {
        
        ToggleOffTriviaScript.setIndex(1);
        TriviaUI.SetActive(true);
        TriviaText.text = "Morgan State University, one of the nation’s oldest institutions founded to provide a college education to black students, was named a National Treasure by the National Trust for Historic Preservation." +
                "\r\n\r\n Morgan State, the largest historically black university in Maryland, was founded in 1867, and its Baltimore-area campus includes 20 buildings eligible for listing on the National Register. ";


    }
}
