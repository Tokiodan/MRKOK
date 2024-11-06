using System.Collections;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public int damage = 10;  // Default damage, but will be set by Fireball script

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
            ApplyDamage(other);
            StartCoroutine(HandleCollisionWithEnemy(other)); // Delay destruction
        }
    }

    private void ApplyDamage(Collider enemy)
    {
        EnemyEntity enemyHealth = enemy.GetComponent<EnemyEntity>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeMagicDmg(damage); // Use ApplyDamage method
            Debug.Log("Damaged enemy: " + enemy.gameObject.name + " for " + damage + " damage.");
        }
        else
        {
            Debug.Log("No valid health component found on " + enemy.gameObject.name);
        }
    }

    private IEnumerator HandleCollisionWithEnemy(Collider enemy)
    {
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            Vector3 pushbackDirection = (enemy.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(pushbackDirection * 10f, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
