using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public float Initialdamage = 20f; // This can be set in the Inspector or updated to pull from the WeaponController if needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Ensure the skeletons are tagged correctly as "Skeleton"
        {
            SkeletonLogic skeleton = other.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                // Apply the damage to the skeleton using its own TakeHit method
                skeleton.TakeHit(Initialdamage);
                Debug.Log("Skeleton hit! Damage dealt: " + Initialdamage);
            }
            else
            {
                Debug.LogWarning("No SkeletonLogic found on the target!");
            }
        }
        else
        {
            Debug.LogWarning("OnTriggerEnter hit something else: " + other.name);
        }
    }
}
