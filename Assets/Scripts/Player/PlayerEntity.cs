using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerEntity : Entity
{
    public healthBar healthBar;
    public ManaBar manaBar;
    public float currentMana;
    public float maxMana;

    private void Start()
    {
        healthBar.SetSliderMax(maxHealth);
    }

    // this update had to be protected in order to function properly?
    // Idk I had issues with inheritance for these methods.
    protected override void Update()
    {
        ManaHandling();
        base.Update();
    }

    // The reason i've used the keyword new here is because it will overwrite the entity method with the same name.
    // I do recall it with base.TakePhysicalDmg in order to still execute that script. 
    // But added the health slider for the player.
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

    public void ManaHandling()
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
    public void UseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana < 0) currentMana = 0;
        Debug.Log("Mana after move:" + currentMana);
        manaBar.SetSlider(currentMana);
    }
}


