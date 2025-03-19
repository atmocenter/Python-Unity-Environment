using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TglMic : MonoBehaviour
{

    [SerializeField] TMP_Text tglText;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void tglMic()
    {
        if(toggle == true)
        {
            tglText.text = "unmute";
            toggle = false;
        }
        else
        {
            tglText.text = "mute";
            toggle = true;
        }

    }
}
