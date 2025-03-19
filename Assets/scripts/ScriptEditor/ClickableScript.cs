using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    GameObject UI;
    
    void Start(){
        UI = GameObject.Find("Window3");
        UI.SetActive(false);
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)){
            UI.SetActive(true);
        }
    }
}
