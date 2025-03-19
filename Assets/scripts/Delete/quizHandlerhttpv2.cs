using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using TMPro;
using Mirror;


public class quizHandlerhttpv2 : MonoBehaviour
{


    //JObject quizdata = new JObject();
    //bool quizReady = false;
    //List<int> Selected_answers = new List<int>();
    //public TMP_Text qNameUi;
    //public GameObject[] qOptions;
    //public TMP_Text[] qOptionsText;
    //public GameObject[] checkBoxes;
    //public GameObject nextButton;
    //public GameObject prevButton;
    //public GameObject newQuizBtn;
    //public GameObject StartQuizBtn;
    //public GameObject submitBtn;
    //public GameObject gradeObj;
    //[SerializeField] private GameObject animatorCompObj;
    //public Animator animatorComp;
    //public GameObject quizItems;
    //[SerializeField] TokenGifter TokenGiftScript;
    //[SerializeField] QdataSync QDataSyncScript;
    //[SerializeField] GameObject quizUITR;
    //private string myToken;
    //private bool inProgress = false;
    //int complete = 0;
    //private int initialLoad = 0;
    //int curr = 0;

    //bool checkAni = false;

    //string token3_11 = "";
    //int attemptID = 891;
    //string uniID = "";
    //string apiUrl = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
    //string apiSubmitUrl = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
    //string quizData = "";
    //string apiStartQuiz = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";


   

    //public void GetTokenFromHandler(string token)
    //{
    //    token3_11 = token;
    //    Debug.Log("new token updated:" + myToken);
    //}


    //public async void startNewQuizUi()
    //{
    //    //if (checkAni == false)
    //    //{
    //    //    animatorComp = animatorCompObj.GetComponentInChildren<Animator>();
    //    //    checkAni = true;
    //    //}

    //    //token3_11 = TokenGiftScript.GetToken();
    //    if (token3_11 == "")
    //    {
    //        Debug.Log("no token yet selected");
    //        return;
    //    }
    //    else
    //    {
    //        Debug.Log(token3_11);
    //    }
    //    apiStartQuiz += "" + token3_11 + "&wsfunction=mod_quiz_start_attempt&moodlewsrestformat=json&quizid=71";
    //    apiUrl += "" + token3_11 + "&wsfunction=mod_quiz_get_attempt_data&moodlewsrestformat=json&attemptid=";
    //    apiSubmitUrl += "" + token3_11 + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=";

    //    //Debug.Log
    //    var url = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle";
    //    Debug.Log(apiStartQuiz);
    //    using var www = UnityWebRequest.Get(apiStartQuiz);
    //    www.SetRequestHeader("Content-Type", "application/json");
    //    www.SetRequestHeader("Authorization", "fd");
    //    var operation = www.SendWebRequest();

    //    while (!operation.isDone)
    //        await Task.Yield();
    //    try
    //    {
    //        if (www.result == UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.result);



    //            inProgress = true;

    //            JObject jsonx = JObject.Parse(www.downloadHandler.text);
    //            //get attempt id, get unique id
    //            Debug.Log(jsonx.ToString());

    //            //catch any issues with attempt ie already active attempt

    //            if (jsonx["attempt"] == null)
    //            {
    //                Debug.Log("error with quiz attempt");
    //                return;
    //            }
    //            Debug.Log(jsonx["attempt"]["id"].ToString());
    //            attemptID = int.Parse(jsonx["attempt"]["id"].ToString());
    //            Debug.Log(attemptID);
    //            uniID = jsonx["attempt"]["uniqueid"].ToString();
    //            Debug.Log(uniID);

    //            apiUrl = apiUrl + attemptID + "&page=0";
    //            apiSubmitUrl = apiSubmitUrl + attemptID;
    //            Debug.Log(apiSubmitUrl);
    //            Debug.Log(apiUrl);

    //            TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);

    //            //QDataSyncScript.CallCmdUpdateQList(attemptID, uniID, token3_11);
    //            //TokenGiftScript.isQuizPending(inProgress);
    //            //TokenGiftScript.getAttemptID(attemptID);
    //            //TokenGiftScript.getUniID(uniID);
    //            //TokenGiftScript.SendoffData(uniID);

    //            //set start quiz button to active
    //            //deactivate start new quiz button

