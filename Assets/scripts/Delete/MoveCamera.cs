using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private float pitch = 0.0f; // Rotation around x-axis
    private float yaw = 0.0f; // Rotation around y-axis
    float sensitivity = 1.0f;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        //look();
    }

    void look ()
    {

        //float y = Input.GetAxis("lookVertical");
        //float x = Input.GetAxis("lookHorizontal");
        //Vector3 pos = transform.position + new Vector3(0,y,0) * 3;


        //pitch -= y * sensitivity; // Inverting 'y' to make pitch up look up
        //yaw += x * sensitivity;
        //pitch = Mathf.Clamp(pitch, -40f, 30f);
        //transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
       
        // Vector3 rotate = new Vector3(y,x, 0);

        //Vector3 checkAngles = transform.eulerAngles + rotate;
        // if(checkAngles.x>30 || checkAngles.x <-40)
        // {
        //     rotate.x = 0;
        // }
        // if (checkAngles.y > 10 || checkAngles.y <-10)
        // {
        //     rotate.y = 0;
        // }
        // transform.eulerAngles = transform.eulerAngles + rotate;
        //Debug.Log("transform: ", transform.eulerAngles.);
    }
}
