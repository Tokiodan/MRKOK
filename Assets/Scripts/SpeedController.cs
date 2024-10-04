using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public PlayerController playerController;
    public float speedIncrease; // This should be a multiplier (e.g., 1.5 for 50% faster)

    private void Start()
    {
        // Find the PlayerController if not assigned
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    public void ChangeSpeed()
    {
        // Calculate the new base move speed
        float newBaseSpeed = playerController.defaultMoveSpeed * speedIncrease;

        // Call the method in PlayerController to set the new base speed
        playerController.SetBaseMoveSpeed(newBaseSpeed);
    }
}
