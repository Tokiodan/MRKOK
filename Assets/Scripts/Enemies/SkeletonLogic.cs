using UnityEngine;

public class SkeletonLogic : MonoBehaviour
{
    [Header("Skeleton Settings")]
    public int hitsToKill = 1; // Number of hits to kill the skeleton
    private int currentHits = 0; // Tracks current hits taken

    public void TakeHit()
    {
        currentHits++;
        if (currentHits >= hitsToKill)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Skeleton is dead!");
        gameObject.SetActive(false); // Deactivates the skeleton
        // Optionally, add additional death effects or cleanup here
    }
}
