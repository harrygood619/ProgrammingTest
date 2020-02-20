using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public Transform PlayerTransform;       // Reference to the player's transforms
    public Transform ExplosionParent;       // Reference to the parent of the exploding building
    public Transform HorizonTransform;      // Reference to the transform the dragon will fly to after the player dies / wins

    public GameObject ExplodingBuilding;    // Reference to the exploding building prefab

    public float Speed = 60;                 // The speed of the dragon

    // Update is called once per frame
    void Update()
    {
        // If there is alive and if the player hasn't won
        if (PlayerTransform != null && Player.HaveWon == false)
        {
            // Fly forward
            Flight();
            // Rotate toward the player
            TargetPlayer(PlayerTransform);
        }
        // Else if the player's dead or has won
        else if (PlayerTransform == null || Player.HaveWon == true)
        {
            // Fly forward
            Flight();
            // Rotate toward the horizon
            TargetPlayer(HorizonTransform);
        }
        // Load all the miscellaneous actions needed each update
        Misc();
    }

    // Called every frame to handle the flight physics
    void Flight()
    {
        // Adjust the speed based on the rotation from pitching and yawing
        Speed -= (transform.forward.y * 0.1f);
        // Apply the acceleration to the player's forward vector via rigidbody force
        transform.position += ((transform.forward * Speed) * Time.deltaTime);
    }

    // Called every frame to handle looking toward the player
    void TargetPlayer(Transform TargetTransform)
    {
        // Determine which direction to rotate towards
        Vector3 TargetDirection = (TargetTransform.position - transform.position);
        // The step size is equal to speed times frame time.
        float SingleStep = ((Speed * 0.005f) * Time.deltaTime);
        // Rotate the forward vector towards the target direction by one step
        Vector3 Target = Vector3.RotateTowards(transform.forward, TargetDirection, SingleStep, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(Target);
    }

    // Called when an object enter's this objects trigger collider
    void OnTriggerEnter(Collider Other)
    {
        // If the dragon collides with the player
        if (Other.transform.tag == "Player")
        {
            // Activate the player's death function
            Other.transform.GetComponent<Player>().Death();
        }
        // Else if the dragon collides with a building/part of the environment
        else if (Other.transform.tag == "Environment")
        {
            // Play sound
            Sounds.CollapsingBuilding.Play(0);
            // Reduce the dragons speed slightly
            Speed -= 5f;
            // Save the position of the building
            Vector3 TempPosition = Other.transform.position;
            // Save the rotation of the building with the offset needed to spawn the explosion in the right orientation
            Quaternion TempRotation = new Quaternion((Other.transform.rotation.x + 90), Other.transform.rotation.y, Other.transform.rotation.z, Other.transform.rotation.w);
            // Destory the piece of environment hit
            Destroy(Other.gameObject);
            // Instantiate it's debris
            GameObject TempExplosion = Instantiate(ExplodingBuilding, Vector3.zero, Quaternion.identity, ExplosionParent);
            // Move the devris container to the correct position
            TempExplosion.transform.position = TempPosition;
            // Allow the container to begin exploding
            TempExplosion.GetComponent<Explosion>().StartExploding = true;
        }
    }
    
    // Contains all the miscellaneous things that need to happen each update
    void Misc()
    {
        // If the dragon gets too close to/hit's the floor
        if (transform.position.y < 1)
        {
            // Reset it's rotation to 0,0,0
            transform.rotation = Quaternion.identity;
            // Force it's position to stay at 1 or above
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        // If speed goes below 30
        if (Speed < 30)
        {
            // Reset it back up to 30 
            Speed = 30;
        }

        // if speed is below 200
        if (Speed < 200)
        {
            // Give the dragon bonus speed per frame
            Speed += 0.03f;
        }
    }
}
