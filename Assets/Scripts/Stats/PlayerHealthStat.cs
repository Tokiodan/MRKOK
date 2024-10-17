using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthStat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;

    private float currentHealth;
    private float currentMana;

    public ManaBar manaBar;
    public healthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);

        currentMana = maxMana;
        manaBar.SetSliderMax(maxMana);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0; 
        Debug.Log("Current Health after damage: " + currentHealth); 
        healthBar.SetSlider(currentHealth);
    }

    public void UseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana < 0) currentMana = 0;
        Debug.Log("Mana after move:" + currentMana);
        manaBar.SetSlider(currentMana);
    }

    private void Update()
    {
        if (currentMana < maxMana)
        {
            RegenerateMana(Time.deltaTime * 2f);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            UseMana(10f);
        }
    }


    public void RegenerateMana(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana) currentMana = maxMana;
        manaBar.SetSlider(currentMana);
    }
}