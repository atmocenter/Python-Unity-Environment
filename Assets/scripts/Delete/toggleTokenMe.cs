using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleTokenMe : MonoBehaviour
{

    public GameObject TokenMenu;

    public GameObject TokenMbtn;
    public void ToggleMenu ()
    {

        if (TokenMenu.activeSelf == true)
        {
            TokenMenu.SetActive (false);
            TokenMbtn.SetActive (true);
            return;
        }

        if(TokenMenu.activeSelf == false)
        {
            TokenMenu.SetActive(true);
            TokenMbtn.SetActive(false);
            return;
        }

    }

}
