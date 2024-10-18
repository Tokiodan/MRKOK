using UnityEngine;

public class DMGController : MonoBehaviour
{
    [Header("Damage Settings")]
    [Tooltip("Initial damage dealt by the player.")]
    public float initialDamage = 20f;
    public float damageIncrement = 5f;

    private WeaponController weaponController; // Reference to the WeaponController

    private void Start()
    {
        // Find the WeaponController component on the same GameObject
        weaponController = GetComponent<WeaponController>();

        // Initialize PlayerDmg with initialDamage
        if (weaponController != null)
        {
            weaponController.PlayerDmg = initialDamage; // Set the initial damage
            Debug.Log("Initial player damage set to: " + weaponController.PlayerDmg);
        }
        else
        {
            Debug.LogError("WeaponController not found on this GameObject!");
        }
    }

    // Public method to increase damage, to be called from UI Button
    public void IncreaseDamage()
    {
        if (weaponController != null)
        {
            weaponController.PlayerDmg += damageIncrement; // Directly increase PlayerDmg
            Debug.Log("Player damage increased! New damage: " + weaponController.PlayerDmg);
        }
        else
        {
            Debug.LogError("WeaponController not found when trying to increase damage!");
        }
    }
}
