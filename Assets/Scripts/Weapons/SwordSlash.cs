using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public float damage = 20f; // Set this to whatever the player damage is

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skeleton")) // Make sure skeletons are tagged appropriately
        {
            SkeletonLogic skeleton = other.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                skeleton.TakeHit(damage); // Pass the damage value to the skeleton
                // If the skeleton is killed, you can add more logic here (e.g., effects)
            }
        }
    }
}
