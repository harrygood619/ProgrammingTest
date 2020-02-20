using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;                     // The speed of the player
    public float Stamina = 100;             // The amount of stamina the player has (affects the ability to flap wings)
    private float FlyCooldownTimer;         // A timer used to handle the cooldown of the fly button

    public Image StaminaBar;                // Reference to the stamina bar in the UI
    public Image FlyRadial;                 // Reference to the fly radial in the UI

    private Rigidbody Rb;                   // Reference to the player's rigidody for apply certain physics to it

    public GameObject LoseScreen;           // Reference to the lose screen to show when the player dies

    public static bool HaveWon = false;     // Global bool to decide whether the player has won or is still playing
    private bool StartFlyCooldown = false;  // When false, the player can press the fly button, when true, the fly button will go on cooldown


    // Start is called before the first frame update
    void Start()
    {
        // Play sound
        Sounds.Gameplay.Play(0);
        // Assign the rigidbbody to the reference
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player hasn't won yet
        if (HaveWon == false)
        {
            // Use this to control the player's movement with both mouse and keyboard and a controller
            transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Yaw"), -Input.GetAxis("Horizontal"));
            /*
             * W / Left Joystick Forward    = Pitch Down
             * S / Left Joystick Backward   = Pitch Up
             * A / Left Joystick Left       = Roll Left
             * D / Left Joystick Right      = Roll Right
             * Q / Left Bumper              = Yaw Left
             * E / Right Bumper             = Yaw Right
             */

            // If the space key / A button / fly button is pressed
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                // If there's at least 10 stamina available
                if (Stamina >= 25)
                {
                    // If the flight button isn't on cooldown
                    if (StartFlyCooldown == false)
                    {
                        // Give speed a slight bump up
                        Speed += 25;
                        // Decrease stamina by 10
                        Stamina -= 25;
                        // Start the cooldown timer
                        StartFlyCooldown = true;
                    }
                }
            }

            // If the ESC key / Start button / quit buttton is pressed
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
            {
                // Quit the game
                Application.Quit();
            }

            // Load all the miscellaneous actions needed each update
            Misc();
        }
    }

    // Called independantly from Update to handle physics (called 50 times per second)
    void FixedUpdate()
    {
        // If the player hasn't won yet
        if (HaveWon == false)
        {
            // Apply an light artificial gravity to slowly push the player down if they are gliding straight forward
            Rb.AddForce(transform.up * -0.25f);
        }
    }

    // Called every frame to handle the flight physics
    void Flight()
    {
        // Adjust the speed based on the rotation from pitching and yawing
        Speed -= (transform.forward.y * 0.2f);
        // Apply the acceleration to the player's forward vector via rigidbody force
        transform.position += ((transform.forward * Speed) * Time.deltaTime);
    }

    // Called when the player is killed by colliding with the environment
    public void Death()
    {
        // If the player hasn't won yet
        if (HaveWon == false)
        {
            // Stop sound
            Sounds.Gameplay.Stop();
            // Play sound
            Sounds.Death.Play(0);
            // Make the camera and it's anchor no longer children of the player
            transform.GetChild(0).parent = transform.parent;
            // Activate the lose screen
            LoseScreen.SetActive(true);
            // Play sound
            Sounds.Lose.Play(0);
            // Change to loading a death screen and with a button to reset the game or exit to main menu
            Destroy(gameObject);
        }
    }

    // Called when the player reaches the finish line
    public void Win()
    {
        // Stop sound
        Sounds.Gameplay.Stop();
        // Play sound
        Sounds.Win.Play(0);
        // Set it globally that the player has won
        HaveWon = true;
        // Hide the player
        GetComponent<MeshRenderer>().enabled = false;
        // Turn off the player's collider
        GetComponent<BoxCollider>().enabled = false;
        // Remove the rigidbody
        Destroy(Rb);
    }

    // Called when this object collides with another's collider
    void OnCollisionEnter(Collision Collision)
    {
        // If the player collides with the floor or the environment
        if (Collision.transform.tag == "Floor" || Collision.transform.tag == "Environment" || Collision.transform.tag == "Debris")
        {
            // Kill the player
            Death();
        }
    }

    // Contains all the miscellaneous things that need to happen each update
    void Misc()
    {
        // If the player is still above the ground
        if (transform.position.y > 0)
        {
            // Load the flight function
            Flight();
        }
        // Else if the player's ended up underground
        else
        {
            Death();
        }

        // If speed goes below 10
        if (Speed < 10)
        {
            // Reset it back to 10
            Speed = 10;
        }

        // If stamina goes above 100
        if (Stamina > 100)
        {
            // Reset it back to 100
            Stamina = 100;
        }

        // If stamina is below 100
        if (Stamina < 100)
        {
            // Slowly raise it back up by 1 per second
            Stamina += (1 * Time.deltaTime);
        }

        // If the stamina bar's fill isn't the same as the player's stamina reduce to 1%
        if (StaminaBar.fillAmount != (Stamina * 0.01f))
        {
            // Set the stamina bar's fill accordingly
            StaminaBar.fillAmount = (Stamina * 0.01f);
        }



        // If the player has recently pressed the fly button and set of the fly cooldown
        if (StartFlyCooldown == true)
        {
            // If the cooldown timer is hasn't reached 4 seconds
            if (FlyCooldownTimer <= 4f)
            {
                // Increase it by one per second
                FlyCooldownTimer += (1 * Time.deltaTime);
            }

            // If the fly radial's fill isn't the same as the cooldown reduced by 25%
            if (FlyRadial.fillAmount != (FlyCooldownTimer * 0.25f))
            {
                // Set the fly radial's fill accordingly
                FlyRadial.fillAmount = (FlyCooldownTimer * 0.25f);
            }

            // If the cooldown has reached 4 seconds
            if (FlyCooldownTimer > 4f)
            {
                // Stop the cooldown period from preventing flight
                StartFlyCooldown = false;
            }
        }
        // Else if the fly button isn't on cooldown
        else
        {
            // If the timer hasn't been reset
            if (FlyCooldownTimer > 0)
            {
                // Set the fly radial's fill to 1 to avoid issues with the timer being reset to 0
                FlyRadial.fillAmount = 1;

                // Reset it to 0
                FlyCooldownTimer = 0;
            }
        }
    }
}
