using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleOffTrivia : MonoBehaviour
{
    public GameObject TriviaUI;
    public TMP_Text TriviaText;
   public int TriviaIndex;


    public void closeTrivia()
    {
        if (TriviaUI.activeSelf == true)
        {

            TriviaText.text = "";
            TriviaUI.SetActive(false);
            if(TriviaIndex== 1)
            {
                GameObject chest = GameObject.Find("treasue_chest");

                Animator ChestAnim = chest.GetComponent<Animator>();
                ChestAnim.SetBool("open", false);
            }
        }

    }

    public void setIndex(int index)
    {
        TriviaIndex = index;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
