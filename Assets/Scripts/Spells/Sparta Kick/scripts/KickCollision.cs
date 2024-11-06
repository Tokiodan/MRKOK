using UnityEngine;

public class KickCollision : MonoBehaviour
{
    public float KickForce = 10.0f;  // The force applied to the enemy
    public int damageAmount = 5;     // Damage amount set by the SpartaKick spell

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))  // Ensure the object has the "Enemy" tag
        {
            // Apply knockback force
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                // Calculate the direction to push the enemy away
                Vector3 direction = other.transform.position - transform.position;
                direction.Normalize();  // Normalize the direction
                enemyRb.AddForce(direction * KickForce, ForceMode.Impulse);
            }

            // Apply damage to the enemy
            EnemyEntity enemyHealth = other.GetComponent<EnemyEntity>();
            if (enemyHealth != null)
            {
                enemyHealth.TakePhysicalDmg(damageAmount);  // Use ApplyDamage method for consistency
                Debug.Log("Enemy " + other.gameObject.name + " took " + damageAmount + " damage from EnemyHealth.");
            }
            else
            {
                // Fallback: Try to get the Entity component if EnemyHealth is not found
                Entity entity = other.GetComponent<Entity>();
                if (entity != null)
                {
                    entity.TakePhysicalDmg(damageAmount);  // Apply physical damage through Entity
                    Debug.Log("Enemy " + other.gameObject.name + " took " + damageAmount + " physical damage from Entity.");
                }
                else
                {
                    Debug.Log("No valid health component found on " + other.gameObject.name);
                }
            }
        }
    }
}
