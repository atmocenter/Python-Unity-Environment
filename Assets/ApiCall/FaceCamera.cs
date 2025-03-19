using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        // If no camera is assigned, use the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Make the UI element face the camera
        //transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,mainCamera.transform.rotation * Vector3.up);
    }
}
