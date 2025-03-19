using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Dissonance;
using UnityEngine.EventSystems;
using TMPro;
public class MuteVoice : MonoBehaviour, IPointerClickHandler
{

    //public VoiceBroadcastTrigger triggerScript;
  
    bool muted = false;
    public TMP_Text muteText;
    //C88F82 mute color
    // Start is called before the first frame update
    void Start()
    {
        //muted = triggerScript.IsMuted;
        //if (muted)
        //{
        //    muteText.text = "Un-Mute";
        //}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void ToggleMicMute()
    //{
    //    triggerScript.ToggleMute();
    //    Debug.Log(triggerScript.IsMuted);
    //    if (triggerScript.IsMuted)
    //    {
    //        muteText.text = "Un-Mute";
    //        muteText.color = Color.red;
    //    }
    //    else
    //    {
    //        muteText.text = "Mute";
    //        muteText.color = Color.black;
    //    }
    //}

    public void OnPointerClick(PointerEventData pointereventdata)
    {
        Debug.Log(pointereventdata);
        //ToggleMicMute();

    }
}
