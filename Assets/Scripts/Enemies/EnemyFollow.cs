using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;

 
    public float viewRadius = 10f; 
    [Range(0, 360)]
    public float viewAngle = 90f; 

    public LayerMask playerMask; 
    public LayerMask obstacleMask; 

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
            agent.destination = transform.position; 
        }
    }

 
    private bool CanSeePlayer()
    {
   
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        // FOVcheck
        float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleBetweenEnemyAndPlayer < viewAngle / 2f)
        {
            // Checking if player is in the view distance
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            if (distanceToPlayer <= viewRadius)
            {
                // Raycast 2 check obstalce
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    // If no obstacle = See player
                    return true;
                }
            }
        }

        
        return false;
    }

   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Visual debug, you can see the enemies viewradius
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
