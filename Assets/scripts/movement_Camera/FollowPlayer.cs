using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float speed = 0f;
    [SerializeField] float orbitDamping = 10f;
    Vector3 localRot;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - Player.position;

        // Initialize localRot based on the current rotation to maintain it
        localRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        //// Update rotation based on input
        //localRot.x += Input.GetAxis("Orbit_Horizontal") * speed;
        //localRot.y -= Input.GetAxis("Orbit_Vertical") * speed;

        //localRot.y = Mathf.Clamp(localRot.y, 0f, 80f);

        //Quaternion QT = Quaternion.Euler(localRot.y, localRot.x, 0f);
        //// Apply rotation to the offset
        //Vector3 rotatedOffset = QT * offset;
        //transform.position = Player.position + rotatedOffset;
        //transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * orbitDamping);

        //transform.position = Player.position;
        //localRot.x += Input.GetAxis("Orbit_Horizontal");
        //localRot.y -= Input.GetAxis("Orbit_Vertical");

        //localRot.y = Mathf.Clamp(localRot.y, 0f, 80f);

        //Quaternion QT = Quaternion.Euler(localRot.x, localRot.y, 0f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * orbitDamping);
    }

    void Look()
    {
        float CamInputHorizontal = Input.GetAxis("Orbit_Horizontal");
        float CamInputVertical = Input.GetAxis("Orbit_Vertical");

        Vector3 rotate = new Vector3(0f, CamInputHorizontal * speed, 0f);
        transform.eulerAngles = transform.eulerAngles+ rotate *Time.deltaTime;


        //move cam transform y axis
        //Vector3 TiltCam = new Vector3(0f, CamInputVertical * speed, 0f);
        //transform.position = transform.position + TiltCam*Time.deltaTime;
        //transform.LookAt(Player.transform);
        //look at player
    }
}
