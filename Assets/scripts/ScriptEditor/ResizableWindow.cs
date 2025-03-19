using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizableWindow : MonoBehaviour, IDragHandler
{
    public Vector2 minSize = new Vector2(100, 100);
    public Vector2 maxSize = new Vector2(800, 600);

    RectTransform rectTransform;
    RectTransform rectTransform_codeEd;
    RectTransform rectTransform_console;
    RectTransform rectTransform_canvas;
    public Canvas canvas;
    public GameObject panel3;
    public GameObject codeEditor;
    public GameObject console;
    private Vector2 OriginalAnchor;
    void Start(){
        //panel3 = GameObject.Find("Panel3");
        //rectTransform = panel3.GetComponentInParent<RectTransform>();
        rectTransform = panel3.GetComponentInParent<RectTransform>();
        //rectTransform_codeEd = codeEditor.GetComponentInParent<RectTransform>();
        //rectTransform_console = console.GetComponentInParent<RectTransform>();
        //rectTransform_canvas = canvas.GetComponentInParent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //rectTransform.sizeDelta = new Vector2(500f, 500f);
        OriginalAnchor = rectTransform.anchoredPosition;
        Debug.Log("dragging");
        Vector2 newSize = new Vector2(
            rectTransform.sizeDelta.x + eventData.delta.x,
            rectTransform.sizeDelta.y - eventData.delta.y
        );
        //Vector2 newSize_Code = new Vector2(
        //    rectTransform_codeEd.sizeDelta.x + eventData.delta.x,
        //    rectTransform_codeEd.sizeDelta.y - eventData.delta.y
        //);
        //Vector2 newSize_Console = new Vector2(
        //    rectTransform_console.sizeDelta.x + eventData.delta.x,
        //    rectTransform_console.sizeDelta.y - eventData.delta.y
        //);

        // Vector2 newSize_Canvas = new Vector2(
        //    rectTransform_canvas.sizeDelta.x + eventData.delta.x,
        //    rectTransform_canvas.sizeDelta.y - eventData.delta.y
        //);

        //newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
        //newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
        rectTransform.sizeDelta = newSize;
        //rectTransform.anchoredPosition = rectTransform.
        

        //newSize_Code.x = Mathf.Clamp(newSize_Code.x, minSize.x, maxSize.x);
        //newSize_Code.y = Mathf.Clamp(newSize_Code.y, minSize.y, maxSize.y);
        //rectTransform_codeEd.sizeDelta = newSize_Code;

        //newSize_Console.x = Mathf.Clamp(newSize_Console.x, minSize.x, maxSize.x);
        //newSize_Console.y = Mathf.Clamp(newSize_Console.y, minSize.y, maxSize.y);
        //rectTransform_console.sizeDelta = newSize_Console;

        //newSize_Canvas.x = Mathf.Clamp(newSize_Canvas.x, minSize.x, maxSize.x);
        //newSize_Canvas.y = Mathf.Clamp(newSize_Canvas.y, minSize.y, maxSize.y);
        //rectTransform_canvas.sizeDelta = newSize_Canvas;
    }
}
