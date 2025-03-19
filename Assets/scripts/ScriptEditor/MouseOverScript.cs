using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{

    GameObject UIwindow;
    
    void Start(){
        UIwindow = GameObject.Find("Window");
        UIwindow.SetActive(false);
    }
    void OnMouseOver()
    {
        UIwindow.SetActive(true);
    }

    void OnMouseExit()
    {
        UIwindow.SetActive(false);
    }
}
