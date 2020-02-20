using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public List<GameObject> Coins = new List<GameObject>(); // List of all coins

    // Update is called once per frame
    void Update()
    {
        // If there's at least one coin in the list
        if (Coins.Count > 0)
        {
            // Loop through all the coins in the scene
            for (int i = 0; i < Coins.Count; i++)
            {
                // Rotate them
                Coins[i].transform.Rotate(Vector3.forward * 50 * Time.deltaTime);
            }
        }
    }
}