    //            if(quizUITR.activeSelf == false)
    //            {
    //                quizUITR.SetActive(true);
    //            }
    //            if (gradeObj.activeSelf != false)
    //            {
    //                gradeObj.SetActive(false);
    //            }

    //            if (StartQuizBtn.activeSelf == false)
    //            {
    //                StartQuizBtn.SetActive(true);
    //            }
    //            newQuizBtn.SetActive(false);



    //        }


    //        else
    //        {
    //            Debug.Log(www.result);
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log(e.ToString());
    //        if (gradeObj.activeSelf != false)
    //        {
    //            gradeObj.SetActive(false);

    //        }
    //        if (StartQuizBtn.activeSelf == true)
    //        {
    //            StartQuizBtn.SetActive(false);
    //        }

    //    }

    //}

    //public async void StartNewQuiz()
    //{
    //    //if (checkAni == false)
    //    //{
    //    //    animatorComp = animatorCompObj.GetComponentInChildren<Animator>();
    //    //    checkAni = true;
    //    //}

    //    //token3_11 = TokenGiftScript.GetToken();
    //    if (token3_11 == "")
    //    {
    //        Debug.Log("no token yet selected");
    //        return;
    //    }
    //    else
    //    {
    //        Debug.Log(token3_11);
    //    }
    //    apiStartQuiz += "" + token3_11 + "&wsfunction=mod_quiz_start_attempt&moodlewsrestformat=json&quizid=71";
    //    apiUrl += "" + token3_11 + "&wsfunction=mod_quiz_get_attempt_data&moodlewsrestformat=json&attemptid=";
    //    apiSubmitUrl += "" + token3_11 + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=";

    //    //Debug.Log
    //    var url = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle";
    //    Debug.Log(apiStartQuiz);
    //    using var www = UnityWebRequest.Get(apiStartQuiz);
    //    www.SetRequestHeader("Content-Type", "application/json");
    //    www.SetRequestHeader("Authorization", "fd");
    //    var operation = www.SendWebRequest();

    //    while (!operation.isDone)
    //        await Task.Yield();
    //    try
    //    {
    //        if (www.result == UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.result);



    //            inProgress = true;

    //            JObject jsonx = JObject.Parse(www.downloadHandler.text);
    //            //get attempt id, get unique id
    //            Debug.Log(jsonx.ToString());

    //            //catch any issues with attempt ie already active attempt

    //            if (jsonx["attempt"] == null)
    //            {
    //                Debug.Log("error with quiz attempt");
    //                return;
    //            }
    //            Debug.Log(jsonx["attempt"]["id"].ToString());
    //            attemptID = int.Parse(jsonx["attempt"]["id"].ToString());
    //            Debug.Log(attemptID);
    //            uniID = jsonx["attempt"]["uniqueid"].ToString();
    //            Debug.Log(uniID);

    //            apiUrl = apiUrl + attemptID + "&page=0";
    //            apiSubmitUrl = apiSubmitUrl + attemptID;
    //            Debug.Log(apiSubmitUrl);
    //            Debug.Log(apiUrl);

    //            TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);

    //            //QDataSyncScript.CallCmdUpdateQList(attemptID, uniID, token3_11);
    //            //TokenGiftScript.isQuizPending(inProgress);
    //            //TokenGiftScript.getAttemptID(attemptID);
    //            //TokenGiftScript.getUniID(uniID);
    //            //TokenGiftScript.SendoffData(uniID);

    //            //set start quiz button to active
    //            //deactivate start new quiz button

    //            if (gradeObj.activeSelf != false)
    //            {
    //                gradeObj.SetActive(false);
    //            }

    //            if (StartQuizBtn.activeSelf == false)
    //            {
    //                StartQuizBtn.SetActive(true);
    //            }
    //            newQuizBtn.SetActive(false);



    //        }


    //        else
    //        {
    //            Debug.Log(www.result);
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log(e.ToString());
    //        if (gradeObj.activeSelf != false)
    //        {
    //            gradeObj.SetActive(false);

    //        }
    //        if (StartQuizBtn.activeSelf == true)
    //        {
    //            StartQuizBtn.SetActive(false);
    //        }

    //    }



    //}

    //public async void TestGet()
    //{
    //    //Debug.Log(apiUrl);



