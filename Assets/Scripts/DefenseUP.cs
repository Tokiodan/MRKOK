using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUP : MonoBehaviour
{
    private player player;

    // Start is called before the first frame update
    private void Start()
    {
        // Get a reference to the player component on the same or another GameObject
        player = FindObjectOfType<player>();  // Assuming there is only one player instance
        if (player == null)
        {
            Debug.LogError("Player component not found!");
        }
    }

    // Method to be called when button is clicked
    public void OnClick()
    {
        if (player != null)
        {
            player.Resistance += 1;
            player.MagResistance += 1;
            Debug.Log("Player's resistances have been increased!");
        }
    }
}
