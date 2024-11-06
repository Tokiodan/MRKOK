using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgWhenCollidingEnemy : MonoBehaviour
{
    public float damage = 10.0f; // Damage dealt by the enemy
    private PlayerEntity playerEntity; // Reference to the player's PlayerEntity component

    void Start()
    {
        // Find the player by tag and get the PlayerEntity component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerEntity = player.GetComponent<PlayerEntity>();
        }
        else
        {
            Debug.LogWarning("Player object with PlayerEntity component not found.");
        }
    }

    // Called when the enemy collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy hits the player
        if (collision.gameObject.CompareTag("Player") && playerEntity != null)
        {
            // Reduce the player's health by the damage amount
            playerEntity.TakePhysicalDmg(damage);
            Debug.Log("Player took damage: " + damage);
        }
    }
}
