using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int CoinScore;               // The score value of the coin

    private Player PlayerScript;        // Reference to the player script

    private CoinRotate ParentScript;    // Reference to the coin rotate script

    // Start is called before the first frame update
    void Start()
    {
        // If the player script is unassigned
        if (PlayerScript == null)
        {
            // Find the player and assign it
            PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        // If the coin rotate script is unassigned
        if (ParentScript == null)
        {
            // Assign it
            ParentScript = transform.parent.GetComponent<CoinRotate>();
        }

        // If this coin isn't in the coin list
        if (!ParentScript.Coins.Contains(gameObject))
        {
            // Add it
            ParentScript.Coins.Add(gameObject);
        }
    }

    // Called when an object enter's this objects trigger collider
    void OnTriggerEnter(Collider Other)
    {
        // If the player flies through the finish line
        if (Other.tag == "Player")
        {
            // Play sound
            Sounds.Coin.Play(0);
            // If this coin is in the coin list
            if (ParentScript.Coins.Contains(gameObject))
            {
                // Remove it
                ParentScript.Coins.Remove(gameObject);
            }
            // Increase the score the player has collected by the value of the coin
            Score.PlayerScore += CoinScore;
            // Increase the number of coins the player has collected by 1
            Score.PlayerCoins += 1;
            // Give the player a speed boost
            PlayerScript.Speed += 2.5f;
            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
