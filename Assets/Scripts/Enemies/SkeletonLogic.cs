using UnityEngine;

public class SkeletonLogic : Entity
{
    [Header("Skeleton Settings")]
    [Tooltip("Damage dealt by the player.")]
    public float Damage = 20f; // Damage dealt by the player; this can be set in the Inspector

    // Method called when the skeleton takes a hit
    public void TakeHit(float damage)
    {
        Debug.Log("Skeleton hit! Damage taken: " + damage);
        TakePhysicalDmg(damage); // Call the base method to handle damage

        // Check if health has dropped to zero or below
        if (Health <= 0)
        {
            Die(); // If health is zero or below, call Die()
        }
    }

    // Handle the death of the skeleton
    private void Die()
    {
        Debug.Log("Skeleton is dead!");
        gameObject.SetActive(false); // Deactivate the skeleton
        // Optionally, add additional death effects or cleanup here
        // Example: Play death animation, drop loot, etc.
    }
}