using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;   // Reference to the player's transforms

    public bool ShouldRotate;           // Use to decide if the following object should also an axis of rotation

    // Late Update is called at the end of the frame
    void LateUpdate()
    {
        // If the player's transform isn't null
        if (PlayerTransform != null)
        {
            // If the minimap isn't at the same x and z position as the player
            if (transform.position != new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z))
            {
                // Move it to the correct position
                transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
            }

            // If the following object should also rotate (i.e. minimap icons)
            if (ShouldRotate == true)
            {
                // If their forward transform is different to the player's
                if (transform.forward != PlayerTransform.forward)
                {
                    // Make it the same
                    transform.forward = PlayerTransform.forward;
                    // Then reset all the axis apart from Y
                    transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                }
            }
        }
    }
}
