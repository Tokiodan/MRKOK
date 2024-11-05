using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBarrier : MonoBehaviour
{
    public float repelForce = 10f; // The force with which to repel the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the Rigidbody component of the player
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate the direction away from the barrier
                Vector3 directionAway = (other.transform.position - transform.position).normalized;

                // Apply a force to repel the player
                rb.AddForce(directionAway * repelForce, ForceMode.Impulse);
            }

            // Optionally play an effect or log a message here
            Debug.Log($"Repelled {other.name}!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Continually repel the player while inside the barrier
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 directionAway = (other.transform.position - transform.position).normalized;
                rb.AddForce(directionAway * repelForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }
}
