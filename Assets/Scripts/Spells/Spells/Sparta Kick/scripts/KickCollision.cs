using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCollision : MonoBehaviour
{
    public float KickForce = 10.0f;  // The force applied to the enemy
    public int damageAmount = 5;     // Amount of damage dealt to enemies

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))  // Ensure the object has the "Enemy" tag
        {
            // Apply knockback force
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                // Calculate the direction to push the enemy away
                Vector3 direction = other.transform.position - transform.position;
                direction.Normalize();  // Normalize the direction
                enemyRb.AddForce(direction * KickForce, ForceMode.Impulse);
            }

            // Apply damage to the enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);  // Apply damage to the enemy
                Debug.Log("Enemy " + other.gameObject.name + " took " + damageAmount + " damage.");
            }
        }
    }
}
