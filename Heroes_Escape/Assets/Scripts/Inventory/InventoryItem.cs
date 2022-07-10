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
    public float defence;
    public int lvl;
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
    public InventoryItem(ItemBase _item, float _multiplier, int _lvl)
    {
        item = _item;
        amount = 1;
        lvl = _lvl;
        switch (item.type)
        {
            case ItemType.Weapon:
                damage = ((WeaponItem)_item).damage * _multiplier * _lvl * UnityEngine.Random.Range(0.75f, 1.25f);
                damage = (float)Math.Round(damage, 1);
                break;
            case ItemType.Equipment:
                defence = ((EquipmentItem)_item).defence * _multiplier * _lvl * UnityEngine.Random.Range(0.75f, 1.25f);
                defence = (float)Math.Round(defence, 1);
                break;
        }
    }
   

    public ItemBase ReturnItem()
    {
        return item;
    }
}