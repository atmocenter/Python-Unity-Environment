using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionBtnClicked : MonoBehaviour
{
    httpRequest httpReqScript;
    public TMP_Text btnText;
    // Start is called before the first frame update
    void Start()
    {
        //create httpRequest Object

        GameObject qzboardObj = GameObject.Find("QuizBoard");
        if (qzboardObj != null)
        {
            httpReqScript = qzboardObj.GetComponent<httpRequest>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optionSelected()
    {
        string name = transform.parent.gameObject.name;
        string ques = name.Substring(1, 1);
        string optTxt = name.Substring(6, 1);
        int quesNum = int.Parse(ques);
        int optNum = int.Parse(optTxt);
        
        httpReqScript.QuizOptionSelected(quesNum, optNum);
        btnText.text = "X";
        //get question
        //get option
        //
    }
}
