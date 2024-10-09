using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCollision : MonoBehaviour
{
    public int damageAmount = 20; // Damage dealt by the pillar

    // A set to keep track of enemies that have already taken damage
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    // This method triggers when another collider enters the AoE collider
    // small issue where I made everything other.gameObject. Idk if it's needed. -Z
    // Issue was on dani's side where his enemyskeleton didn't have a rigidbody for the onTriggerEnter -Z
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy (tagged as "Enemy")
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Ensure this enemy has not been damaged by this AoE yet
            if (!damagedEnemies.Contains(other.gameObject))
            {
                Entity enemyHealth = other.gameObject.GetComponent<Entity>();
                if (enemyHealth != null)
                {
                    // Apply damage to the enemy
                    enemyHealth.TakeMagicDmg(damageAmount);
                    Debug.Log("Damaged enemy: " + other.gameObject.name + " for " + damageAmount + " damage.");

                    // Add the enemy to the HashSet to prevent further damage from this AoE
                    damagedEnemies.Add(other.gameObject);
                }
            }
        }
    }

}