    //    var url = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle";
    //    using var www = UnityWebRequest.Get(apiUrl);
    //    www.SetRequestHeader("Content-Type", "application/json");
    //    www.SetRequestHeader("Authorization", "fd");
    //    var operation = www.SendWebRequest();

    //    while (!operation.isDone)
    //        await Task.Yield();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {


    //        //Debug.Log("success: " + www.downloadHandler.text);
    //        Debug.Log("success: ");
    //        JObject jsonx = JObject.Parse(www.downloadHandler.text);
    //        Debug.Log(jsonx.ToString());
    //        string jsonVals = jsonx["questions"][0]["html"].ToString();
    //        //string type = jsonx["questions"][0]["type"].ToString();
    //        JArray NumofQ = jsonx["questions"] as JArray;
    //        Debug.Log("length: " + NumofQ.Count);
    //        string[] typeArr = new string[3];
    //        string[] Qnames = new string[3];
    //        int j = 0;
    //        foreach (JObject types in jsonx["questions"])
    //        {
    //            Selected_answers.Add(-1);
    //            typeArr[j] = types["type"].ToString();
    //            HtmlDocument htmlDocument = new HtmlDocument();
    //            string htmlloading = types["html"].ToString();
    //            htmlDocument.LoadHtml(htmlloading);


    //            var QuestionName = htmlDocument.DocumentNode.SelectNodes("//div[@class='qtext']/p/text()");

    //            if (QuestionName != null)
    //            {
    //                Debug.Log("question: " + QuestionName[0].InnerText);
    //                Qnames[j] = QuestionName[0].InnerText;
    //            }
    //            else
    //            {
    //                QuestionName = htmlDocument.DocumentNode.SelectNodes("//div[@class='qtext']/text()");
    //                Debug.Log("question: " + QuestionName[0].InnerText);
    //                Qnames[j] = QuestionName[0].InnerText;
    //            }
    //            JObject qObject = new JObject();
    //            qObject["name"] = Qnames[j];
    //            quizdata["q" + j] = qObject;
    //            //var QuestionNames = htmlDoc.DocumentNode.SelectNodes("//div[@class='qtext']/p/text()");
    //            //Qnames[j] = QuestionName[0].InnerText;
    //            //if (QuestionName[0].InnerText != null)
    //            //{
    //            //    Qnames[j] = QuestionName[0].InnerText;
    //            //   Debug.Log("Qname: " + Qnames[j].ToString());
    //            //}
    //            //Debug.Log("question: " + QuestionName[0].InnerText);
    //            //Debug.Log(types["html"].ToString());
    //            j += 1;


    //        }
    //        //Debug.Log(typeArr);
    //        //Debug.Log(Qnames);  

    //        //if multiple choice

    //        //if true/false

    //        //q1
    //        //  name
    //        //  type
    //        //  options...
    //        //q2

    //        //quizdata["q0"]["boom"] = "yeaaa";
    //        Debug.Log(quizdata.ToString());



    //        j = 0;


    //        foreach (string type in typeArr)
    //        {
    //            JObject optionsKey = new JObject();
    //            Debug.Log("type: " + type);
    //            if (type == "multichoice")
    //            {
    //                quizdata["q" + j]["type"] = type;

    //                //qestion j
    //                //get choices from html 
    //                HtmlDocument htmlDocument = new HtmlDocument();
    //                string htmlText = jsonx["questions"][j]["html"].ToString();
    //                htmlDocument.LoadHtml(htmlText);
    //                var questOpt = htmlDocument.DocumentNode.SelectNodes("//span[@class='answernumber']/following-sibling::div[1]");
    //                int a = 0;
    //                foreach (HtmlAgilityPack.HtmlNode opt in questOpt)
    //                {
    //                    //quizdata["q" + j]["option"+a] = opt.InnerText;
    //                    optionsKey["option" + a] = opt.InnerText;

    //                    a += 1;
    //                }


    //            }

    //            if (type == "truefalse")
    //            {
    //                quizdata["q" + j]["type"] = type;
    //                optionsKey["option" + 0] = "false";
    //                optionsKey["option" + 1] = "true";
    //                //quizdata["q" + j]["option" + 0] = "true";
    //                //quizdata["q" + j]["option" + 1] = "false";
    //            }
    //            JObject optiondata = new JObject();
    //            optiondata = optionsKey;
    //            quizdata["q" + j]["options"] = optiondata;
    //            j += 1;
    //        }

