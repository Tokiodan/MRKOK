using UnityEngine;

public class BBEGFireballTracking : MonoBehaviour
{
    [HideInInspector] public Transform player;  // Public reference to the player
    public float speed = 10f;                   // Speed of the fireball
    public float maxTrackingDistance = 10f;     // Maximum distance for tracking

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isTracking = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is present
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the fireball prefab.");
            return;
        }

        // Ensure player is set
        if (player == null)
        {
            Debug.LogError("Player not assigned in FireballTracking!");
            return;
        }

        targetPosition = player.position; // Start tracking to player's initial position

        // Apply initial force toward player
        Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
        rb.velocity = directionToPlayer * speed;
    }

    private void Update()
    {
        if (isTracking && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Stop tracking if out of range
            if (distanceToPlayer > maxTrackingDistance)
            {
                isTracking = false;
            }
            else
            {
                targetPosition = player.position;
                Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
                rb.velocity = directionToPlayer * speed; // Adjust velocity toward player
            }
        }

        // Rotate to face the target position
        if (rb.velocity != Vector3.zero) transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Fireball collided with: " + other.name); // Log the collision

    if (other.CompareTag("Player"))
    {
        Debug.Log("Collided with player. Applying damage and destroying fireball.");
        ApplyDamage(other);
        Destroy(gameObject);
    }
    else if (!other.CompareTag("Enemy")) // Destroy on impact with non-enemy
    {
        Debug.Log("Collided with non-enemy object. Destroying fireball.");
        Destroy(gameObject);
    }
}

    private void ApplyDamage(Collider target)
    {
        Entity entity = target.GetComponent<Entity>();
        if (entity != null) entity.TakePhysicalDmg(20); // Example damage value
    }
}
