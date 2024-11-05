using UnityEngine;
using System.Collections;

public class AegisShield : Spell
{
    public GameObject shieldPrefab; // The prefab for the shield
    public float shieldDuration = 5f; // Duration of the shield
    public float shieldRadius = 0.5f; // Radius of the shield
    private GameObject currentShield; // Reference to the instantiated shield

    private void Awake()
    {
        spellID = "AegisShield"; // Set a unique ID for this spell
        baseDamage = 0; // No damage as it's a defensive spell
        damageIncrement = 0; // No damage increment
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Find the player GameObject by tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found. Ensure the player GameObject has the 'Player' tag.");
            return; // Exit if player not found
        }

        // Create the shield around the player
        if (currentShield != null) Destroy(currentShield); // Destroy existing shield if it exists

        // Create the shield
        currentShield = Instantiate(shieldPrefab, spawnPosition, spawnRotation);
        
        // Position the shield at the player's location
        currentShield.transform.position = player.transform.position; // Set shield position to player's position

        // Set the shield's radius
        SphereCollider collider = currentShield.GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.radius = shieldRadius;
        }

        // Start a coroutine to handle the shield duration
        StartCoroutine(HandleShieldDuration());
    }

    private IEnumerator HandleShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);
        Destroy(currentShield); // Destroy the shield after the duration
    }

    private void Update()
    {
        // Keep the shield centered around the player
        if (currentShield != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                currentShield.transform.position = player.transform.position; // Update the shield position to follow the player
            }
        }
    }
}