    //        Debug.Log(quizdata.ToString());
    //        //Debug.Log(type)
    //        HtmlDocument htmlDoc = new HtmlDocument();
    //        htmlDoc.LoadHtml(jsonVals);
    //        //string Question
    //        var QuestionNames = htmlDoc.DocumentNode.SelectNodes("//div[@class='qtext']/text()");
    //        //Debug.Log(QuestionNames[0].InnerText);
    //        //foreach(HtmlAgilityPack.HtmlNode names in QuestionNames)
    //        //{

    //        //}
    //        string[] QuestionOptions = new string[3];
    //        var qOptions = htmlDoc.DocumentNode.SelectNodes("//span[@class='answernumber']/following-sibling::div[1]");
    //        int i = 0;
    //        foreach (HtmlAgilityPack.HtmlNode opt in qOptions)
    //        {
    //            QuestionOptions[i] = opt.InnerText;
    //            Debug.Log(QuestionOptions[i]);
    //            i += 1;
    //        }

    //        populateUIQuiz();
    //        if (StartQuizBtn.activeSelf == true)
    //        {
    //            StartQuizBtn.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("error: " + www.error);
    //    }

    //    //amount of questions
    //    //arrows to switch between questions
    //    //will page adjust based on text size amount?
    //    //
    //}
    ////add jobject items into the quiz canvas, only for question 1
    ////array of optiontext[] texttmp
    ////checkboxes[] (set active and deactive) based on amount of options
    ////prev gameobject
    ////next gameobject
    ////currIndex

    ////answers[]
    ////
    //public void populateUIQuiz()
    //{

    //    // get data
    //    JObject currQuestion = (JObject)quizdata["q" + curr];
    //    string nameTmptext = currQuestion["name"].ToString();
    //    string QuestionType = currQuestion["type"].ToString();
    //    TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
    //    JObject optionObject = (JObject)currQuestion["options"];

    //    int b = 0;
    //    tmpName.text = nameTmptext;
    //    foreach (var currOption in optionObject)
    //    {
    //        TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
    //        optxText.text = currOption.Value.ToString();
    //        if (qOptions[b].activeSelf == false)
    //        {
    //            qOptions[b].SetActive(true);
    //            checkBoxes[b].SetActive(true);
    //        }
    //        b++;
    //    }

    //    if (b != 3)
    //    {
    //        if (qOptions[2].activeSelf == true)
    //        {
    //            qOptions[2].SetActive(false);
    //            checkBoxes[2].SetActive(false);
    //        }
    //    }

    //    if (QuestionType == "multichoice")
    //    {



    //        //deactivate based on the b count
    //        // if 3 than all should be active
    //        //if 2 than last one should be deactivated ...
    //    }
    //    if (QuestionType == "truefalse")
    //    {

    //    }

    //    TMP_Text x = qOptions[curr].GetComponent<TMP_Text>();
    //    Debug.Log(nameTmptext);

    //    if (nextButton.activeSelf == false)
    //    {
    //        nextButton.SetActive(true);
    //    }

    //    //set items active if not already


    //    // type 
    //    //multiple choice
    //    //all three text and checkboxes are shown
    //    //when option is selected that item's index is added to answer array,for answer[curr] 


    //    //#num of questions
    //    //curr index
    //    //name
    //    //options
    //    //set items active

    //    quizItems.SetActive(true);
    //    prevButton.SetActive(false);
    //}

    ////select an answer

    //public void choiceSelected(int a)
    //{
    //    // add a x to the button
    //    //remove any other x's 
    //    //add value to answer list
    //    int bxCount = 0;
    //    foreach (var chkbx in checkBoxes)

    //    {
    //        TMP_Text checkText = checkBoxes[bxCount].GetComponentInChildren<TMP_Text>();
    //        if (bxCount == a)
    //        {

    //            checkText.text = "X";
    //        }
    //        else
    //        {
    //            checkText.text = "";
    //        }

    //        bxCount++;


    //    }

    //    Selected_answers[curr] = a;

    //    int counta = 0;
    //    foreach (var selectItems in Selected_answers)
    //    {
    //        Debug.Log("item " + counta + ": " + selectItems);

    //        counta++;
    //    }

    //    //TMP_Text checkText =  checkBoxes[a].GetComponent<TMP_Text>();






    //}


