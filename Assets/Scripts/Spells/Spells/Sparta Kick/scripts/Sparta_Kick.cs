using System.Collections;
using UnityEngine;

public class SpartaKick : Spell
{
    private float footOffsetDistance = 1.0f; // Set to a lower value to appear lower
    public float footForwardDistance = 1.0f; // Distance in front of the camera

    private void Awake()
    {
        spellID = "SpartaKick"; // Set a unique ID for this spell
        cooldownDuration = 1.0f; // Set the cooldown duration
        spawnOffsetDistance = footOffsetDistance; // Override the spawn offset for this spell
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Calculate the position for the foot lower and in front of the camera
        Vector3 footSpawnPosition = spawnPosition 
                                    - spawnRotation * Vector3.up * footOffsetDistance 
                                    + spawnRotation * Vector3.forward * footForwardDistance;

        // Instantiate foot at the calculated position with the camera's rotation
        GameObject spawnedFoot = Instantiate(spellPrefab, footSpawnPosition, spawnRotation);

        // Attach the foot to the camera so it moves with the player's view
        spawnedFoot.transform.SetParent(Camera.main.transform);

        // Start a coroutine to handle the cooldown and destruction of the foot
        StartCoroutine(FootCooldown(spawnedFoot));
    }

    private IEnumerator FootCooldown(GameObject spawnedFoot)
    {
        yield return new WaitForSeconds(cooldownDuration); // Wait for the cooldown duration
        Destroy(spawnedFoot); // Destroy the foot after cooldown
    }
}
