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

using UnityEngine.UI;

using System.Linq;

public class   httpRequest : MonoBehaviour
{
    JObject quizdata = new JObject();
    //
    string xax = "3";
    bool quizReady = false;
List<int> Selected_answers = new List<int>();
    public TMP_Text qNameUi;
    public GameObject[] qOptions;
    public TMP_Text[] qOptionsText;
    public GameObject[] checkBoxes;
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject newQuizBtn;
    public GameObject StartQuizBtn;
    public GameObject submitBtn;
    public GameObject gradeObj;
    [SerializeField] private GameObject animatorCompObj;
    public Animator animatorComp;
    public GameObject quizItems;
    [SerializeField] TokenGifter TokenGiftScript;
    [SerializeField] QdataSync QDataSyncScript;
    [SerializeField] GameObject quizUITR;
    [SerializeField] GameObject quizDonebtn;
    [SerializeField] ToggleObjectives toggleObjectivesScript;

    [SerializeField] GameObject quizContent;
    [SerializeField] GameObject quizMultiChoiceOption;
    [SerializeField] GameObject quizPrompt;
    [SerializeField] GameObject submitBtnQuiz;
    [SerializeField] GameObject GradeText;
    [SerializeField] GameObject newQuizGameObj;
    [SerializeField] GameObject QuizExitBtn;
    GameObject gradeObject;
    [SerializeField] GameObject QuizStartButton;

    [SerializeField] GameObject LMSContent;
    [SerializeField] GameObject SectionNameItem;
    [SerializeField] GameObject ModuleItem;
    private string myToken;
    private bool inProgress = false;
    int complete = 0;
    private int initialLoad = 0;
    int curr = 0;
    List<string> Answers_Selected = new List<string>();
    List<List<GameObject>> listQuestOpts = new List<List<GameObject>>();

    List<int> LMSItemList = new List<int>();
    int LMSItemSelected = 0;    

    bool checkAni = false;

    string token3_11 = "";
    int  attemptID = 891;
    string uniID = "";
    

    string apiUrl = "";
    string apiSubmitUrl = "";
    string quizData = "";
    string apiStartQuiz = "";
    [SerializeField] string moodleAPIAddress;
    [SerializeField] string courseID;
    //changing quiz id from 71 to 74

    private void Start()
    {
        apiStartQuiz = moodleAPIAddress;
        apiUrl = moodleAPIAddress;
        apiSubmitUrl = moodleAPIAddress;
    }



    // currently being used //
    //             Function startQuizGetData()             //
    // this function will handle the start of a quiz attempt
    // getting the quiz attempt question data
    // parsing the data, and displaying the questions on the UI
    public async void startQuizGetData(int instance)
    {
       
        if(QuizStartButton.activeSelf == true)
        {
            QuizStartButton.SetActive(false);
        }
        
        if (token3_11 == "")
        {
            Debug.Log("no token yet selected");
            return;
        }
        else
        {
            //Debug.Log(token3_11);
        }
        apiStartQuiz += "" + token3_11 + "&wsfunction=mod_quiz_start_attempt&moodlewsrestformat=json&quizid="+instance;
        apiUrl += "" + token3_11 + "&wsfunction=mod_quiz_get_attempt_data&moodlewsrestformat=json&attemptid=";
        apiSubmitUrl += "" + token3_11 + "&wsfunction=mod_quiz_process_attempt&moodlewsrestformat=json&attemptid=";

         
       
        Debug.Log(apiStartQuiz);
        using var www = UnityWebRequest.Get(apiStartQuiz);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();
        try
        {
            if (StartQuizBtn.activeSelf == true)
            {
                StartQuizBtn.SetActive(false);
            }
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);



                inProgress = true;

                JObject jsonx = JObject.Parse(www.downloadHandler.text);
                //get attempt id, get unique id
                Debug.Log("start attempt");
                Debug.Log(jsonx.ToString());

                //catch any issues with attempt ie already active attempt

                if (jsonx["attempt"] == null)
                {
                    Debug.Log("error with quiz attempt");
                    return;
                }
                Debug.Log(jsonx["attempt"]["id"].ToString());
                attemptID = int.Parse(jsonx["attempt"]["id"].ToString());
                Debug.Log(attemptID);
                uniID = jsonx["attempt"]["uniqueid"].ToString();
                Debug.Log(uniID);

                apiUrl = apiUrl + attemptID + "&page=0";
                apiSubmitUrl = apiSubmitUrl + attemptID;
                Debug.Log(apiSubmitUrl);
                Debug.Log(apiUrl);

                TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);
 
