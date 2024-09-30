using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class divinePillar : MonoBehaviour, MagicAttack
{
    public GameObject aoeIndicatorPrefab; // For the indicator
    public GameObject aoeLightPrefab; // For the light beam
    public float cooldownDuration = 5f; // Duration of the cooldown in seconds
    public int damageAmount = 20; // Amount of damage to deal

    private float cooldownTimer = 0f; // Timer to keep track of cooldown
    private GameObject currentIndicator;
    private Vector3 targetPosition;
    private bool isPlacingAoE = false;
    private bool hasDealtDamage = false; // Ensure damage is only dealt once

    // void Update()
    // {
    //     HandleAoEInput();
    //     UpdateCooldownTimer();
    // }

    public void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !isPlacingAoE && cooldownTimer <= 0f)
        {
            // Create the indicator
            currentIndicator = Instantiate(aoeIndicatorPrefab, Vector3.zero, Quaternion.identity);
            isPlacingAoE = true;
            hasDealtDamage = false; // Reset damage flag for a new cast
        }

        if (isPlacingAoE && currentIndicator != null)
        {
            // Follow the mouse position to move the indicator
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Update the indicator position but save the target position
                targetPosition = new Vector3(hit.point.x, -0.99f, hit.point.z);
                currentIndicator.transform.position = targetPosition; // Keep the indicator above the ground
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha4) && currentIndicator != null)
        {
            // sets the cooldown after casting.
            PlayerController.lastSpawnTime = Time.time;

            // Confirm the attack
            // Destroy the indicator
            Destroy(currentIndicator);
            isPlacingAoE = false;

            // Create the light beam with a collider to handle damage
            GameObject aoeLight = Instantiate(aoeLightPrefab, targetPosition, Quaternion.identity);
            Destroy(aoeLight, 3f); // Remove the light beam after 3 seconds

            // Start cooldown
            cooldownTimer = cooldownDuration;
        }
    }

    void UpdateCooldownTimer()
    {
        // Reduce the cooldown timer if it's greater than 0
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
