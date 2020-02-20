using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Timer = 0;                 // Use to count down to the debris

    public bool StartExploding = false;     // Use this to trigger the explosion once in the FixedUpdate when the container is in the correct position

    // Called independantly from Update to handle physics (called 50 times per second)
    void FixedUpdate()
    {
        // If the container is positioned correctly and is ready to blow
        if (StartExploding == true)
        {
            // Use a for loop to go through all the children and make them explode
            for (int i = 0; i < transform.childCount; i++)
            {
                // Add explosive force to this child
                transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(2, transform.position, 1, 1);
            }
            // Stop the contianer from exploding the children again
            StartExploding = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the timer by 1 every second
        Timer += (1 * Time.deltaTime);

        // If the timer reaches 10 seconds but is still below 15 seconds
        if (Timer >= 10f && Timer < 15f)
        {
            // Use a for loop to go through all the children and shrink them gradually
            for (int i = 0; i < transform.childCount; i++)
            {
                // If the current child isn't null
                if (transform.GetChild(i) != null)
                {
                    // Create a temporary reference to the child's transform
                    Transform TempTransform = transform.GetChild(i).transform;

                    // If the scale of the debris isn't going to invert into a negative in any axis
                    if (TempTransform.localScale.x > 0 && TempTransform.localScale.y > 0 && TempTransform.localScale.z > 0)
                    {
                        // Reduce the child's scale
                        TempTransform.localScale = new Vector3((TempTransform.localScale.x - 0.2f), (TempTransform.localScale.y - 0.2f), (TempTransform.localScale.z - 0.2f));
                    }
                }
            }
        }
        // Else if the timer reaches 15 seconds
        else if (Timer >= 15f)
        {
            // Use a for loop to go through all the children and destory them
            for (int i = 0; i < transform.childCount; i++)
            {
                // If the current child isn't null
                if (transform.GetChild(i) != null)
                {
                    // Destory the child
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            // Destory the container object of the debris (this)
            Destroy(gameObject);
        }
    }
}
