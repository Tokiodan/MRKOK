using UnityEngine;

public class SkeletonLogic : EnemeyEntity
{
    

    public void TakeHit(float damage)
    {
        Debug.Log("Skeleton hit! Damage taken: " + damage);
        TakePhysicalDmg(damage); // Call the base class method to apply damage

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Skeleton is dead!");
        gameObject.SetActive(false); // Deactivate the skeleton on death
        // Optional: You can use Destroy(gameObject) if permanent removal is needed
    }
}
