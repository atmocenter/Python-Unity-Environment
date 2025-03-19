using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitQuizBtn : MonoBehaviour
{
    httpRequest httpReqScript;
    // Start is called before the first frame update
    void Start()
    {
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
    public void SubmitBtnPressed()
    {
        if (httpReqScript != null)
        {
            httpReqScript.SubmitQuizDataNew();
        }
    }
}
