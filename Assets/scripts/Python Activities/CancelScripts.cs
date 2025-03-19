using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelScripts 
{
    public event Action CancelTheUserScript;

    public void triggerEvent()
    {
        CancelTheUserScript?.Invoke();
        Debug.Log("trigger Event happening");
    }
    public void triggerMsg()
    {
        Debug.Log("msg Triggered");
    }


}
