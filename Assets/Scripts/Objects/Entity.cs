using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
<<<<<<< HEAD
    public float Health = 100f;
=======
    private void OnEnable()
    {
        // it will automatically set the database as the database object
        database = Resources.Load<ItemDatabaseObject>("Database");

    }
    private ItemDatabaseObject database;
    public Dropchance[] dropTable;
    public float Health = 100f;
    protected float maxHealth;
>>>>>>> 343e8fa243504e919dacceb1334f0c6e68c9a755
    public float Resistance;
    public float MagResistance;
    public int experienceReward = 25;

    private ExperienceManager experienceManager;

    void Start()
    {
<<<<<<< HEAD
        if (Health <= 0)
        {
            Destroy(gameObject);
=======
        
        experienceManager = FindObjectOfType<ExperienceManager>();
    }

    // virtual, in this case, is the keyword for override. It makes it possible to change the same method later on
    protected virtual void Awake()
    {
        maxHealth = Health;
    }

    protected virtual void Update()
    {
        Debug.Log("checking..." + gameObject.name);
        if (Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("health is zero...");
            SpawnLoot();
            GrantExperience();
>>>>>>> 343e8fa243504e919dacceb1334f0c6e68c9a755
        }
    }

    public void TakePhysicalDmg(float Damage)
    {
<<<<<<< HEAD
        float takenDamage = Damage - (0.75f * Resistance);
        Health -= takenDamage;
=======
        float takenDamge = Damage - (0.75f * Resistance);
        Health -= takenDamge;
>>>>>>> 343e8fa243504e919dacceb1334f0c6e68c9a755
    }

    public void TakeMagicDmg(float Damage)
    {
<<<<<<< HEAD
        float takenDamage = Damage - (0.75f * MagResistance);
        Health -= takenDamage;
    }
}
=======
        float takenDamge = Damage - (0.75f * MagResistance);
        Health -= takenDamge;
    }

    private void GrantExperience()
    {
        if (experienceManager != null)
        {
            experienceManager.AddExperience(experienceReward); 
        }
    }

    public void SpawnLoot()
    {
        // don't do anything if it hasn't anything to drop.
        if (dropTable.Length == 0)
        {
            return;
        }
        // we loop through each item
        for (int i = 0; i < dropTable.Length; i++)
        {
            // get a random number
            float randomNumber = Random.Range(0f, 100f);
            Debug.Log(randomNumber);
            Debug.Log(dropTable[i].dropchance);

            // check if number is within the percentage
            if (randomNumber <= dropTable[i].dropchance)
            {
                // spawn prefab
                GameObject item = Instantiate(dropTable[i].DropItemPrefab, transform.position, Quaternion.identity);

                // add the invenoryItem to the 3d object
                item.GetComponent<GroundItem>().item = dropTable[i].item;
            }
        }
    }
}
[System.Serializable]
public class Dropchance
{
    [Range(0f, 100f)]
    public float dropchance;
    public ItemObject item;
    public GameObject DropItemPrefab;
}
>>>>>>> 343e8fa243504e919dacceb1334f0c6e68c9a755
