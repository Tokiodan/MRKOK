using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public float damage;
    public float resistance;
    public float attackSpeed;
    private void Awake()
    {
        type = ItemType.Weapon;
    }

    public override Dictionary<string, object> ItemDetails()
    {
        Dictionary<string, object> dict = base.ItemDetails();
        dict["damage"] = damage;
        dict["attackSpeed"] = attackSpeed;
        dict["resistance"] = resistance;
        return dict;
    }
}
