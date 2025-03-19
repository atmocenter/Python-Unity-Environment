using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class TokenGifter : NetworkBehaviour
{
    private int currIndex = 0;
    private string[] token = { "" };
    private string myToken = "";
    private string uniID = "";
    private int attemptID = 0;
    private string connIDstr = "";
    private bool quizPending = false;
    [SerializeField] GetSetName GetSetScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Command(requiresAuthority = false)]
    public void TokenRequest(NetworkConnectionToClient sender = null)
    {

        var netId = sender.identity.connectionToClient;
        //LocalConnectionToClient id = netId;
        Debug.Log("type: " + sender.GetType());


        if (currIndex >= token.Length)
        {
            Debug.Log("too long: " + currIndex);
            currIndex = 0;
        }
        int index = currIndex;
        Debug.Log("curr index: " + currIndex);
        Debug.Log("Cmd " + token[currIndex]);
        
        TokenRecieve(netId, currIndex);
        currIndex++;
    }
    [TargetRpc]
    public void TokenRecieve(NetworkConnection target,int curr)
    {

        Debug.Log("type recieved: " + target.GetType());

        myToken = token[curr];
        Debug.Log("my Token: " + myToken);
    }


    public void CallTokenRequestCmd()
    {

        TokenRequest();
    }


 

   //[TargetRpc]
   // public void DisperseInfo(NetworkConnectionToClient target)
   // {
   //     //get obj
   //     //get component
   //     //give data
   //     //uniID = uniqueID;
   //     GetSetScript.setUniID("sdf");
   //     //PlayerQuizData objComp = target.identity.GetComponent<PlayerQuizData>();
   //     //objComp.setUniID(uniqueID);
   // }






    public void SendoffData()
    {
        //client
        GetInfo();
    }
    
    [Command(requiresAuthority = false)]
    public void GetInfo( NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        Debug.Log("sending jljkfam");
        connIDstr = netId.ToString();
        Debug.Log("setting netID: "+ connIDstr);
        //uniID = uniqueID;
        //DisperseInfo(netId, uniqueID);

    }
    //
 



    public void grabinfoCmd ()
    {
        grabInfo();
    }



    [Command(requiresAuthority = false)]
    public void grabInfo (NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        //DisperseInfo(netId, uniID);

        Debug.Log("Grab netID: " + connIDstr);


    }


    // Function setIDtoPlayer(string attID, string tokn, string uniqueID, bool quizinProgress) //
    //will after each test start, place data into player variables in server
    //if the user does not end the quiz the server will do it for them


    public void setIDtoPlayer(string attID, string tokn, string uniqueID, bool quizinProgress)
    {
        Debug.Log("set stage idtoplayer: " + tokn);
        idToPlayer(attID,tokn, uniqueID, quizinProgress);
    }


    public void printIDonPlayer()
    {
        PrintID();
    }


   [Command(requiresAuthority = false)]
    public void idToPlayer(string attID, string tkn, string uniqueID,bool quizinProgress, NetworkConnectionToClient sender = null)
    {
        Debug.Log("idtoplayer: "+tkn);
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        Player player = netId.identity.GetComponent<Player>();
        player.setAttemptID(attID);
        player.setTokenID(tkn);
        player.setUniqueID(uniqueID);
        player.setQuizProgress(quizinProgress);


    }

    [Command(requiresAuthority = false)]
    public void PrintID(NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        Player player = netId.identity.GetComponent<Player>();
        player.getconnID();
    }

    public void TokenCheck()
    {
        Debug.Log("current token on Machine: "+myToken);
    }

    public string GetToken ()
    {
        
        return myToken;
    }

    public void bam(string check)
    {
        Debug.Log(check);
    }

    public void getAttemptID(int Aid)
    {
        attemptID = Aid;


    }
    public void getUniID(string uni)
    {
        
        uniID = uni;
    }

    public string grabUniID()
    {
        //return uniID;
        return uniID;
    }

    public void isQuizPending(bool pending)
    {
        quizPending = pending;
    }



    [Command(requiresAuthority = false)]
    public async void submitQuiz(bool quizinProgress, string uniqueID,string attID, string tkn )
    {
        Debug.Log("enter submit");
        if (quizinProgress == true)
        {
            Debug.Log("quiz in progress and about to sub token: " +tkn);
            string apiSubmitUrl = "";
            int numQuestions = 3;
            string uniID = uniqueID;

            int checkAns = 0;

            int itemCounter = 0;
            int questCounter = 0;

            string apiAdd = "";


           

                for (int i = 0; i < numQuestions; i++)
                {
                    //need uniqueID
                    //int uniqueID_ = 905;


                    questCounter++;
                    string Flag_1 = "&data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                    itemCounter++;

                    string Flag_2 = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                    itemCounter++;
                    string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]=" + "0" + "&";
                    itemCounter++;
                    string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
                    itemCounter++;


                    apiAdd = apiAdd + Flag_1 + Flag_2 + questAns + seqChekc;

                }






                string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]=1,2,3&finishattempt=1";



                apiAdd = apiAdd + slots;

                apiSubmitUrl = apiSubmitUrl + tkn + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=" + attID + apiAdd;
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
        else
        {
            Debug.Log("quiz was completed, quiz not in progress");
        }





    }



}
