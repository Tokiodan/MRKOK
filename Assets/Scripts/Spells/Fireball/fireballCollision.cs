using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name); // Log object name

        // Check if the object does not have the tag "Player"
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) // Closing parenthesis added here
        {
            Debug.Log("Collided with: " + other.gameObject.name + " (Not a Player)");
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player") && other.CompareTag("Enemy")) // Closing parenthesis added here
        {
            Debug.Log("Collided with: " + other.gameObject.name + " (Enemy)");
            // Start coroutine to handle the delayed destruction
            StartCoroutine(HandleCollisionWithEnemy(other));
        }
        else
        {
            Debug.Log("Collided with Player, no destruction.");
        }
    }

    private IEnumerator HandleCollisionWithEnemy(Collider enemy)
    {
        // Optionally, apply a pushback force to the enemy
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            Vector3 pushbackDirection = (enemy.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(pushbackDirection * 10f, ForceMode.Impulse); // Adjust the force as necessary
        }

        // Wait for a short duration before destroying the fireball
        yield return new WaitForSeconds(0.5f); // Adjust the time as necessary

        // Destroy the fireball after the delay
        Destroy(gameObject);
    }
}
