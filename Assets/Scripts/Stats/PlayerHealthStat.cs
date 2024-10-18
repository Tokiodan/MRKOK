using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthStat : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;
    public healthBar healthBar;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0; // Ensure it doesn't go negative.
        Debug.Log("Current Health after damage: " + currentHealth); // Debug log to track health
        healthBar.SetSlider(currentHealth);
    }

    //  private void Update()
    //  {
    //  if (Input.GetKeyDown(KeyCode.K))
    //  {
    //      TakeDamage(10f);
    // Debug.Log("Damage has been taken");
    //  }
    // }
}
