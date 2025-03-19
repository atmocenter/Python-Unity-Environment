using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class ColorChange : MonoBehaviour
{

    [SerializeField] TMP_InputField inputObj;
    bool FlagText = false;
    int txt = 3;

    private string[] pythonKeywords = new string[]
    {
        "def", "class", "if", "else", "elif", "return", "import", "for", "while",
        "try", "except", "finally", "with", "as", "lambda", "from", "global", "nonlocal", "assert"
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueHasChanged(string text)
    {
        Debug.Log(text);
        if (FlagText == false)
        {
            FlagText = true;
            string code = HighlightSyntax(text);
            inputObj.text = code;
            
        }
        else
        {
            FlagText= false;
        }
        
        //inputObj.text = "ge" + inputObj.text;
    }


    string HighlightSyntax(string code)
    {
        
        //code = Regex.Replace(code, @"\n</color>", "</color>\n");
        
        // Highlight Python keywords
        foreach (var keyword in pythonKeywords)
        {
            code = Regex.Replace(code, $@"\b{keyword}\b", $"<color=#00BFFF>{keyword}</color>");
        }

        // Handle strings (either single quotes or double quotes)
        code = Regex.Replace(code, "(\".*?\"|'.*?')", "<color=#FF4500>$&</color>");

        // Handle comments (starting with #), but ignore those inside <color=...>
        code = Regex.Replace(code, @"(?<!<color=.*?)#.*", "<color=#008000>$&</color>");

        // This regex will look for the new line character and add closing color tags before it
        


        return code;
    }

}
