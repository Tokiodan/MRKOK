using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCollision : MonoBehaviour
{
    public int damageAmount = 20; // Damage dealt by the pillar

    // A set to keep track of enemies that have already taken damage
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    // This method triggers when another collider enters the AoE collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy (tagged as "Enemy")
        if (other.CompareTag("Enemy"))
        {
            // Ensure this enemy has not been damaged by this AoE yet
            if (!damagedEnemies.Contains(other.gameObject))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Apply damage to the enemy
                    enemyHealth.TakeDamage(damageAmount);
                    Debug.Log("Damaged enemy: " + other.gameObject.name + " for " + damageAmount + " damage.");

                    // Add the enemy to the HashSet to prevent further damage from this AoE
                    damagedEnemies.Add(other.gameObject);
                }
            }
        }
    }
    
}
