using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Player PlayerScript;     // Reference to the player script

    // Called when an object enter's this objects trigger collider
    void OnTriggerEnter(Collider Other)
    {
        // If the player flies through the finish line
        if (Other.tag == "Player")
        {
            // Show the win screen
            PlayerScript.Win();
        }
    }
}
