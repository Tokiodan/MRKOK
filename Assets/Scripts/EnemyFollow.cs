using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;

    // Parameters for enemy vision
    public float viewRadius = 10f; // How far the enemy can see
    [Range(0, 360)]
    public float viewAngle = 90f; // The field of view (FOV) of the enemy in degrees

    public LayerMask playerMask; // Layer for the player
    public LayerMask obstacleMask; // Layer for obstacles (like walls)

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            agent.destination = Player.position;
        }
        else
        {
            agent.destination = transform.position; // Optional: You can set a patrol or idle behavior here.
        }
    }

    // Method to check if the enemy can see the player
    private bool CanSeePlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        // Check if the player is within the enemy's FOV
        float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleBetweenEnemyAndPlayer < viewAngle / 2f)
        {
            // Check if the player is within the view radius
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            if (distanceToPlayer <= viewRadius)
            {
                // Raycast to check if there's an obstacle between the enemy and the player
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    // If no obstacle, the enemy can see the player
                    return true;
                }
            }
        }

        // If any condition is not satisfied, the enemy cannot see the player
        return false;
    }

    // Optional: Visualize the enemy's FOV in the editor (can be useful for debugging)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 fovLine1 = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewRadius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        Gizmos.color = Color.green;
        if (Player != null)
        {
            Gizmos.DrawLine(transform.position, Player.position);
        }
    }
}
