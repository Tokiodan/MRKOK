using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public float repelForce = 10f; // The force with which to repel the enemies/projectiles

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Get the Rigidbody component of the enemy or projectile
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate the direction away from the shield
                Vector3 directionAway = (other.transform.position - transform.position).normalized;

                // Apply a force to repel the object
                rb.AddForce(directionAway * repelForce, ForceMode.Impulse);
            }

            // Optionally play an effect or log a message here
            Debug.Log($"Repelled {other.name}!");
        }
    }
}
