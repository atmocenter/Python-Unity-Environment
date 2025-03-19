using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Mirror;
public class ToggleObjectives : MonoBehaviour
{

    public GameObject objectiveMenu;

    public TMP_Text[] taskText;
    private int[] finishedTasks = { 0, 0, 0, 0 };
    public LeaderBoard leaderBoard;
    public TMP_Text ObjBtnName;
    public GameObject ObjBtn;


    //public readonly SyncList<int> Topranks = new SyncList<int>();

    //[SyncVar]
    //private int latestRank = 0;

    public void unhideObjective()
    {    
        if (objectiveMenu.activeSelf == false)
        {
            objectiveMenu.SetActive(true);
            ObjBtn.SetActive(false);
            return;
        }
        if (objectiveMenu.activeSelf == true)
        {
            objectiveMenu.SetActive(false);
            ObjBtn.SetActive(true);
        }
    }


    public void taskCompleted(int i)
    {
        //get task text i
        //update text 
        if (finishedTasks[i] == 0)
        {
            finishedTasks[i] = 1;
            string text = taskText[i].text;
            string textUpdate = text + " -- DONE";

            if(i == 2)
            {
                textUpdate = "Touched The Treasure Chest - Done";
            }
            if(i == 0)
            {
                textUpdate = "Touched The Fredrick Douglas Statue - Done";
            }
            if (i == 1)
            {
                textUpdate = "Touched The Golden #1 - Done";
            }
            //if (i == 3)
            //{
            //    textUpdate = "Recieved 10/10 Score on Exam - Done";
            //}
            taskText[i].text = textUpdate;
            taskText[i].color = Color.green;
            bool tasksComp = AllTasksCompleted();
            if (tasksComp == true)
            {
                // print 
                Debug.Log("all completed");
                leaderBoard.CmdRankUpdateRun();
                //CmdRankUpdate();
            }
            else
            {
                Debug.Log("not all completed");
            }
        }

        
    }

    public void quizCompleted(int i, float quizGrade)
    {
        if (finishedTasks[i] == 0 && quizGrade >= 10)
        {
            finishedTasks[i] = 1;
            string text = taskText[i].text;
            string textUpdate = "You Earned A 100% On Your Exam - Done";
            taskText[i].text = textUpdate;
            taskText[i].color = Color.green;
            bool tasksComp = AllTasksCompleted();
            if (tasksComp == true)
            {
                // print 
                Debug.Log("all completed");
                leaderBoard.CmdRankUpdateRun();
                //CmdRankUpdate();
            }
            else
            {
                Debug.Log("not all completed");
            }

        }
    }

    private bool AllTasksCompleted()
    {
        int taskIncomplete = 0;
        Debug.Log("looking at Tasks");
        for(int i = 0; i <= finishedTasks.Length -1; i++) 
        { 
            if (finishedTasks[i] == 0)
            {
                taskIncomplete = 0;
                Debug.Log("item " + i + " :" + finishedTasks[i]);
                break;
            }
            else
            {
                taskIncomplete = 1;
                Debug.Log("item " + i + " :" + finishedTasks[i]);
            }

        }
        if (taskIncomplete == 0)
        {
            Debug.Log("Incomplete: " + finishedTasks);
            return false;
        }
        else
        {
            return true;
        }
    }

    //[Command]
    private void CmdRankUpdate()
    {
       
        //Debug.Log("count of rank: " + leaderBoard.Topranks.Count);
        //Topranks.Add(latestRank + 1);
        //latestRank += 1;


    }

   

 
}
