using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerConsole : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject CompScreen;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click detected");
        //trigger code
        if (CompScreen.activeSelf == false)
        {
           
            CompScreen.SetActive(true);
        }
        CompScreen.SetActive(true);


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
