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

    public float accelSpeed;
    public float speed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // there will be one player anyway, why not just scan for the player tag? -Z
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            // an extra stop check so skelly doesn't even wanna run through you. -Z
            float playerDistance = Vector3.Distance(transform.position, Player.position);
            if (playerDistance > agent.stoppingDistance)
            {
                agent.destination = Player.position;

                // This is to make sure skelly doens't run through your ass -Z
                if (playerDistance < 2f)
                {
                    agent.speed = Mathf.Lerp(agent.speed, 1.0f, Time.deltaTime * 2f);
                    agent.acceleration = Mathf.Lerp(agent.acceleration, 4.0f, Time.deltaTime * 2f);
                }
                else
                {
                    agent.speed = speed;
                    agent.acceleration = accelSpeed;
                }
            }
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
