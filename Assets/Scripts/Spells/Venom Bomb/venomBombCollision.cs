using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class venomBombCollision : MonoBehaviour
{
    public GameObject venomPuddlePrefab; // Assign the Venom Puddle prefab in the Inspector
    public float puddleDuration = 6f; // Duration before the puddle gets destroyed

    // Detect collision with any object
    void OnCollisionEnter(Collision collision)
    {
        // Use a Raycast to detect the ground position below the bomb
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Instantiate the Venom Puddle at the ground position (hit point)
            GameObject venomPuddle = Instantiate(venomPuddlePrefab, hit.point, Quaternion.identity);

            // Destroy the Venom Puddle after the specified duration
            Destroy(venomPuddle, puddleDuration);
        }

        // Destroy the Venom Bomb object after the collision
        Destroy(gameObject);
    }
}
