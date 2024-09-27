using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Tool,
    Magic,
    Material,
    Currency,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite itemImg;
    public ItemType type;

    [TextArea(15, 20)]
    public string Desc;

    public Dictionary<string, object> ItemDetails()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict["Item"] = this.Id;
        dict["Type"] = this.type;
        dict["desc"] = this.Desc;
        return dict;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}