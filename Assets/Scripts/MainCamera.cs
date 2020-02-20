using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform PlayerTransform;  // Reference to the player's transforms
    private Transform CameraAnchor;     // Reference to the camera anchor

    private Vector3 MoveCamera;         // A vector to alter the camera's position based on the player's roll/pitch/yaw and their speed

    private float Bias = 0.96f;         // A bias value to create more fluid angles and movements

    // Start is called before the first frame update
    void Start()
    {
        // Assign the transform reference of the player
        PlayerTransform = transform.parent.parent;
        // Assign the transform reference of the camera anchor
        CameraAnchor = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player's transform isn't null
        if (PlayerTransform != null)
        {
            // Setup the move vector based on the player's positon, forward direction and the up value of the world
            MoveCamera = PlayerTransform.position - PlayerTransform.forward * 24f + Vector3.up * 5f;
            // Move the camera to the move vector based on the camera's exisiting position with the bias modifier
            transform.position = transform.position * Bias + MoveCamera * (1 - Bias);
        }
    }
}
