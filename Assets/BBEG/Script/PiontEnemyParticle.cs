using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiontEnemyParticle : MonoBehaviour
{
    public Transform enemy;

    void Update()
    {
        if (enemy != null)
        {
            // Calculate the direction to the enemy
            Vector3 directionToEnemy = (enemy.position - transform.position).normalized;
            Quaternion rotationToEnemy = Quaternion.LookRotation(directionToEnemy);
            
            // Rotate the particle system to face the enemy
            transform.rotation = rotationToEnemy;
        }
    }
}
