using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotateCamera : MonoBehaviour
{

    public float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotateCam();
    }

    void rotateCam()
    {
       float horizontal = Input.GetAxis("Horizontal_Camera"); 
        if(Input.GetKey(KeyCode.A) == true | Input.GetKey(KeyCode.D))
        {

            Debug.Log("rotate cam value: "+horizontal);
        Vector3 Rotation = new Vector3 (0f,horizontal*rotateSpeed, 0f);
        transform.eulerAngles = Rotation+ transform.eulerAngles;
        }

        
        
    }
}