    ////move to next question press right arrow
    //public void NextQuestion()
    //{
    //    //current index
    //    //check if there is another question,
    //    //if so, repopulate quiz with next question items
    //    //look at type
    //    //populate question items based on that

    //    //if not dont do anything
    //    //

    //    curr = curr + 1;
    //    JObject currQuestion = (JObject)quizdata["q" + curr];
    //    string nameTmptext = currQuestion["name"].ToString();
    //    string QuestionType = currQuestion["type"].ToString();
    //    TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
    //    JObject optionObject = (JObject)currQuestion["options"];

    //    int b = 0;
    //    tmpName.text = nameTmptext;

    //    //answered?

    //    foreach (var itemX in checkBoxes)

    //    {
    //        TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
    //        chkText.text = "";
    //    }

    //    if (Selected_answers[curr] != -1)
    //    {
    //        int item_selected = Selected_answers[curr];
    //        TMP_Text checkText = checkBoxes[item_selected].GetComponentInChildren<TMP_Text>();
    //        checkText.text = "X";
    //    }
    //    //TMP_Text checkText = checkBoxes[ Selected_answers[curr] ].GetComponentInChildren<TMP_Text>();
    //    //

    //    foreach (var currOption in optionObject)
    //    {
    //        TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
    //        optxText.text = currOption.Value.ToString();
    //        if (qOptions[b].activeSelf == false)
    //        {
    //            qOptions[b].SetActive(true);
    //            checkBoxes[b].SetActive(true);
    //        }
    //        b++;
    //    }

    //    if (b != 3)
    //    {
    //        if (qOptions[2].activeSelf == true)
    //        {
    //            qOptions[2].SetActive(false);
    //            checkBoxes[2].SetActive(false);
    //        }
    //    }


    //    if (curr >= 2)
    //    {
    //        if (complete == 0)
    //        {
    //            //check if all answers are correct 
    //            //if so make submit button active
    //        }
    //        submitBtn.SetActive(true);
    //        nextButton.SetActive(false);



    //        //deactivate next

    //    }
    //    //prev active?
    //    if (prevButton.activeSelf == false)
    //    {
    //        prevButton.SetActive(true);
    //    }



    //}
    //public void PrevQuestion()
    //{
    //    //current index
    //    //check if there is another question, to go back to
    //    //if so, repopulate quiz with prev question items
    //    //look at type
    //    //populate question items based on that

    //    //if not dont do anything
    //    //

    //    if (curr != 0)
    //    {



    //        curr = curr - 1;
    //        JObject currQuestion = (JObject)quizdata["q" + curr];
    //        string nameTmptext = currQuestion["name"].ToString();
    //        string QuestionType = currQuestion["type"].ToString();
    //        TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
    //        JObject optionObject = (JObject)currQuestion["options"];

    //        foreach (var itemX in checkBoxes)

    //        {
    //            TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
    //            chkText.text = "";
    //        }

    //        if (Selected_answers[curr] != -1)
    //        {
    //            TMP_Text checkText = checkBoxes[Selected_answers[curr]].GetComponentInChildren<TMP_Text>();
    //            checkText.text = "X";
    //        }


    //        int b = 0;
    //        tmpName.text = nameTmptext;
    //        foreach (var currOption in optionObject)
    //        {
    //            TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
    //            optxText.text = currOption.Value.ToString();
    //            if (qOptions[b].activeSelf == false)
    //            {
    //                qOptions[b].SetActive(true);
    //                checkBoxes[b].SetActive(true);
    //            }
    //            b++;
    //        }

    //        if (b != 3)
    //        {
    //            if (qOptions[2].activeSelf == true)
    //            {
    //                qOptions[2].SetActive(false);
    //                checkBoxes[2].SetActive(false);
    //            }
    //        }


    //        if (curr <= 0)
    //        {
    //            //change submit to submit quiz
    //            //deactivate next
    //            prevButton.SetActive(false);
    //        }

    //        if (nextButton.activeSelf == false)
    //        {
    //            nextButton.SetActive(true);
    //        }
    //        //prev active?

    //    }
    //}
    ////submit the quiz
    ////mod_quiz_process_attempt


    ////must have answer list populated with non negative numbers
    ////answered each question




    //public async void submitQuiz()
    //{


