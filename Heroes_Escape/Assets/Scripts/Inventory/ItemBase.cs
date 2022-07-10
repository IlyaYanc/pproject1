using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Weapon,
    Default
}

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite inventorySprite;
    public ItemType type;
    public bool stackable;
    public bool canBeApplied;
    [TextArea(5,10)]
    public string description;
}
