using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("health is zero...");
            SpawnLoot();
        }
    }

    public void TakePhysicalDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * Resistance);
        Health -= takenDamge;
    }
    public void TakeMagicDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * MagResistance);
        Health -= takenDamge;
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
            int randomNumber = Random.Range(0, 100);
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
    [Range(0, 100)]
    public int dropchance;
    public ItemObject item;
    public GameObject DropItemPrefab;
}