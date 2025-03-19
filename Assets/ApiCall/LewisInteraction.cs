using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewisInteraction : MonoBehaviour
{
    public GameObject gameManager,scrollView,inputField,introText;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //void OnMouseDown(){
    //    if (introText.activeSelf == true){
    //        gameManager.SetActive(true);
    //        scrollView.SetActive(true);
    //        inputField.SetActive(true);
    //        introText.SetActive(false);

    //        animator.SetBool("isTalking", true);
    //    }
    //    else if (introText.activeSelf == false){
    //        introText.SetActive(true);
    //        gameManager.SetActive(false);
    //        scrollView.SetActive(false);
    //        inputField.SetActive(false);
    //        animator.SetBool("isTalking", false);
    //    }
        
    //}

    public void letsTalk()
    {
        gameManager.SetActive(true);
        scrollView.SetActive(true);
        inputField.SetActive(true);
        introText.SetActive(false);

    }

    public void AIUIExit()
    {
        introText.SetActive(true);
        gameManager.SetActive(false);
        scrollView.SetActive(false);
        inputField.SetActive(false);

        animator.SetBool("isTalking", false);
    }
}
