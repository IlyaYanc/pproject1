using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory/Items/Food")]
public class FoodItem : ItemBase
{
    public int restoreHealthValue;
    public int defenceBoostValue;
    public float damageBoostValue;
    public float boostLength;
    public void Awake()
    {
        type = ItemType.Food;
        canBeApplied = false;
    }
}

