using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InventoryItem
{
    public ItemBase item;
    public int amount = 1;
    public bool isApplied = false;
    public float damage;

    public void SetAmount(int _amount)
    {
        amount = _amount;
    }
    public void IncreaseAmount(int _amount)
    {
        amount += _amount;
    }
    public InventoryItem(ItemBase _item)
    {
        item = _item;
        amount = 1;
    }
    public InventoryItem()
    {
        item = null;
        amount = 0;
    }
    public InventoryItem(ItemBase _item, float _lvlK)
    {
        item = _item;
        amount = 1;
        if(item.type == ItemType.Weapon)
        {
            damage = ((WeaponItem)_item).damage * _lvlK * UnityEngine.Random.Range(0.75f, 1.25f);
            damage = (float)Math.Round(damage, 1);
        }
    }
   

    public ItemBase ReturnItem()
    {
        return item;
    }
}