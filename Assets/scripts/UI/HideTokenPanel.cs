using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTokenPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePanel()
    {
        if (Panel != null)
        {
            if(Panel.activeSelf==true)
            {
                Panel.SetActive(false);
            }
            else
            {
                Panel.SetActive(true);
            }
        }
    }
}
