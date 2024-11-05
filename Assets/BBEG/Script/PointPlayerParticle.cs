using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlayerParticle : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            
            // Rotate the particle system to face the player
            transform.rotation = rotationToPlayer;
        }
    }
}
