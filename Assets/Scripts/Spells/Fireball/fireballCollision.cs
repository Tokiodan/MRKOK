using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public int damage = 10; // The amount of damage the fireball does
    public int LifeTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name); // Log object name

        // Check if the object is not the player
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy"))
        {
            Debug.Log("Collided with: " + other.gameObject.name + " (Not a Player or Enemy)");
            Destroy(gameObject); // Destroy the fireball on impact with non-enemy objects
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collided with: " + other.gameObject.name + " (Enemy)");
            // Deal damage to the enemy
            ApplyDamage(other);

            // Start coroutine to handle the delayed destruction
            StartCoroutine(HandleCollisionWithEnemy(other));
        }
        else
        {
            Debug.Log("Collided with Player, no destruction.");
        }
    }

    private void ApplyDamage(Collider enemy)
    {
        // Get the EnemyHealth component and apply damage
        Entity enemyHealth = enemy.GetComponent<Entity>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeMagicDmg(damage); // Apply damage to the enemy
            Debug.Log("Damaged enemy: " + enemy.gameObject.name + " for " + damage + " damage.");
        }
        else
        {
            Debug.Log("Enemy does not have an EnemyHealth component.");
        }
    }

    private IEnumerator HandleCollisionWithEnemy(Collider enemy)
    {
        // Optionally, apply a pushback force to the enemy
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            Vector3 pushbackDirection = (enemy.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(pushbackDirection * 100f, ForceMode.Impulse); // Adjust the force as necessary
        }

        // Wait for a short duration before destroying the fireball
        yield return new WaitForSeconds(0.5f); // Adjust the time as necessary

        // Destroy the fireball after the delay
        Destroy(gameObject);
    }
}
