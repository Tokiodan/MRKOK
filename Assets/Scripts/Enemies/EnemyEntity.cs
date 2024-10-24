using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemeyEntity : Entity
{

    // worldlevel scaling.
    protected override void Awake()
    {
        Health *= ExperienceManager.currentLevel;
        Resistance *= ExperienceManager.currentLevel;
        MagResistance *= ExperienceManager.currentLevel;
        base.Awake();
    }
}