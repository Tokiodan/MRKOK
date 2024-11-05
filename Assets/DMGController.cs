using UnityEngine;

public class DMGController : MonoBehaviour
{
    public float damageIncrement = 5f; // How much to increase the damage each time
    private SwordSlash swordSlash; // Reference to the SwordSlash script

    private void Start()
    {
        // Find the SwordSlash component (adjust this as needed)
        swordSlash = FindObjectOfType<SwordSlash>();

        if (swordSlash == null)
        {
            Debug.LogError("SwordSlash script not found in the scene!");
        }
    }

    // Method called when UI button is clicked
    public void IncreaseDamage()
    {
        if (swordSlash != null)
        {
            swordSlash.Initialdamage += damageIncrement; // Increase sword's initial damage
            Debug.Log("Sword damage increased! New damage: " + swordSlash.Initialdamage);
        }
        else
        {
            Debug.LogError("SwordSlash script not found when trying to increase damage!");
        }
    }
}