                TestGet();
                //populate quiz with data
              
                //than show quiz UI

               if(newQuizGameObj.activeSelf == false)
                {
                    newQuizGameObj.SetActive(true);
                }

            }


            else
            {
                Debug.Log(www.result);
                if (QuizStartButton.activeSelf == false)
                {
                    QuizStartButton.SetActive(true);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            //if (gradeObj.activeSelf != false)
            //{
            //    gradeObj.SetActive(false);

            //}
            if (QuizStartButton.activeSelf == false)
            {
                QuizStartButton.SetActive(true);
            }
            if (StartQuizBtn.activeSelf == false)
            {
                StartQuizBtn.SetActive(true);
            }

        }

    }

    // currently being used //
    //course id = 2 //
    public async void getLMSContent()
    {
        if(token3_11 == "" )
        {
            return;
        }
     
        string url = moodleAPIAddress + token3_11+ "&wsfunction=core_course_get_contents&moodlewsrestformat=json&courseid="+courseID;
        
        Debug.Log(url);
        using UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();
        try
        {
            if (www.result == UnityWebRequest.Result.Success)
            {

                // use JArray since its a Json array
                Debug.Log(www.downloadHandler.text);
                JArray jsonx = JArray.Parse(www.downloadHandler.text);


                Debug.Log(jsonx[0].ToString());
                for(int i =0; i<=jsonx.Count -1 ; i++)
                {
                    GameObject sectionItemObj = Instantiate(SectionNameItem);
                    TMP_Text SectNameTxt = sectionItemObj.GetComponent<TMP_Text>();
                    if (SectNameTxt != null)
                    {
                        SectNameTxt.text = jsonx[i]["name"].ToString();
                    }
                    Debug.Log("count of modules: "+jsonx[i]["modules"].Count());
                    sectionItemObj.transform.SetParent(LMSContent.transform, false);
                    for(int j = 0; j <= jsonx[i]["modules"].Count() - 1 ; j++)
                    {
                        GameObject ItemInfoObj = Instantiate(ModuleItem);
                        Transform tModName = ItemInfoObj.transform.Find("ModuleName");
                        Transform tModType = ItemInfoObj.transform.Find("ModuleType");
                        TMP_Text ModType = tModType.gameObject.GetComponent<TMP_Text>();
                        TMP_Text ModName = tModName.gameObject.GetComponent<TMP_Text>();

                        ModType.text = jsonx[i]["modules"][j]["modname"].ToString();
                        ModName.text = jsonx[i]["modules"][j]["name"].ToString();

                        
                        ItemInfoObj.AddComponent<LMSItemSelected>();
                        LMSItemSelected itemScript = ItemInfoObj.GetComponent<LMSItemSelected>();
                        itemScript.setData(int.Parse(jsonx[i]["modules"][j]["instance"].ToString()), jsonx[i]["modules"][j]["modname"].ToString(), this);
                        ItemInfoObj.transform.SetParent(LMSContent.transform, false);
                        LMSItemList.Add(0);

                    }
                }
                //instantiate Section and Module

                

            }
            else
            {
                Debug.Log(www.result.ToString());
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    public bool getProgressStatus()
    {
         
        return (inProgress);
    }





    public void GetTokenFromHandler(string token)
    {
        token3_11 = token;
        Debug.Log("new token updated:" + token);
    }




    // currently being used //
    //Function TestGet() //
    //this function gets the quiz attempt data and parses through it

    public async void TestGet()
    {
        //Debug.Log(apiUrl);
        
        using var www = UnityWebRequest.Get(apiUrl);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if(www.result == UnityWebRequest.Result.Success)
        {
            
            if (gradeObject != null)
            {
                gradeObj.SetActive(false);
            }
            else
            {
                Debug.Log("grade object is null");
            }
            
            //Debug.Log("success: " + www.downloadHandler.text);
            Debug.Log("success: ");
            JObject jsonx = JObject.Parse(www.downloadHandler.text);
            Debug.Log(jsonx.ToString());
            string jsonVals = jsonx["questions"][0]["html"].ToString();
            //string type = jsonx["questions"][0]["type"].ToString();
            JArray NumofQ = jsonx["questions"] as JArray;
            Debug.Log("length: " + NumofQ.Count);
            //string[] typeArr = new string[3];
            //string[] Qnames = new string[3];

            List<string> typeArr = new List<string>(); 
            List<string> Qnames = new List<string>();
            int j = 0;
            foreach(JObject types in jsonx["questions"])
            {
                Selected_answers.Add(-1);
                typeArr.Add(types["type"].ToString());
                HtmlDocument htmlDocument = new HtmlDocument();
                string htmlloading = types["html"].ToString();
                htmlDocument.LoadHtml(htmlloading);

                
                var QuestionName = htmlDocument.DocumentNode.SelectNodes("//div[@class='qtext']/p/text()");

                if(QuestionName != null)
                {
                    Debug.Log("question: " + QuestionName[0].InnerText);
                    Qnames.Add( QuestionName[0].InnerText);
                }
                else
                {
                    QuestionName = htmlDocument.DocumentNode.SelectNodes("//div[@class='qtext']/text()");
                    Debug.Log("question: " + QuestionName[0].InnerText);
                    Qnames.Add( QuestionName[0].InnerText);
                }
                JObject qObject = new JObject();
                qObject["name"] = Qnames[j];
                quizdata["q"+j] = qObject;
                //var QuestionNames = htmlDoc.DocumentNode.SelectNodes("//div[@class='qtext']/p/text()");
                //Qnames[j] = QuestionName[0].InnerText;
                //if (QuestionName[0].InnerText != null)
                //{
                //    Qnames[j] = QuestionName[0].InnerText;
                //   Debug.Log("Qname: " + Qnames[j].ToString());
                //}
                //Debug.Log("question: " + QuestionName[0].InnerText);
                //Debug.Log(types["html"].ToString());
                j+= 1;


            }
            //Debug.Log(typeArr);
            //Debug.Log(Qnames);  

            //if multiple choice

            //if true/false

            //q1
            //  name
            //  type
            //  options...
            //q2

            //quizdata["q0"]["boom"] = "yeaaa";
            Debug.Log(quizdata.ToString());

            QDataSyncScript.CallCmdUpdateQList(attemptID, uniID, token3_11,quizdata.Count);

            j = 0;
           

            foreach (object x in quizdata)
            { 
                JObject optionsKey = new JObject();
                Debug.Log("type: " + typeArr[j]);
                if(typeArr[j] == "multichoice")
                {
                    quizdata["q" + j]["type"] = typeArr[j];

                    //qestion j
                    //get choices from html 
                    HtmlDocument htmlDocument = new HtmlDocument();
                    string htmlText = jsonx["questions"][j]["html"].ToString();
                    htmlDocument.LoadHtml(htmlText);
                   var questOpt = htmlDocument.DocumentNode.SelectNodes("//span[@class='answernumber']/following-sibling::div[1]");
                    int a = 0;
                    foreach (HtmlAgilityPack.HtmlNode opt in questOpt)
                    {
                        //quizdata["q" + j]["option"+a] = opt.InnerText;
                        optionsKey["option" + a] = opt.InnerText;
                        
                        a += 1;
                    }


                }

                if (typeArr[j] == "truefalse")
                {
                    quizdata["q" + j]["type"] = typeArr[j];
                    optionsKey["option" + 0] = "false";
                    optionsKey["option" + 1] = "true";
                    //quizdata["q" + j]["option" + 0] = "true";
                    //quizdata["q" + j]["option" + 1] = "false";
                }
                JObject optiondata = new JObject();
                optiondata = optionsKey;
                quizdata["q" + j]["options"] = optiondata;
                j += 1;
            }

            Debug.Log(quizdata.ToString());
            //Debug.Log(type)
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(jsonVals);
            //string Question
            var QuestionNames = htmlDoc.DocumentNode.SelectNodes("//div[@class='qtext']/text()");
            
            string[] QuestionOptions = new string[3];
            var qOptions = htmlDoc.DocumentNode.SelectNodes("//span[@class='answernumber']/following-sibling::div[1]");
            int i = 0;

            if(qOptions != null )
            {
             foreach (HtmlAgilityPack.HtmlNode opt in qOptions)
                        {
                            QuestionOptions[i] = opt.InnerText;
                            Debug.Log(QuestionOptions[i]);
                                i += 1;
                        }
            }


            //populateUIQuiz();
            popUIQuiz();
            if (StartQuizBtn.activeSelf == true)
            {
                StartQuizBtn.SetActive(false);
            }
        }
        else
        {
            Debug.Log("error: "+ www.error);
            if (QuizStartButton.activeSelf == false)
            {
                QuizStartButton.SetActive(true);
            }
        }

        //amount of questions
        //arrows to switch between questions
        //will page adjust based on text size amount?
        //
    }
 

  


    // currently being used //
    public void popUIQuiz()
    {
        if(Answers_Selected.Count > 0)
        {
            Answers_Selected.Clear();
        }
        for (int itr = 0; itr <= quizdata.Count - 1; itr++)
        {
            Answers_Selected.Add("");
            JObject questionInfo = (JObject)quizdata["q" + itr];
            // get quiz prompt
            GameObject qPrompt = Instantiate(quizPrompt);
            TMP_Text promptText = qPrompt.GetComponent<TMP_Text>();
            promptText.text = questionInfo["name"].ToString();
            //add to quiz content
            qPrompt.transform.SetParent(quizContent.transform, false);

            List<GameObject> OptionsList = new List<GameObject>();
            //GameObject opt1; 
            JObject OptObject = (JObject)questionInfo["options"];
            //determine type
            if (questionInfo["type"].ToString() == "truefalse")
            {
                // going through options add to main obj
                for (int itrOptions = 0; itrOptions <= OptObject.Count -1; itrOptions++)
                {
                   GameObject opt1 = Instantiate(quizMultiChoiceOption);
                    opt1.name = "Q" + itr + "_" + "opt" + itrOptions;
                    string xx = opt1.name;
                    TMP_Text opt1Text = opt1.gameObject.GetComponent<TMP_Text>();
                    opt1Text.text = OptObject["option"+itrOptions].ToString();

                    Transform childTransform = opt1.transform.Find("Button");
                    Button buttonComp = childTransform.gameObject.GetComponent<Button>();
                 
                    opt1.transform.SetParent(quizContent.transform, false);
                    OptionsList.Add(opt1);
                }
            }

            else if (questionInfo["type"].ToString() == "multichoice")
            {
                for (int itrOptions = 0; itrOptions <= OptObject.Count - 1; itrOptions++)
                {
                    GameObject opt1 = Instantiate(quizMultiChoiceOption);
                    opt1.name = "Q" + itr + "_" + "opt" + itrOptions;
                    TMP_Text opt1Text = opt1.gameObject.GetComponent<TMP_Text>();
                    opt1Text.text = OptObject["option" + itrOptions].ToString();

                    // add to quiz content
                    opt1.transform.SetParent(quizContent.transform, false);
                    OptionsList.Add(opt1);
                }

            }
            
            listQuestOpts.Add(OptionsList);

            // go through all options
            //put into parent obj

        }

        GameObject subBtn = Instantiate(submitBtnQuiz);
        subBtn.transform.SetParent(quizContent.transform, false);
        RectTransform parentRect = quizContent.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);

        // iterate through object list of lists 
        for (int i = 0; i<= listQuestOpts.Count-1; i++)
        {
            

            for (int j = 0; j <= listQuestOpts[i].Count-1; j++)
            {
                
                Debug.Log(listQuestOpts[i][j].ToString());

                Debug.Log(listQuestOpts[i][j].name.ToString());
            }
        }
       
    }

    //answer select
    //only able to select 1 option for a quiz at once can't select multiple
    //each option should be tied to the question


    //can i do a dictionary

    //submit button
    //iterate through each option to find selected answer


    //another list 
    //
    public void QuizOptionSelected(int quest, int OptionNum)
    {
        Debug.Log("quest #: "+ quest);
        Debug.Log("option #: " + OptionNum);

        //loop through options for each question

        for (int i = 0; i<= listQuestOpts[quest].Count-1; i++)
        {
            Debug.Log("list quest options: " + i);
            GameObject OptionQues = listQuestOpts[quest][i];
            Transform BtnObjTransform = OptionQues.transform.Find("Button");
            Transform txtBtnTransform = BtnObjTransform.Find("Text (TMP)");
            TMP_Text txtBtn = txtBtnTransform.gameObject.GetComponent<TMP_Text>();
            string checkText = txtBtn.text;
            if(checkText == "X")
            {
                if(i != OptionNum)
                {
                    txtBtn.text = "";
                    //update selectionanswer list
                }
            }
            Answers_Selected[quest]=OptionNum.ToString();
            
           






        }
        //submit
        //iterate through each option for each question
        //log selected option for each question

         
        //

    }
    public void populateUIQuiz ()
    {

        JObject xy = new JObject();

        int itrQ = 0;
        int itrO = 0;

        popUIQuiz();
        Debug.Log("quiz data: ");
        Debug.Log(quizdata.ToString());


        // get data
        JObject currQuestion = (JObject)quizdata["q" + curr];
        string nameTmptext = currQuestion["name"].ToString();
        string QuestionType = currQuestion["type"].ToString();
        TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
        JObject optionObject = (JObject) currQuestion["options"];

        int b = 0;
        tmpName.text = nameTmptext;

        foreach (var currOption in optionObject)
        {
            TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
            optxText.text = currOption.Value.ToString();
            if (qOptions[b].activeSelf == false)
            {
                qOptions[b].SetActive(true);
                checkBoxes[b].SetActive(true);
            }
            b++;
        }

        if (b!=3)
        {
            if (qOptions[2].activeSelf == true)
            {
                qOptions[2].SetActive(false);
                checkBoxes[2].SetActive(false);
            }
        }

        if (QuestionType == "multichoice")
        {
           


            //deactivate based on the b count
            // if 3 than all should be active
            //if 2 than last one should be deactivated ...
        }
        if(QuestionType == "truefalse")
        {

        }

        TMP_Text x = qOptions[curr].GetComponent<TMP_Text>();
        Debug.Log(nameTmptext);


        //determine what buttons to use
        //if(nextButton.activeSelf== false)
        //{
        //    nextButton.SetActive(true);
        //}
        //prevButton.SetActive(false);
        checkQuestions();
        //set items active if not already


        // type 
        //multiple choice
        //all three text and checkboxes are shown
        //when option is selected that item's index is added to answer array,for answer[curr] 


        //#num of questions
        //curr index
        //name
        //options
        //set items active

        quizItems.SetActive(true);
        
    }

    //select an answer

    public void choiceSelected(int a)
    {
        // add a x to the button
        //remove any other x's 
        //add value to answer list
        int bxCount = 0;
        foreach(var chkbx in checkBoxes)
            
        {
            TMP_Text checkText = checkBoxes[bxCount].GetComponentInChildren<TMP_Text>();
            if(bxCount == a)
            {
                
                checkText.text = "X";
            }
            else
            {
                checkText.text = "";
            }
            
            bxCount++;


        }

        Selected_answers[curr] = a;

        int counta = 0;
        foreach (var selectItems in Selected_answers)
        {
            Debug.Log("item "+ counta+ ": "+selectItems);

            counta++;
        }
       
       
       



        

    }

    public void checkQuestions()
    {
        //check if there is another question after this and/or before it ... if so than either show or don't show arrows.

        //check next question
        
        JObject currQuestionNxt = (JObject)quizdata["q" + curr + 1];
        if(currQuestionNxt != null)
        {
            //show next arrow
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }

        JObject currQuestionPrev = (JObject)quizdata["q" + (curr - 1)];
        if (currQuestionPrev != null)
        {
            //show previous arrow
            prevButton.SetActive(true);
        }
        else
        {
            prevButton.SetActive(false);
        }
        if(currQuestionNxt == null && currQuestionPrev == null)
        {
            submitBtn.SetActive(true);
        }
    }

    //move to next question press right arrow
    public void NextQuestion ()
    {
        //current index
        //check if there is another question,
        //if so, repopulate quiz with next question items
        //look at type
        //populate question items based on that

        //if not dont do anything
        //
        Debug.Log(quizdata.ToString());
        curr = curr + 1;
        JObject currQuestion = (JObject)quizdata["q" + curr];
        string nameTmptext = currQuestion["name"].ToString();
        string QuestionType = currQuestion["type"].ToString();
        TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
        JObject optionObject = (JObject)currQuestion["options"];

        int b = 0;
        tmpName.text = nameTmptext;

        //answered?

        foreach (var itemX in checkBoxes)

        {
            TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
            chkText.text = "";
        }

        if(Selected_answers[curr] != -1)
        {
            int item_selected = Selected_answers[curr];
            TMP_Text checkText = checkBoxes[item_selected].GetComponentInChildren<TMP_Text>();
            checkText.text = "X";
        }
        //TMP_Text checkText = checkBoxes[ Selected_answers[curr] ].GetComponentInChildren<TMP_Text>();
        //

        foreach (var currOption in optionObject)
        {
            TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
            optxText.text = currOption.Value.ToString();
            if (qOptions[b].activeSelf == false)
            {
                qOptions[b].SetActive(true);
                checkBoxes[b].SetActive(true);
            }
            b++;
        }

        //check if all fields are used, if not deactivate last one
        if (b != 3)
        {
            if (qOptions[2].activeSelf == true)
            {
                qOptions[2].SetActive(false);
                checkBoxes[2].SetActive(false);
            }
        }

        // are there any questions left

        if (quizdata["q"+curr+1] == null)
        {
            if(complete == 0 )
            {
                //check if all answers are correct 
                //if so make submit button active
            }
            submitBtn.SetActive(true);
            nextButton.SetActive(false);



            //deactivate next

        }
        //prev active?
        if(prevButton.activeSelf == false)
        {
            prevButton.SetActive(true);
        }



    }
    public void PrevQuestion()
    {
        //current index
        //check if there is another question, to go back to
        //if so, repopulate quiz with prev question items
        //look at type
        //populate question items based on that

        //if not dont do anything
        //

        if (curr != 0)
        {

        curr = curr - 1;
        JObject currQuestion = (JObject)quizdata["q" + curr];
        string nameTmptext = currQuestion["name"].ToString();
        string QuestionType = currQuestion["type"].ToString();
        TMP_Text tmpName = qNameUi.GetComponent<TMP_Text>();
        JObject optionObject = (JObject)currQuestion["options"];

        foreach (var itemX in checkBoxes)

        {
            TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
            chkText.text = "";
        }

        if(Selected_answers[curr] != -1)
            {
            TMP_Text checkText = checkBoxes[Selected_answers[curr]].GetComponentInChildren<TMP_Text>();
        checkText.text = "X";
            }
        

        int b = 0;
        tmpName.text = nameTmptext;
        foreach (var currOption in optionObject)
        {
            TMP_Text optxText = qOptions[b].GetComponent<TMP_Text>();
            optxText.text = currOption.Value.ToString();
            if (qOptions[b].activeSelf == false)
            {
                qOptions[b].SetActive(true);
                checkBoxes[b].SetActive(true);
            }
            b++;
        }

        if (b != 3)
        {
            if (qOptions[2].activeSelf == true)
            {
                qOptions[2].SetActive(false);
                checkBoxes[2].SetActive(false);
            }
        }


        if (curr <= 0 )
        {
            //change submit to submit quiz
            //deactivate next
            prevButton.SetActive(false);
        }

        if(nextButton.activeSelf == false)
        {
            nextButton.SetActive(true);
        }
        //prev active?

    }
    }
    //submit the quiz
    //mod_quiz_process_attempt


    //must have answer list populated with non negative numbers
    //answered each question



    public async void SubmitQuizDataNew()
    {
        int questCounter = 0;
        string seqCounter = "";
        string apiAdd = "";
        int itemCounter = 0;
        int checkAns = 0;
        foreach (string x in Answers_Selected)
        {
            Debug.Log(x);
            if( x == "" )
            {
                return;
            }
        }

        foreach (string itemSelect in Answers_Selected)
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
        Debug.Log(subString);

        string slots = "&data[" + itemCounter + "][name]=slots&data[" + itemCounter + "][value]=" + subString + "&finishattempt=1";

        apiAdd = apiAdd + slots;
        apiSubmitUrl = apiSubmitUrl + apiAdd;
        Debug.Log(apiSubmitUrl);


        using var www = UnityWebRequest.Get(apiSubmitUrl);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();


        if (www.result == UnityWebRequest.Result.Success)
        {
            inProgress = false;
            //TokenGiftScript.isQuizPending(inProgress);
            //TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);
            getNewQuizGrades();
            QDataSyncScript.CallCmdRemoveFromList();
            Debug.Log(www.result);

            // remove items from list


            listQuestOpts.Clear();

            //
            //getQuizGrades();

            

             //for testing w/ local prxy

             apiStartQuiz = moodleAPIAddress;
            apiUrl = moodleAPIAddress;
            apiSubmitUrl = moodleAPIAddress;

            curr = 0;
          
            
        }
        else
        {
            Debug.Log(www.error);
        }
    }


 

    public async void submitQuiz()
    {
       
        int checkAns = 0;
        foreach (var itemSelect in Selected_answers)
        {
            if(itemSelect == -1)
            {
                checkAns = 0;
                break;
            }
            else
            {
                checkAns = 1;
            }
        }
            int itemCounter = 0;
        int questCounter = 0;
       
        string apiAdd = "";


        if(checkAns == 1)
        {
            string seqCounter = "";

            foreach (var itemSelect in Selected_answers)
            {
                //need uniqueID
                //int uniqueID_ = 905;

                
                questCounter++;
                string Flag_1 ="&data["+itemCounter+"][name]=q"+ uniID+":"+questCounter+"_:flagged&data["+itemCounter+"][value]=0&" ;
                itemCounter++;
            
                string Flag_2 = "data["+itemCounter+"][name]=q"+uniID+":"+questCounter+ "_:flagged&data[" + itemCounter + "][value]=0&";
                itemCounter++;
                string questAns = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_answer&data[" + itemCounter + "][value]="+itemSelect+"&";
                itemCounter++;
                string seqChekc = "data[" + itemCounter + "][name]=q" + uniID + ":" + questCounter + "_:sequencecheck&data[" + itemCounter + "][value]=1";
                itemCounter++;

                seqCounter = seqCounter + questCounter.ToString() + ",";
                apiAdd = apiAdd + Flag_1+Flag_2+questAns+seqChekc;

            }
            string subString =  seqCounter.Substring(0,seqCounter.Length-1);
            Selected_answers.Clear();
            Debug.Log(subString);
            Debug.Log(Selected_answers);


            foreach (var itemX in checkBoxes)

            {
                TMP_Text chkText = itemX.GetComponentInChildren<TMP_Text>();
                chkText.text = "";
            }


            string slots = "&data["+itemCounter+"][name]=slots&data["+itemCounter+"][value]="+subString+"&finishattempt=1";


        
            apiAdd = apiAdd + slots;

        apiSubmitUrl = apiSubmitUrl + apiAdd;
            Debug.Log(apiSubmitUrl);

            using var www = UnityWebRequest.Get(apiSubmitUrl);
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "fd");
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.Success)
            {
                inProgress = false;
                //TokenGiftScript.isQuizPending(inProgress);
                TokenGiftScript.setIDtoPlayer(attemptID.ToString(), token3_11, uniID, inProgress);
                QDataSyncScript.CallCmdRemoveFromList();
                Debug.Log(www.result);

                //toggleObjectivesScript.taskCompleted(3);
                getQuizGrades();

        

                //for testing w/ local prxy

                apiStartQuiz = " ";
                apiUrl = " ";
                apiSubmitUrl = " ";

                curr = 0;
                //if (newQuizBtn.activeSelf == false)
                //{
                //    newQuizBtn.SetActive(true);

                //}
                submitBtn.SetActive(false);
                StartQuizBtn.SetActive(false);
            }
            else
            {
                Debug.Log(www.error);
            }
        }
        else
        {
            Debug.Log("answer all questions");
        }


    }
    public void quizUIHide()
    {
        if (quizDonebtn.activeSelf == true)
        {
            quizDonebtn.SetActive(false);
        }

        if (gradeObj.activeSelf == true)
        {
            gradeObj.SetActive(false);
        }

        if (quizUITR.activeSelf == true)
        {
            quizUITR.SetActive(false);
        }




    }

    //if exiting save attempt
    public void exitQuizWindow()
    {

        if(newQuizGameObj.activeSelf == true)
        { 
            newQuizGameObj.SetActive(false);
            QuizExitBtn.SetActive(false);
            if(gradeObject != null) 
            {
                Debug.Log("destroy Obj");
                GameObject.Destroy(gradeObject);
            }
        }
        if (QuizStartButton.activeSelf == false)
        {
            QuizStartButton.SetActive(true);
        }
    }

    
    async void viewQuizzes()
    {
        var url = "";

        var viewQuizzesRequest = url + "" + token3_11 + "&wsfunction=mod_quiz_get_attempt_review&moodlewsrestformat=json&attemptid=" + attemptID;
        using var www = UnityWebRequest.Get(viewQuizzesRequest);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();
    }

    private async void getNewQuizGrades()
    {
        
        //var url = "";
        string url = moodleAPIAddress;

        string attemptReview = url +  token3_11 + "&wsfunction=mod_quiz_get_attempt_review&moodlewsrestformat=json&attemptid=" + attemptID;
        Debug.Log(attemptReview);
        using var www = UnityWebRequest.Get(attemptReview);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
        {

            foreach (Transform child in quizContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }


            JObject jsonx = JObject.Parse(www.downloadHandler.text);
            //get attempt id, get unique id
            Debug.Log(jsonx.ToString());
            Debug.Log(jsonx["grade"].ToString());

            string quizGrade =  jsonx["grade"].ToString();
            //
            GameObject gradeObj = Instantiate(GradeText);
            
            TMP_Text gradeObjText = gradeObj.gameObject.GetComponent<TMP_Text>();
            gradeObjText.text = "Grade: " + quizGrade;

            // add to quiz content
            gradeObjText.transform.SetParent(quizContent.transform, false);
            gradeObject = gradeObj;
            //add grade object 
            // remove quiz items 
            
             
            //Debug.Log(quizGrade);
            //animatorComp.SetBool("celebration", false);





        }

        else
        {
            Debug.Log(www.result);
            GameObject gradeObj = Instantiate(GradeText);

            TMP_Text gradeObjText = gradeObj.gameObject.GetComponent<TMP_Text>();
            gradeObjText.text = "not able to provide grade yet";

            // add to quiz content
            gradeObjText.transform.SetParent(quizContent.transform, false);
            gradeObject = gradeObj;
        }
        QuizExitBtn.SetActive(true);
    }


    private async void getQuizGrades ()
    {

        var url = "";

        var attemptReview = url + "" + token3_11+"&wsfunction=mod_quiz_get_attempt_review&moodlewsrestformat=json&attemptid="+attemptID;
        using var www = UnityWebRequest.Get(attemptReview);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "fd");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
        {
            JObject jsonx = JObject.Parse(www.downloadHandler.text);
            //get attempt id, get unique id
            Debug.Log(jsonx.ToString());
            Debug.Log(jsonx["grade"].ToString());
            
            float quizGrade = float.Parse(jsonx["grade"].ToString());
            //toggleObjectivesScript.quizCompleted(3, quizGrade);
            quizItems.SetActive(false);
            if(gradeObj.activeSelf != true)
            {
                gradeObj.SetActive(true);
            }
            if (quizDonebtn.activeSelf == false)
            {

                quizDonebtn.SetActive(true);
            }
            TMP_Text gradeText = gradeObj.GetComponent<TMP_Text>();
            gradeText.text = "Grade: " + quizGrade.ToString();
            //Debug.Log(quizGrade);
            //animatorComp.SetBool("celebration", false);





        }

        else
        {
            Debug.Log(www.result);
        }
    }
  
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool quizInProgress()
    {
        return inProgress;
    }
    public int getAttemptID()
    {
        return attemptID;
    }
    public string getUniID()
    {
        return uniID;
    }

    public float getQuizResults(float quiz)
    {
        return 4.0f;
    }

}
