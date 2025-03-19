using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AvatarSelected : MonoBehaviour, IPointerClickHandler
{
    public int index = 0;

    public PlayerSelect playerSelectScript;
    
    // OnPointerClick () //
    // When avatar is clicked on the avatar selection menu the event is triggered // 
    public void OnPointerClick(PointerEventData pointereventdata)
    {
        
        playerSelectScript.AvatarSelectedClicked(index);

    }


}
