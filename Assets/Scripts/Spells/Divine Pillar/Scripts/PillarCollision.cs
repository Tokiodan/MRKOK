using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCollision : MonoBehaviour
{
    private int damageAmount; // Damage dealt by the pillar
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>(); // Track damaged enemies

    // Set the damage amount based on the spell's level
    public void SetDamageAmount(int damage)
    {
        damageAmount = damage;
    }

    // Trigger when another collider enters the AoE collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy (tagged as "Enemy")
        if (other.CompareTag("Enemy") && !damagedEnemies.Contains(other.gameObject))
        {
            // Try to get the EnemyHealth component
            EnemyEntity enemyHealth = other.GetComponent<EnemyEntity>();
            if (enemyHealth != null)
            {
                // Call the ApplyDamage method on EnemyHealth
                enemyHealth.TakeMagicDmg(damageAmount);
                Debug.Log("Damaged enemy: " + other.gameObject.name + " for " + damageAmount + " damage.");
            }
            else
            {
                // Fallback: Try to get the Entity component directly
                Entity entity = other.GetComponent<Entity>();
                if (entity != null)
                {
                    entity.TakeMagicDmg(damageAmount); // Apply magic damage
                    Debug.Log("Damaged entity: " + other.gameObject.name + " for " + damageAmount + " magic damage.");
                }
                else
                {
                    Debug.Log("No valid health component found on " + other.gameObject.name);
                }
            }

            // Add the enemy to the HashSet to prevent further damage from this AoE
            damagedEnemies.Add(other.gameObject);
        }
    }
}
