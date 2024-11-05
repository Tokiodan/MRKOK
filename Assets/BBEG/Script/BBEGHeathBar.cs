using UnityEngine;
using UnityEngine.UI;

public class BBEGHealthBar : MonoBehaviour
{
    public Image healthBar;   // Reference to the UI Image for the health bar fill
    public BBEG bBEG;         // Reference to the BBEG health script
    
    void Start()
    {
        if (bBEG != null && healthBar != null)
        {
            // Initialize the health bar based on BBEG's current health
            UpdateHealthBar();
        }
    }

    void Update()
    {
        if (bBEG != null && healthBar != null)
        {
            // Update the health bar fill amount based on BBEG's current health
            UpdateHealthBar();
        }
    }

    // Updates the health bar fill based on current health percentage
    void UpdateHealthBar()
    {
        healthBar.fillAmount = bBEG.CurrentHealth / bBEG.MaxHealth;
    }
}
