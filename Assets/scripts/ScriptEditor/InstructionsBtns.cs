using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsBtns : MonoBehaviour
{
    // Start is called before the first frame update
    //btns
    public Button pyBtn;
    public Button GenInsBtn;
    public Button CookBtn;
    public GameObject PyInst;
    public GameObject GenInst;
    public GameObject CookInst;

    void Start()

    {
     //GenInsBtn.enabled = false;
        
    }
    public void PySelected()
    {
        CookBtn.enabled = true;
        pyBtn.enabled = false;
        GenInsBtn.enabled = true;
        GenInst.SetActive(false);
        PyInst.SetActive(true);
        CookInst.SetActive(false);

    }
    public void GenSelected()
    {
        CookBtn.enabled = true;
        pyBtn.enabled = true;
        GenInsBtn.enabled = false;
        GenInst.SetActive(true);
        PyInst.SetActive(false);
        CookInst.SetActive(false);
        //disable py instructions if enabled

    }

    public void CookSelected()
    {
        CookBtn.enabled = false;
        pyBtn.enabled = true;
        GenInsBtn.enabled = true;
        
        GenInst.SetActive(false);
        PyInst.SetActive(false);
        CookInst.SetActive(true);
        //disable py instructions if enabled

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
