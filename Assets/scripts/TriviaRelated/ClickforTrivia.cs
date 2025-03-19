using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickforTrivia : MonoBehaviour, IPointerClickHandler
{

// Determines what scavenger item was found, 
//ques up the MSU trivia that coresponds to the item

    public int triviaIndex;
    GameObject chest;
    public GameObject TriviaUI;
    public TMP_Text TriviaText;
    private ToggleObjectives toggleObjectivesScript;
    public void OnPointerClick(PointerEventData pointereventdata)
    {
        Debug.Log("clicked #1");

        //national treasure
        if (triviaIndex == 2)
        {

            //toggleObjectivesScript.taskCompleted(triviaIndex);
            Animator ChestAnim = chest.GetComponent<Animator>();
            ChestAnim.SetBool("open", true);
            return;
        }
        

        //number 1
        if(triviaIndex == 1)
        {
            TriviaText.text = "Morgan State historically ranks among the top public campuses nationally in the production of black graduates who go on to receive doctorate degrees." +
                "\r\n\r\n In recent years Morgan was shown to be #1 in the state of Maryland for awarding Doctoral Research Degrees in the area of Engineering, Higher Education, Bio-Environmental Science, Science Education, and more ";
            //toggleObjectivesScript.taskCompleted(triviaIndex);
        }
        //national treasure
        if (triviaIndex == 2)
        {

            TriviaText.text = "Morgan State University, one of the nation’s oldest institutions founded to provide a college education to black students, was named a National Treasure by the National Trust for Historic Preservation." +
                "\r\n\r\n Morgan State, the largest historically black university in Maryland, was founded in 1867, and its Baltimore-area campus includes 20 buildings eligible for listing on the National Register. ";
        }
        //Fredrick Statue
        if (triviaIndex == 0)
        {
            TriviaText.text = "Born into slavery in 1818 on Maryland’s Eastern Shore in Talbot County, Frederick Bailey escaped to freedom in 1838 while working as a ship’s caulker in Fell’s Point. Aided by Anna Murray, whom he married soon after, Douglass took a train from Baltimore to New York.\r\n\r\nAs a free man, he adopted the surname “Douglass” and became a powerful voice of the abolitionist cause as a writer, publisher, and orator traveling throughout the United States and the United Kingdom. His autobiography,Narrative of the Life of Frederick Douglass, An American Slave, was first published in 1845. ";

            //toggleObjectivesScript.taskCompleted(triviaIndex);
        }

        TriviaUI.SetActive(true);


    }

    public void chestOpenFinished()
    {
        TriviaUI.SetActive(true);
        TriviaText.text = "Morgan State University, one of the nation’s oldest institutions founded to provide a college education to black students, was named a National Treasure by the National Trust for Historic Preservation." +
                "\r\n\r\n Morgan State, the largest historically black university in Maryland, was founded in 1867, and its Baltimore-area campus includes 20 buildings eligible for listing on the National Register. ";

    }


    public void closeTrivia()
    {
        if(TriviaUI.activeSelf == true)
        {
            if(triviaIndex == 2)
            {
                Animator ChestAnim = chest.GetComponent<Animator>();
                ChestAnim.SetBool("open", false);
            }
            TriviaUI.SetActive(false);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
         chest = GameObject.Find("treasue_chest");
        GameObject objectives = GameObject.Find("Obj_Inst_Btns");
        toggleObjectivesScript = objectives.GetComponent<ToggleObjectives>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
