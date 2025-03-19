using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
     public Canvas canvas;
     private RectTransform rectTransform;
    public RectTransform rct;
    
    void Start(){
        //rectTransform = GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData){
        rct.anchoredPosition += eventData.delta;
    }

}
