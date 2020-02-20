using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // If the player presses R / the right joystick down
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown("joystick button 9"))
        {
            // Look behind the player
            transform.localRotation = new Quaternion(transform.localRotation.x, (transform.localRotation.y + 180), transform.localRotation.z, transform.localRotation.w);
        }
        // Else if the player releases R / the right joystick down
        else if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp("joystick button 9"))
        {
            // Look in front of the player again 
            transform.localRotation = new Quaternion(transform.localRotation.x, 0, transform.localRotation.z, transform.localRotation.w);
        }
    }
}
