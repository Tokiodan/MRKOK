using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class Enemy_pounce_attack : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public float pounceDamage = 15.0f; // Pounce does more damage
    public float pounceRange = 10.0f; // Range within which the enemy can pounce
    public float pounceForceHorizontal = 5.0f; // Horizontal force of the pounce
    public float pounceForceVertical = 5.0f; // Vertical force of the pounce for arching trajectory
    public int pounceChance = 10; // 1 in 10 chance of pouncing, adjustable
    public float pounceDelay = 1.0f; // Delay before pounce to give time for dodging
    public float pounceDuration = 2.0f; // Time it takes to complete the pounce

    private NavMeshAgent navMeshAgent; // NavMeshAgent for AI navigation
    private Rigidbody enemyRb; // Rigidbody for applying force
    private player playerScript; // Reference to the player's health script
    private bool isPouncing = false; // To prevent multiple pounces at once

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Getting the NavMeshAgent component
        enemyRb = GetComponent<Rigidbody>(); // Getting Rigidbody component for pounce
        playerScript = player.GetComponent<player>(); // Getting player's script
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is within pounce range
        if (!isPouncing && Vector3.Distance(transform.position, player.transform.position) <= pounceRange)
        {
            TryPounce();
        }
    }

    void TryPounce()
    {
        // Random chance for pounce
        if (Random.Range(1, pounceChance + 1) == 1)
        {
            StartCoroutine(PounceWithDelay());
        }
    }

    IEnumerator PounceWithDelay()
    {
        isPouncing = true;

        // Wait before pouncing to give the player a chance to dodge
        yield return new WaitForSeconds(pounceDelay);

        // Temporarily disable NavMeshAgent for the jump
        navMeshAgent.enabled = false;

        // Execute the pounce
        Pounce();

        // Wait for the pounce duration to end
        yield return new WaitForSeconds(pounceDuration);

        // Re-enable NavMeshAgent after the pounce is complete
        navMeshAgent.enabled = true;

        isPouncing = false;
    }

    void Pounce()
    {
        // Calculate direction towards player on the XZ plane (ignoring vertical difference)
        Vector3 horizontalDirection = (player.transform.position - transform.position).normalized;
        horizontalDirection.y = 0; // Ensure only horizontal direction

        // Apply force in an arching motion
        Vector3 pounceVelocity = horizontalDirection * pounceForceHorizontal;
        pounceVelocity.y = pounceForceVertical; // Add vertical force for arch

        enemyRb.velocity = pounceVelocity; // Set velocity directly to control the speed and trajectory

        // Inflict damage to the player
        StartCoroutine(InflictDamageAfterPounce());
    }

    IEnumerator InflictDamageAfterPounce()
    {
        // Wait for the pounce duration to finish before applying damage
        yield return new WaitForSeconds(pounceDuration);

        // Check if the enemy is still near the player to apply damage
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f) // Adjust range for contact
        {
            playerScript.TakeDamage(pounceDamage);
            Debug.Log("Enemy pounced and dealt " + pounceDamage + " damage!");
        }
    }
}
