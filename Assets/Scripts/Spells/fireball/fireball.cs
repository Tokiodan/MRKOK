using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, MagicAttack
{
    public float speed;                    // Speed of the fireball
    public GameObject fireballPrefab;      // Fireball prefab to spawn
    public float spawnOffsetDistance = 2.0f; // Distance in front of the camera
    public float cooldownDuration = 5.0f;  // Cooldown duration in seconds

    private bool isCooldown = false;       // Track whether the cooldown is active

    // // Update is called once per frame
    // void Update()
    // {
    //     // Check if the fireball input is pressed and if it's ready to fire
    //     if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && !isCooldown)
    //     {
    //         // Spawn the fireball
    //         SpawnFireball();
    //         // Start the cooldown
    //         StartCoroutine(FireballCooldown());
    //     }
    // }

    public void CastSpell()
    {
        // sets casting cooldown.
        PlayerController.lastSpawnTime = Time.time;

        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the spawn position for the fireball
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnOffsetDistance;

        // Instantiate the fireball at the calculated position with the camera's rotation
        GameObject spawnedFireball = Instantiate(fireballPrefab, spawnPosition, mainCamera.transform.rotation);
        // _ = StartCoroutine(FireballCooldown(spawnedFireball));

        // Add velocity to the fireball to make it move forward
        Rigidbody rb = spawnedFireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = mainCamera.transform.forward * speed; // Set velocity in the direction the camera is facing
        }

    }

    private IEnumerator FireballCooldown(GameObject obj)
    {
        Debug.Log("Enum started");
        yield return new WaitForSeconds(cooldownDuration); // Wait for the cooldown duration
        Debug.Log("object Destroyed");
        Destroy(obj);
    }
}
