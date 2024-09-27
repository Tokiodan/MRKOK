using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;
[CreateAssetMenu(fileName = "New Magic Object", menuName = "Inventory/Items/Magic")]
public class MagicObject : ItemObject
{
    public float attackDamage;
    public float cooldown;
    public MagicAttack mgattack;
    public void Awake()
    {
        type = ItemType.Magic;
    }
}