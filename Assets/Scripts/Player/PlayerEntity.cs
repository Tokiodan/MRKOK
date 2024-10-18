using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerEntity : Entity
{
    public healthBar healthBar;
    public float maxHealth;
    private void Start()
    {
        maxHealth = Health;
        healthBar.SetSliderMax(maxHealth);
    }
    protected void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("health is zero...");
            // SpawnLoot();
        }
    }

    // new void is added to avoid confusion with the base class.
    public new void TakePhysicalDmg(float damage)
    {
        // base is the entity class we inherit from.
        base.TakePhysicalDmg(damage);
        healthBar.SetSlider(Health);
    }

    public new void TakeMagicDmg(float damage)
    {
        base.TakeMagicDmg(damage);
        healthBar.SetSlider(Health);
    }
}


// [SerializeField] private float maxMana;

// private float currentHealth;
// private float currentMana;

// public ManaBar manaBar;
// public healthBar healthBar;

// private void Start()
// {
//     currentHealth = maxHealth;
//     healthBar.SetSliderMax(maxHealth);

//     currentMana = maxMana;
//     manaBar.SetSliderMax(maxMana);
// }

// public void TakeDamage(float amount)
// {
//     currentHealth -= amount;
//     if (currentHealth < 0) currentHealth = 0;
//     Debug.Log("Current Health after damage: " + currentHealth);
//     healthBar.SetSlider(currentHealth);
// }

// public void UseMana(float amount)
// {
//     currentMana -= amount;
//     if (currentMana < 0) currentMana = 0;
//     Debug.Log("Mana after move:" + currentMana);
//     manaBar.SetSlider(currentMana);
// }

// private void Update()
// {
//     if (currentMana < maxMana)
//     {
//         RegenerateMana(Time.deltaTime * 2f);
//     }

//     if (Input.GetKeyDown(KeyCode.K))
//     {
//         UseMana(10f);
//     }
// }


// public void RegenerateMana(float amount)
// {
//     currentMana += amount;
//     if (currentMana > maxMana) currentMana = maxMana;
//     manaBar.SetSlider(currentMana);
// }