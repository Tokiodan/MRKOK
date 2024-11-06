using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory/Items/Food")]
public class FoodObject : ItemObject
{
    public int healthRestore;
    public void Awake()
    {
        type = ItemType.Food;
    }

    public void HealPlayer()
    {
        PlayerEntity player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();

        player.Health += healthRestore;
        player.healthBar.SetSlider(player.Health);
    }

    public override Dictionary<string, object> ItemDetails()
    {
        Dictionary<string, object> dict = base.ItemDetails();
        dict["Food_Restore"] = healthRestore;
        return dict;
    }
}
