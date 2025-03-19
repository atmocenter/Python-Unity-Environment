using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using Mirror;
public class PlayerQuizData : NetworkBehaviour
{

    //grab data from quiz chair
    //have publicmethod that networkidentity can call to submit if not submitted

    //connID:
    //attemptID
    //UniqueID
    //myToken
    //quizPending

    int counter = 0;
    JObject Players = new JObject();
    private bool quizPending = false;
    private int attemptID = 0;
    private int numQuestions = 3;
    private string uniID = "";
    private string myToken = "";
    string apiSubmitUrl = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
     httpRequest httpRequestScript;
     TokenGifter TokenGiftScript;
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            GameObject httpObj = GameObject.Find("QuizBoard");
        httpRequestScript = httpObj.GetComponent<httpRequest>();

        GameObject tokenObj = GameObject.Find("TokenGifter");
        TokenGiftScript = tokenObj.GetComponent<TokenGifter>();

        attemptID = 34;
        
        //setConnID();

        }
        
    }


    private void Update()
    {
        if (isLocalPlayer)
        {
if (counter == 5 )
        {
            //printConnID();
        }
        counter++;
        }
            
    }

    [Command]
    public void setConnID()
    {
        Debug.Log("netID: " + netId);
        JObject ConnID = new JObject();
        //Players.Add(netId.ToString(), ConnID);
        //Debug.Log(Players);
    }


    [Command]
    public void printConnID()
    {
        Debug.Log(Players);
    }

    public void PendingCheck()
    {
        //grab quiz pending 
        //grab attempt id from httprequest

        //send http request to process quiz
        Debug.Log("pendingCheck");
        quizPending = httpRequestScript.quizInProgress();
        Debug.Log("quiz Pending: "+quizPending);
        attemptID = httpRequestScript.getAttemptID();
        Debug.Log ("quiz attid: "+ attemptID);
        if (quizPending == true)
        {
            uniID = httpRequestScript.getUniID();
            attemptID = httpRequestScript.getAttemptID();
            myToken = TokenGiftScript.GetToken();
            submitQuiz();
        }
    }


    public async void submitQuiz()
    {


        int checkAns = 0;
      
        int itemCounter = 0;
        int questCounter = 0;

        string apiAdd = "";


        if (checkAns == 1)
        {

            for (int i = 0; i < numQuestions; i ++)
            {
                //need uniqueID
                //int uniqueID_ = 905;


                questCounter++;
                string Flag_1 = "&data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                itemCounter++;

                string Flag_2 = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                itemCounter++;
                string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]=" + "0"+ "&";
                itemCounter++;
                string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
                itemCounter++;


                apiAdd = apiAdd + Flag_1 + Flag_2 + questAns + seqChekc;

            }

           




            string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]=1,2,3&finishattempt=1";



            apiAdd = apiAdd + slots;

            apiSubmitUrl = apiSubmitUrl + myToken+ "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid="+ attemptID+ apiAdd;
            Debug.Log(apiSubmitUrl);

            using var www = UnityWebRequest.Get(apiSubmitUrl);
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "fd");
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.Success)
            {
               
                Debug.Log("submit before quit: " + www.result);

               
            }
            else
            {
                Debug.Log("submit before quit: " + www.error);
            }
        }
        
        


    }

    // Update is called once per frame


    public string getUniID()
    {
        return TokenGiftScript.grabUniID();
    }
    public void setUniID(string uniqueID)
    {
        uniID = uniqueID;
    }
    public void getdad()
    {
        Debug.Log(attemptID);
    }




}
