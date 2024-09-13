using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Tool,
    Material,
    Currency,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public ItemType type;

    [TextArea(15, 20)]
    public string Desc;
    public Sprite itemImg;
}
