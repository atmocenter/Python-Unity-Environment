using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class QdataSync : NetworkBehaviour
{
    private readonly SyncList<string> QdataSyncList = new SyncList<string>();

    [SerializeField] string MoodleAPIUrl = "";
    string getAttemptDataurlOrg = "";
    string apiSubmitUrlOrg = "";


    JObject qDataInfo = new JObject();

    private void Start()
    {
        if (MoodleAPIUrl != "")
        {
            getAttemptDataurlOrg = MoodleAPIUrl + "?wstoken=";
            apiSubmitUrlOrg = MoodleAPIUrl + "?wstoken=";
        }
    }
    public void CallCmdUpdateQList(int attemptID, string uniID, string tokenID, int QuestionNum)
    {
        CmdupdateList(attemptID, uniID, tokenID, QuestionNum);
    }
    [Command(requiresAuthority = false)]
    private void CmdupdateList (int attemptID, string uniID, string tokenID,int QuestionNum, NetworkConnectionToClient sender = null)
    {
        string conn = sender.connectionId.ToString();
        Debug.Log("Cmd Update QdataList");
        conn = "connID: " + conn; 
        string attID = attemptID.ToString();

        //JObject ConnInfo = new JObject();
        //ConnInfo["connID"] = conn;
        //ConnInfo["attemptID"] = attID;
        //ConnInfo["uniqueID"] = uniID;
        //ConnInfo["token"] = tokenID;
        //string dataName = conn.ToString();
        //qDataInfo[dataName] = ConnInfo;
       
        QdataSyncList.Add(conn);
        QdataSyncList.Add(tokenID);
        QdataSyncList.Add(attID);
        QdataSyncList.Add(uniID);
        QdataSyncList.Add(QuestionNum.ToString());
        //have questions # here so we can know how many times to loop

        RpcprintQdatalist();
    }

    [Command(requiresAuthority = false)]
    private void CmdVerifyAttemptCompletion()
    {
        //check if attempt finished, perform a http call
    }



   


    [Command]
    private void CmdremoveFromList()
    {
        //search for index of conn
        //if its there
        //remove QdataSy[index} index + 1 and index +2

    }

    [ClientRpc]
    private void RpcprintQdatalist()
    {
        Debug.Log("Rpc Print Quiz Data List");
        foreach (var item in QdataSyncList) 
        {
            //Debug.Log(item);
        }
    }
    [Command(requiresAuthority = false)]
    private void cmdRemovefrmList(NetworkConnectionToClient sender = null)
    {
        string conn = sender.connectionId.ToString();
        conn = "connID: " + conn;
        int index = QdataSyncList.IndexOf(conn);
        if (index == -1)
        {
            Debug.Log("nothing found");
            return;
        }
        //qDataInfo[conn].Remove();
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);

    }
     public void CallCmdRemoveFromList()
     {
        cmdRemovefrmList();
        
     }

    [Server]
    public void RemovefromListAfterQuit(int connectId)
    {
        string conn = connectId.ToString();
        conn = "connID: " + conn;
        int index = QdataSyncList.IndexOf(conn);
        if (index == -1)
        {
            Debug.Log("nothing found");
            return;
        }
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);
        QdataSyncList.RemoveAt(index);

    }


    //          Function - QuizAttemptCheck(int conn)         //

    //if quiz attempt has been started by user, but not finished
    //this function will end the quiz attempt, as of now it will 
    //submit the quiz attempt with all answers filled in as 0
    //********************************************************//
    [Server]
    public async void QuizAttemptCheck(int conn)
    {
        string connectionID = conn.ToString();

        connectionID = "connID: " + connectionID;
        Debug.Log("from quiz attempt check: "+ connectionID);

        int index = QdataSyncList.IndexOf(connectionID);
        Debug.Log("index of conn: " + index);

        bool activeQuizAttempt = false;
        foreach (var item in QdataSyncList)
        {
            Debug.Log(item);
        }

        if (index == -1)
        {
            Debug.Log("no quiz attempt made");
           


            return;
        }

        string tkID = QdataSyncList[index+1].ToString();
        string attID = QdataSyncList[index + 2].ToString();
        string uniID = QdataSyncList[index + 3].ToString();
        int questNum = int.Parse(QdataSyncList[index + 4]);
        Debug.Log("token: "+tkID);
        Debug.Log("attemptID: " + attID);
        foreach (var item in QdataSyncList)
        {
            Debug.Log(item);
        }
        string getAttemptDataurl = getAttemptDataurlOrg;
        getAttemptDataurl = getAttemptDataurl+ "" + tkID + "&wsfunction=mod_quiz_get_attempt_data&moodlewsrestformat=json&attemptid=" + attID +"&page=0";

        Debug.Log(getAttemptDataurl);
        using var www = UnityWebRequest.Get(getAttemptDataurl);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");

        try
        {
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                JObject jsonx = JObject.Parse(www.downloadHandler.text);
                Debug.Log(jsonx.ToString());
                if (jsonx["errorcode"] != null)

                {
                   
                    Debug.Log("possible duplicate of data in list, or error occurred");
                    return;
                }
                else
                {
                   activeQuizAttempt=true;
                }
                Debug.Log(jsonx.ToString());

            }
        }

        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }


       
        if (activeQuizAttempt == true)
        {
            string apiSubmitUrl = apiSubmitUrlOrg;
            apiSubmitUrl = apiSubmitUrl + "" + tkID + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=" + attID;
            string apiAdd = "";
            int itemCounter = 0;
            int questCounter = 0;
            int itemSelect = 0;
            string seqCounter = "";

            //hardcode # of questions for 3
            for (int i = 0; i <= questNum-1; i ++)
            {
                    questCounter++;
                    string Flag_1 = "&data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                    itemCounter++;

                    string Flag_2 = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
                    itemCounter++;
                    string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]=" + itemSelect + "&";
                    itemCounter++;
                    string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
                    itemCounter++;

                    seqCounter = seqCounter + questCounter.ToString() + ",";

                    apiAdd = apiAdd + Flag_1 + Flag_2 + questAns + seqChekc;
            }

            string subString = seqCounter.Substring(0, seqCounter.Length - 1);

            string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]="+ subString+"&finishattempt=1";
            apiAdd = apiAdd + slots;
            apiSubmitUrl = apiSubmitUrl + apiAdd;
            Debug.Log(apiSubmitUrl);

            using var webReq = UnityWebRequest.Get(apiSubmitUrl);
            webReq.SetRequestHeader("Content-Type", "application/json");
            webReq.SetRequestHeader("Authorization", "fd");
           

            //process attempt
            try
            {
                var operation = webReq.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (webReq.result == UnityWebRequest.Result.Success)
                {
                    JObject jsony = JObject.Parse(webReq.downloadHandler.text);
                    Debug.Log(jsony.ToString());
                    RemovefromListAfterQuit(conn);
                }





            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }



        }


       
 






    }
}
