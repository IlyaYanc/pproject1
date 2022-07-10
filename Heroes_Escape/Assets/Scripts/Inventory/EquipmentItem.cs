using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum equipmentType
{
    Hat,
    ChestPlate
}
public enum equpmentClassType
{
    Knight,
    Archer,
    Mage,
    Thief
}

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Items/Equipment")]

public class EquipmentItem : ItemBase
{
    public int defence;
    public RuntimeAnimatorController animator;
    public equpmentClassType classType;
    public equipmentType equipmentType;
    public void Awake()
    {
        type = ItemType.Equipment;
        canBeApplied = true;
    }

}
