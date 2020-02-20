using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreTextCoin;      // The score the player has achieved by collecting coins after winning
    public Text ScoreTextPoint;     // The score the player has achieved by collecting points after winning
    public Text ScoreTextTime;      // The score the player has achieved based on the time played after winning
    public Text ScoreTextTotal;     // The total score the player has achieved
    public Text ScoreTextRank;      // The rank the player has achieved based on their score
    public Text GameTimerText;      // The timer of game time played written on the UI
    public Text GameScoreText;      // The score the player has achieved from collecting coins written on the UI

    public GameObject WinScreen;    // The screen to show when the player has won
    public GameObject MiniMap;      // Reference to the minimap to hide it when the player wins
    public GameObject StaminaBar;   // Reference to the stamina bar to hide it when the player wins
    public GameObject FlyRadial;    // Reference to the fly radial to hite it when the player 

    public static int PlayerScore;  // The score the player has gained from collecting coins
    public static int PlayerCoins;  // The number of coins the player has collected
    public float GameTimer;         // The amount of time the player has been playing

    // Update is called once per frame
    void Update()
    {
        // If the game hasn't been won
        if (Player.HaveWon == false)
        {
            // Increase the timer by 1 per second
            GameTimer += (1 * Time.deltaTime);

            // If the timer UI element isn't the same as the timer rounded to an int
            if (Mathf.RoundToInt(GameTimer).ToString() != GameTimerText.text)
            {
                // Update it to show the number
                GameTimerText.text = Mathf.RoundToInt(GameTimer).ToString();
            }

            // If the score UI element isn't the same as the score handled by this script
            if (PlayerScore.ToString() != GameScoreText.text)
            {
                // Update it to show the number
                GameScoreText.text = PlayerScore.ToString();
            }
        }
        // Else if the player has won
        else
        {
            // If the minimap is active
            if (MiniMap.activeSelf == true)
            {
                // Deactivate it
                MiniMap.SetActive(false);
            }
            // If the stamina bar is active
            if (StaminaBar.activeSelf == true)
            {
                // Deactivate it
                StaminaBar.SetActive(false);
            }
            // If the fly radial is active
            if (FlyRadial.activeSelf == true)
            {
                // Deactivate it
                FlyRadial.SetActive(false);
            }
            // If the UI timer is active
            if (GameTimerText.gameObject.activeSelf == true)
            {
                // Deactivate it
                GameTimerText.gameObject.SetActive(false);
            }
            // If the UI score is active
            if (GameScoreText.gameObject.activeSelf == true)
            {
                // Deactivate it
                GameScoreText.gameObject.SetActive(false);
            }
            // If the win screen is inactive
            if (WinScreen.activeSelf == false)
            {
                // Activate it
                WinScreen.SetActive(true);
            }
            // Set the final point score
            ScoreTextCoin.text = ("Coins Collected					-               " + PlayerCoins);
            // Set the final amount of coins collected
            ScoreTextPoint.text = ("Score Collected					-               " + PlayerScore);
            // Set the amount of time as a score
            ScoreTextTime.text = ("Time Taken							-               -" + ((Mathf.RoundToInt(GameTimer) * 10) / 2));
            // Create the total
            int Total = ((((PlayerCoins * 10) * 2) + PlayerScore) - ((Mathf.RoundToInt(GameTimer) * 10) / 2));
            // Set the total
            ScoreTextTotal.text = ("Total										-               " + Total);
            
            // If the score is less than than 1000
            if (Total <= 1000)
            {
                // Get an E rank
                ScoreTextRank.text = ("Rank: E");
            }
            // If the score is less than 1500 but greater than 1000
            else if (Total <= 1500 && Total > 1000)
            {
                // Get an D rank
                ScoreTextRank.text = ("Rank: D");
            }
            // If the score is less than 3000 but greater than 1500
            else if (Total <= 3000 && Total > 1500)
            {
                // Get an C rank
                ScoreTextRank.text = ("Rank: C");
            }
            // If the score is less than 5000 but greater than 3000
            else if (Total <= 5000 && Total > 3000)
            {
                // Get an B rank
                ScoreTextRank.text = ("Rank: B");
            }
            // If the score is less than 7500 but greater than 5000
            else if (Total <= 7500 && Total > 5000)
            {
                // Get an A rank
                ScoreTextRank.text = ("Rank: A");
            }
            // If the score is greater than 7500
            else if (Total > 7500)
            {
                // Get an S rank
                ScoreTextRank.text = ("Rank: S");
            }
        }
    }
}
