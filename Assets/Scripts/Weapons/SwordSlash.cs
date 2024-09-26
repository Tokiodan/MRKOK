using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skeleton")) // Make sure skeletons are tagged appropriately
        {
            SkeletonLogic skeleton = other.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                skeleton.TakeHit(); // Call the TakeHit method on the skeleton
                // If the skeleton is killed, you can add more logic here (e.g., effects)
            }
        }
    }
}
