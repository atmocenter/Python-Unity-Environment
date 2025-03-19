using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTilt : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 0f;
    [SerializeField] float orbitDamping = 10f;
    Vector3 localRot;

    public float minClamp = 0f;
    public float maxClamp = 3f;

    private FixedJoystick joystick_Cam;
    
    float jstkX = 0f;
    float jstkY = 0f;

    float pastYValue = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject joystickCamObj = GameObject.Find("Fixed Joystick Cam");
        joystick_Cam = joystickCamObj.GetComponent<FixedJoystick>();

    }



    // Update is called once per frame
    void Update()
    {
        pitchCam();
    
    }

    void pitchCam()
    {
        float CamInputVerticalRaw = Input.GetAxisRaw("lookVertical");
        float CamInputVertical = Input.GetAxis("lookVertical");
        CamInputVertical += joystick_Cam.Vertical;
        Vector3 TiltCam = new Vector3(0f, CamInputVertical * speed, 0f);
        float updateYvalue = 0f;
        updateYvalue = Mathf.Clamp(transform.position.y + TiltCam.y * Time.deltaTime, minClamp, maxClamp);
        transform.position = new Vector3(transform.position.x, updateYvalue, transform.position.z);
        //Camera.main.transform.LookAt(player);

        transform.LookAt(player);
    }
 
        



    
}
