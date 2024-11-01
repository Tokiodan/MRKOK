using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBEGroundSlam : MonoBehaviour
{
    public Transform Player; // Reference to the player
    public float slamDamage = 20.0f; // Damage dealt by the slam
    public float slamRadius = 5.0f; // Radius of the AoE
    public float slamHeight = 15.0f; // Height of the jump
    public float slamDuration = 1.0f; // Time it takes to complete the slam
    public float cooldownTime = 3.0f; // Time before the enemy can slam again
    public float slamChance = 0.1f; // 10% chance to perform the slam (adjustable)

    public GameObject dangerAreaPrefab; // Reference to the danger area prefab
    private GameObject dangerAreaInstance; // Instance of the danger area prefab

    private bool canSlam = true; // Flag to control slam cooldown

    private void Update()
    {
        if (canSlam && Vector3.Distance(transform.position, Player.position) < slamRadius)
        {
            // Check if the slam should be performed based on chance
            if (Random.value < slamChance) // Random.value returns a float between 0 and 1
            {
                StartCoroutine(PerformSlam());
            }
        }
    }

    private IEnumerator PerformSlam()
    {
        canSlam = false;

        // Instantiate the danger area prefab
        dangerAreaInstance = Instantiate(dangerAreaPrefab, transform.position, Quaternion.identity);
        dangerAreaInstance.transform.localScale = new Vector3(slamRadius * 2, 1, slamRadius * 2); // Set scale based on slam radius

        // Set the initial position of the danger area instance to slightly below ground
        dangerAreaInstance.transform.position = new Vector3(transform.position.x, -0.99f, transform.position.z);

        // Jump up
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * slamHeight;
        float elapsedTime = 0f;

        while (elapsedTime < slamDuration / 2)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / (slamDuration / 2));
            // Keep danger area on the ground while jumping
            dangerAreaInstance.transform.position = new Vector3(transform.position.x, -0.99f, transform.position.z); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fall back down
        elapsedTime = 0f;

        while (elapsedTime < slamDuration / 2)
        {
            transform.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / (slamDuration / 2));
            // Keep danger area on the ground while falling
            dangerAreaInstance.transform.position = new Vector3(transform.position.x, -0.99f, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deal damage in the AoE
        DealDamageInAoE();

        // Destroy the danger area instance after the slam
        Destroy(dangerAreaInstance);

        // Wait for cooldown before allowing another slam
        yield return new WaitForSeconds(cooldownTime);
        canSlam = true;
    }

    private void DealDamageInAoE()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, slamRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player")) // Make sure the player has the "Player" tag
            {
                collider.GetComponent<player>().TakeDamage(slamDamage);
                Debug.Log("Ground Slam dealt " + slamDamage + " damage!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the AoE radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
