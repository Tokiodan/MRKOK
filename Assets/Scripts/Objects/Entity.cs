using System.Collections.Generic;
using UnityEngine;
public class Entity : MonoBehaviour
{
    private void OnEnable()
    {
        // it will automatically set the database as the database object
        database = Resources.Load<ItemDatabaseObject>("Database");

    }
    private ItemDatabaseObject database;
    public Dropchance[] dropTable;
    public float Health = 100f;
    public float Resistance;
    public float MagResistance;

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            SpawnLoot();
        }
    }

    public void TakePhysicalDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * Resistance);
        //   Health -= takenDamge;
    }
    public void TakeMagicDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * MagResistance);
        //  Health -= takenDamge;
    }

    public void SpawnLoot()
    {
        // we loop through each item
        // get a random number
        // check if number is within the percentage
        // spawn item if it is
        // don't spawn item if it's not.
    }
}

public class Dropchance
{
    public float dropchance;
    public ItemObject item;
}