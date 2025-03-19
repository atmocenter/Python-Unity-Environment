using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DynamicInputWidth : MonoBehaviour
{
    public TMP_InputField tmpInputField;
    public float padding = 10f; // Extra space around the text
    public float stdSize = 365f;
    private RectTransform rectTransform;
    private TMP_Text textComponent;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent = tmpInputField.textComponent;
        tmpInputField.onValueChanged.AddListener(OnTextChanged);
        checkIPField();
    }

    public TMP_InputField ipField;
    // Start is called before the first frame update
   

    void OnTextChanged(string text)
    {
        checkIPField();

    }
    public void checkIPField()
    {
        if (textComponent != null)
        {
            if(textComponent.preferredWidth >stdSize )
            {
                // Calculate the preferred width of the text

                float preferredWidth = textComponent.preferredWidth+padding ;
               
                Debug.Log("preferred width: " + preferredWidth);
                // Set the width of the Input Field
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            }
            else if(textComponent.preferredWidth > stdSize-100f && textComponent.preferredWidth <stdSize)
            {
                padding += 1f;
                Debug.Log("preferred width: " + textComponent.preferredWidth);
                // Set the width of the Input Field
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stdSize+padding);
            }
            else if (textComponent.preferredWidth <stdSize-100f)
            {
                Debug.Log("preferred width <160: " + textComponent.preferredWidth);
                // Set the width of the Input Field
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stdSize);
            }

            
        }
        else
        {
            Debug.Log("texcomponent == null");
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
