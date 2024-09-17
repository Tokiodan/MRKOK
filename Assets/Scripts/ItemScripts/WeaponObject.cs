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
}
