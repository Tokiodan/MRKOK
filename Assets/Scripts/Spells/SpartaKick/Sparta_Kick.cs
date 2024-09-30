using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartaKick : MonoBehaviour, MagicAttack
{
    public GameObject FootPrefab;  // The foot prefab to be instantiated
    public float footOffsetDistance = 0.5f; // Distance below the camera
    public float footForwardDistance = 1.0f; // Distance in front of the camera
    public float footCooldown = 1.0f; // Cooldown time in seconds

    private bool isCooldown = false; // Track whether the cooldown is active


    public void CastSpell()
    {
        // Get the camera's position and rotation
        Camera mainCamera = Camera.main;

        // Calculate the position for the foot slightly below and in front of the camera
        Vector3 footSpawnPosition = mainCamera.transform.position
                                    - mainCamera.transform.up * footOffsetDistance
                                    + mainCamera.transform.forward * footForwardDistance;

        // Get the camera's rotation
        Quaternion cameraRotation = mainCamera.transform.rotation;

        // Instantiate foot at the calculated position with the camera's rotation
        GameObject spawnedFoot = Instantiate(FootPrefab, footSpawnPosition, cameraRotation);

        // Attach the foot to the camera so it moves with the player's view
        spawnedFoot.transform.SetParent(mainCamera.transform);

        // Set cooldown active
        isCooldown = true;

        // Start the cooldown coroutine
        StartCoroutine(FootCooldown(spawnedFoot));
    }

    private IEnumerator FootCooldown(GameObject spawnedFoot)
    {
        yield return new WaitForSeconds(footCooldown); // Wait for the cooldown duration
        isCooldown = false; // Reset the cooldown
        Destroy(spawnedFoot); // Destroy the foot after cooldown
    }
}
