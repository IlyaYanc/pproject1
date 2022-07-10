using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType
{
    Knight,
    Archer,
    Mage,
    Thief
}

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Weapon")]
public class WeaponItem : ItemBase
{
    public weaponType weaponType;
    public float damage;
    public float hasFire;
    public RuntimeAnimatorController animator;
    public void Awake()
    {
        type = ItemType.Weapon;
        canBeApplied = true;
    }
    
}
