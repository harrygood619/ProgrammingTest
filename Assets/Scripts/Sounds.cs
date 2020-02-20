using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioSource Coin;                 // Audio to play when collecting a coin
    public static AudioSource CollapsingBuilding;   // Audio to play when the building collapses
    public static AudioSource Death;                // Audio to play when the player dies
    public static AudioSource Gameplay;             // Audio to play while in the game
    public static AudioSource Lose;                 // Audio to play during the lose screen
    public static AudioSource Win;                  // Audio to play during the win screen 

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Assign all the audio at the start of gameplay
        Coin = transform.GetChild(0).GetComponent<AudioSource>();
        CollapsingBuilding = transform.GetChild(1).GetComponent<AudioSource>();
        Death = transform.GetChild(2).GetComponent<AudioSource>();
        Gameplay = transform.GetChild(3).GetComponent<AudioSource>();
        Lose = transform.GetChild(4).GetComponent<AudioSource>();
        Win = transform.GetChild(5).GetComponent<AudioSource>();

    }
}
