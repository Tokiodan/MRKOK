using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController

    [Header("Speed Upgrade Settings")]
    [SerializeField] private float speedIncreaseAmount = 0.1f; // Amount to increase speed per upgrade
    [SerializeField] private float maxSpeedMultiplier = 3f; // Maximum speed multiplier
    [SerializeField] private float minSpeedMultiplier = 0.5f; // Minimum speed multiplier

    // Call this method to upgrade the speed
    public void UpgradeSpeed()
    {
        if (playerController != null)
        {
            // Check current speed multiplier
            float currentMultiplier = playerController.sprintSpeedMultiplier;

            // Only upgrade if it's less than max speed multiplier
            if (currentMultiplier < maxSpeedMultiplier)
            {
                // Increase sprint speed multiplier
                playerController.sprintSpeedMultiplier += speedIncreaseAmount;

                // Clamp to maximum value
                playerController.sprintSpeedMultiplier = Mathf.Clamp(playerController.sprintSpeedMultiplier, minSpeedMultiplier, maxSpeedMultiplier);

                // Log the current speed multiplier
                Debug.Log($"Sprint Speed Multiplier changed to: {playerController.sprintSpeedMultiplier}");
            }
            else
            {
                Debug.Log("Maximum sprint speed multiplier reached.");
            }
        }
    }
}
