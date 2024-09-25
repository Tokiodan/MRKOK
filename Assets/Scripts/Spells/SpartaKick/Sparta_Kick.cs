using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartaKick : MonoBehaviour
{
    public float KickForce = 10.0f; // Initialize with a default value
    public float KickRange = 2.0f; // Initialize with a default value
    public GameObject Foot;
    public float footOffsetDistance = 0.5f; // Distance below the camera
    public float footForwardDistance = 1.0f; // Distance in front of the camera
    public float footCooldown = 1.0f; // Cooldown time in seconds

    private bool isCooldown = false; // Track whether the cooldown is active

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the kick input is correctly defined and not on cooldown
        if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && !isCooldown) 
        {
            Kick();
        }
    }

    void Kick()
{
    // Get the camera's position and rotation
    Camera mainCamera = Camera.main;

    // Calculate the position for the foot slightly below and in front of the camera
    Vector3 footSpawnPosition = mainCamera.transform.position 
                                - mainCamera.transform.up * footOffsetDistance 
                                + mainCamera.transform.forward * footForwardDistance;

    // Get the camera's rotation
    Quaternion cameraRotation = mainCamera.transform.rotation;

    // Instantiate foot at the calculated position with camera's rotation
    GameObject spawnedFoot = Instantiate(Foot, footSpawnPosition, cameraRotation);

    // Attach the foot to the camera so it moves with the player's view
    spawnedFoot.transform.SetParent(mainCamera.transform);

    // Set cooldown active
    isCooldown = true;

    // Start the cooldown coroutine
    StartCoroutine(FootCooldown(spawnedFoot));

    // Use class variables instead of redefining them
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, KickRange);
    foreach (var hitCollider in hitColliders)
    {
        if (hitCollider.CompareTag("Enemy")) // Ensure your enemy has the "Enemy" tag
        {
            Rigidbody enemyRb = hitCollider.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                // Calculate the direction to push the enemy away
                Vector3 direction = hitCollider.transform.position - transform.position;
                direction.Normalize(); // Normalize the direction
                enemyRb.AddForce(direction * KickForce, ForceMode.Impulse);
            }
        }
    }

    // Optionally destroy the spawned foot object after some time
    Destroy(spawnedFoot, 1.0f); // Adjust the duration as needed
}

    private IEnumerator FootCooldown(GameObject spawnedFoot)
    {
        yield return new WaitForSeconds(footCooldown); // Wait for the cooldown duration
        isCooldown = false; // Reset the cooldown
        Destroy(spawnedFoot); // Destroy the foot after cooldown
    }
}
