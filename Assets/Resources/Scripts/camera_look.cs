using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_look : MonoBehaviour
 {
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public bool toggle=true;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown("space"))
        {
            toggle = !toggle;
        }

        if (toggle)
        {
            transform.eulerAngles = new Vector3(18 + pitch, 180 + yaw, 0.0f);
        }
       
    }
}



