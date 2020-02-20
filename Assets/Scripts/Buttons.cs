using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject HowToPlayMenu;        // Reference to the how to play menu within the main menu

    // If the quit game button is pressed in a menu
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    // If the start game button is pressed in a menu
    public void StartGame()
    {
        // Make sure the player's win screen doesn't show when restarting or going back to the main menu before restarting
        Player.HaveWon = false;
        // load the game scene
        SceneManager.LoadScene("Game");
    }

    // If the how to play button is pressed in a menu
    public void HowToPlay()
    {
        // If the menu is assigned
        if (HowToPlayMenu != null)
        {
            // Enable the menu
            HowToPlayMenu.SetActive(true);
        }
    }

    // If the load to main menu button is pressed in a menu
    public void LoadMainMenu()
    {
        // load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    // If the return to main menu button is pressed in a menu
    public void ReturnToMainMenu()
    {
        // If the menu is assigned
        if (HowToPlayMenu != null)
        {
            // Disable the menu
            HowToPlayMenu.SetActive(false);
        }
    }
}
