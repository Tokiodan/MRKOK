using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    // Reference to the amount of damage dealt by the sword
    public float damage = 20f; // This can be set in the Inspector or updated to pull from the WeaponController if needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skeleton")) // Make sure skeletons are tagged appropriately
        {
            SkeletonLogic skeleton = other.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                skeleton.TakeHit(damage); // Call the TakeHit method on the skeleton with the damage value
                Debug.Log("Skeleton hit! Damage dealt: " + damage);

                // Optional: Add visual effects or sound effects here when the skeleton is hit
                // Example: Play a hit animation or particle effect
            }
        }
    }
}
