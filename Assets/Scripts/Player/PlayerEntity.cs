using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{

    public healthBar healthBar;
    public float maxHealth = 100f;


    private void Start()
    {
        Health = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0; // Ensure it doesn't go negative.
        Debug.Log("Current Health after damage: " + Health); // Debug log to track health
        healthBar.SetSlider(Health);
    }
}