    //    int checkAns = 0;
    //    foreach (var itemSelect in Selected_answers)
    //    {
    //        if (itemSelect == -1)
    //        {
    //            checkAns = 0;
    //            break;
    //        }
    //        else
    //        {
    //            checkAns = 1;
    //        }
    //    }
    //    int itemCounter = 0;
    //    int questCounter = 0;

    //    string apiAdd = "";


    //    if (checkAns == 1)
    //    {

    //        foreach (var itemSelect in Selected_answers)
    //        {
    //            //need uniqueID
    //            //int uniqueID_ = 905;


    //            questCounter++;
    //            string Flag_1 = "&data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
    //            itemCounter++;

    //            string Flag_2 = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:flagged&data[" + itemCounter + "][value]=0&";
    //            itemCounter++;
    //            string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]=" + itemSelect + "&";
    //            itemCounter++;
    //            string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
    //            itemCounter++;


    //            apiAdd = apiAdd + Flag_1 + Flag_2 + questAns + seqChekc;

    //        }

    //        Selected_answers.Clear();
    //        Debug.Log(Selected_answers);


    //        foreach (var itemX in checkBoxes)

    //        {
    //            TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
    //            chkText.text = "";
    //        }


    //        string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]=1,2,3&finishattempt=1";



    //        apiAdd = apiAdd + slots;

    //        apiSubmitUrl = apiSubmitUrl + apiAdd;
    //        Debug.Log(apiSubmitUrl);

    //        using var www = UnityWebRequest.Get(apiSubmitUrl);
    //        www.SetRequestHeader("Content-Type", "application/json");
    //        www.SetRequestHeader("Authorization", "fd");
    //        var operation = www.SendWebRequest();

    //        while (!operation.isDone)
    //            await Task.Yield();

    //        if (www.result == UnityWebRequest.Result.Success)
    //        {
    //            inProgress = false;
    //            //TokenGiftScript.isQuizPending(inProgress);
    //            TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);
    //            QDataSyncScript.CallCmdRemoveFromList();
    //            Debug.Log(www.result);

    //            getQuizGrades();

    //            apiStartQuiz = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
    //            apiUrl = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
    //            apiSubmitUrl = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=";
    //            curr = 0;
    //            if (newQuizBtn.activeSelf == false)
    //            {
    //                newQuizBtn.SetActive(true);

    //            }
    //            submitBtn.SetActive(false);
    //            StartQuizBtn.SetActive(false);
    //        }
    //        else
    //        {
    //            Debug.Log(www.error);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("answer all questions");
    //    }


    //}

    //private async void getQuizGrades()
    //{
    //    var url = "http://proxymoodleapi-env.eba-zy72iziq.us-east-1.elasticbeanstalk.com/moodle/";

    //    var attemptReview = url + "https://lms.lifechanges.solutions/moodle/webservice/rest/server.php?wstoken=" + token3_11 + "&wsfunction=mod_quiz_get_attempt_review&moodlewsrestformat=json&attemptid=" + attemptID;
    //    using var www = UnityWebRequest.Get(attemptReview);
    //    www.SetRequestHeader("Content-Type", "application/json");
    //    www.SetRequestHeader("Authorization", "fd");
    //    var operation = www.SendWebRequest();

    //    while (!operation.isDone)
    //        await Task.Yield();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {
    //        JObject jsonx = JObject.Parse(www.downloadHandler.text);
    //        //get attempt id, get unique id
    //        Debug.Log(jsonx.ToString());
    //        Debug.Log(jsonx["grade"].ToString());

    //        float quizGrade = float.Parse(jsonx["grade"].ToString());

    //        quizItems.SetActive(false);
    //        if (gradeObj.activeSelf != true)
    //        {
    //            gradeObj.SetActive(true);
    //        }
    //        TMP_Text gradeText = gradeObj.GetComponent<TMP_Text>();
    //        gradeText.text = "Grade: " + quizGrade.ToString();
    //        //Debug.Log(quizGrade);
    //        //animatorComp.SetBool("celebration", false);





    //    }

    //    else
    //    {
    //        Debug.Log(www.result);
    //    }
    //}


    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public bool quizInProgress()
    //{
    //    return inProgress;
    //}
    //public int getAttemptID()
    //{
    //    return attemptID;
    //}
    //public string getUniID()
    //{
    //    return uniID;
    //}

    //public float getQuizResults(float quiz)
    //{
    //    return 4.0f;
    //}

}
