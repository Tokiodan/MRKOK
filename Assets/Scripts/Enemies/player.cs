using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float player_HP = 100.0f; // Start HP of the player

    // Function to apply damage to the player
    public void TakeDamage(float damage)
    {
        player_HP -= damage;
        Debug.Log("Hit! Current HP: " + player_HP);

        // Check if the player has no HP left
        if (player_HP <= 0)
        {
            Die();
        }
    }

    // Function that is called when the player dies
    void Die()
    {
        // Code to execute when the player dies
        Debug.Log("Player is dead!");
        // For example, deactivate the player or show a 'Game Over' screen
        gameObject.SetActive(false);
    }
}
