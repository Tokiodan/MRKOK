using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBomb : MonoBehaviour
{
    public GameObject venomBombPrefab;   // Assign the Venom Bomb prefab in the Inspector
    public GameObject venomPuddlePrefab; // Assign the Venom Puddle prefab in the Inspector
    public float launchForce = 10f;      // The force at which the bomb is launched
    public float upwardForce = 5f;       // The upward force to create the arc
    public float spawnOffsetDistance = 2.0f; // Distance in front of the camera
    public float cooldownDuration = 5.0f; // Cooldown time in seconds

    private float lastSpawnTime;         // Time when the last bomb was spawned

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = -cooldownDuration; // Initialize so the player can spawn right away
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the '5' key and the cooldown has passed
        if (Input.GetKeyDown(KeyCode.Alpha5) && Time.time >= lastSpawnTime + cooldownDuration)
        {
            // Spawn the Venom Bomb when pressing '5'
            SpawnVenomBomb();
            
            // Update the last spawn time
            lastSpawnTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Venom Bomb is on cooldown.");
        }
    }

    // Method to spawn and launch the Venom Bomb
    void SpawnVenomBomb()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the spawn position in front of the camera
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnOffsetDistance;

        // Instantiate the Venom Bomb prefab at the calculated position and the camera's rotation
        GameObject bomb = Instantiate(venomBombPrefab, spawnPosition, mainCamera.transform.rotation);

        // Get the Rigidbody of the spawned bomb to apply force
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();

        if (bombRb != null)
        {
            // Apply an arcing force to the bomb based on the camera's forward and upward direction
            Vector3 force = mainCamera.transform.forward * launchForce + mainCamera.transform.up * upwardForce;
            bombRb.AddForce(force, ForceMode.Impulse);
        }
    }

    // Detect collision with any object
    void OnCollisionEnter(Collision collision)
    {
        // Instantiate the Venom Puddle at the collision point
        Instantiate(venomPuddlePrefab, transform.position, Quaternion.identity);

        // Destroy the Venom Bomb object after the collision
        Destroy(gameObject);
    }
}
