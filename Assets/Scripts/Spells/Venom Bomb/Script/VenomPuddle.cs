using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomPuddle : MonoBehaviour
{
    private int damage = 5;  // Default damage value (will be overridden)
    public float damageInterval = 1f;  // Interval at which damage is applied (in seconds)
    private List<GameObject> enemiesInPuddle = new List<GameObject>();  // List to track enemies inside the puddle
    private Dictionary<GameObject, Coroutine> damageCoroutines = new Dictionary<GameObject, Coroutine>();  // Track damage coroutines per enemy

    // Set the damage of the puddle
    public void SetDamage(int newDamage)
    {
        damage = newDamage;  // Override default damage with the value passed from VenomBomb
        Debug.Log($"Venom Puddle damage set to {damage}");
    }

    // Called when another collider enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInPuddle.Contains(other.gameObject))
        {
            // Add the enemy to the list if it has the "Enemy" tag and start damaging them
            enemiesInPuddle.Add(other.gameObject);
            StartDamageCoroutine(other.gameObject);
            Debug.Log("Enemy entered the puddle: " + other.name);
        }
    }

    // Called when another collider exits the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Remove the enemy from the list if it leaves the puddle and stop damaging them
            enemiesInPuddle.Remove(other.gameObject);
            StopDamageCoroutine(other.gameObject);
            Debug.Log("Enemy exited the puddle: " + other.name);
        }
    }

    // Start the coroutine to apply damage to the enemy over time, if not already started
    private void StartDamageCoroutine(GameObject enemy)
    {
        if (!damageCoroutines.ContainsKey(enemy))
        {
            Coroutine damageCoroutine = StartCoroutine(DamageEnemyOverTime(enemy));
            damageCoroutines[enemy] = damageCoroutine;  // Track the coroutine for this enemy
        }
    }

    // Stop the damage coroutine for a specific enemy
    private void StopDamageCoroutine(GameObject enemy)
    {
        if (damageCoroutines.ContainsKey(enemy))
        {
            StopCoroutine(damageCoroutines[enemy]);
            damageCoroutines.Remove(enemy);  // Remove from the dictionary
        }
    }

    // Coroutine to apply damage to the enemy over time
    IEnumerator DamageEnemyOverTime(GameObject enemy)
    {
        while (enemiesInPuddle.Contains(enemy))
        {
            // Apply damage to the enemy
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage);  // Use ApplyDamage method
                Debug.Log($"Damaged enemy: {enemy.name}, Damage: {damage}");
            }

            // Wait for the next damage interval
            yield return new WaitForSeconds(damageInterval);
        }

        // Remove the coroutine from the dictionary when the coroutine ends
        damageCoroutines.Remove(enemy);
    }

    // Handle puddle destruction
    private void OnDestroy()
    {
        // Clear the list of enemies and stop all damage coroutines
        foreach (var enemy in enemiesInPuddle)
        {
            StopDamageCoroutine(enemy);
        }

        enemiesInPuddle.Clear();
        damageCoroutines.Clear();
        Debug.Log("Puddle destroyed, all enemies list cleared and coroutines stopped.");
    }
}
