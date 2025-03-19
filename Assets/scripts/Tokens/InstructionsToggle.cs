using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsToggle : MonoBehaviour
{

    public GameObject InstructMenu;
    public GameObject TokenMenu;
    public GameObject TokenLaterBtn;
    public void ToggleOff()
    {
        if (InstructMenu != null)
        {
            InstructMenu.SetActive(false);
        }
    }

    public void ToggleTokenMenu()
    {
        TokenMenu.SetActive(false);
        TokenLaterBtn.SetActive(true);
    }


    public void Toggle()
    {
        if(InstructMenu.activeSelf == false)
        {
            Debug.Log("false but now activate");
            InstructMenu.SetActive(true);
            return;
        }
        if (InstructMenu.activeSelf == true)
        {
            InstructMenu.SetActive(false);
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
