using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public EquipmentItem chestPlate;
    public EquipmentItem hat;
    [SerializeField] private HP hp;
    [SerializeField] private InventoryRenderer inventoryRenderer;

    [SerializeField] private equpmentClassType classType;
    [SerializeField] private Animator bodyClothesAnimator;
    [SerializeField] private RuntimeAnimatorController bodyClothesAnimatorDefault;
    [SerializeField] private Animator hatClothesAnimator;
    [SerializeField] private RuntimeAnimatorController hatClothesAnimatorDefault;
    
    private float chestPlateDefence = 0f;
    private float hatDefence = 0f;
    

    public void ApplyChestPlate(EquipmentItem item, bool isApplied)
    {
        if (chestPlate != null)
        {
            hp.EncreaseResist(CalculateResist(chestPlateDefence)); //the lower resist is the lower damage goes
        }
        if (isApplied || item == null)
        {
            chestPlate = null;
            chestPlateDefence = 0;
            bodyClothesAnimator.runtimeAnimatorController = bodyClothesAnimatorDefault;
            return;
        }
        chestPlate = item;
        chestPlateDefence = item.defence;
        hp.DecreaseResist(CalculateResist(chestPlateDefence));
        bodyClothesAnimator.runtimeAnimatorController = item.animator;
    }
    public void ApplyHat(EquipmentItem item, bool isApplied)
    {
        if (hat != null)
        {
            hp.EncreaseResist(CalculateResist(hatDefence)); //the lower resist is the lower damage goes
        }
        if (isApplied || item == null)
        {
            hat = null;
            hatDefence = 0;
            hatClothesAnimator.runtimeAnimatorController = hatClothesAnimatorDefault;
            return;
        }
        hat = item;
        hatDefence = item.defence;
        hp.DecreaseResist(CalculateResist(hatDefence));
        hatClothesAnimator.runtimeAnimatorController = item.animator;
    }

    public equpmentClassType EqupmentClassType()
    {
        return classType;
    }
    
    
    /*public void ChangeEquipment(InventoryItem invItem, bool isApplied)
    {
        EquipmentItem item = (EquipmentItem)(invItem.item) ;
        switch (item.equipmentType)
        {
            case equipmentType.ChestPlate:
                if (chestPlate != new InventoryItem())
                {
                    //Debug.Log(chestPlate.item);
                    UnequipItem(chestPlate);
                    inventoryRenderer.UnapplyItem(chestPlate);
                    chestPlate = new InventoryItem();

                }
                if (!isApplied)
                {
                    EquipItem(invItem);
                    chestPlate = invItem;
                }
                break;
            
            case equipmentType.Hat:
                if (hat != new InventoryItem())
                {
                    Debug.Log("UnequipItem");
                    UnequipItem(hat);
                    inventoryRenderer.UnapplyItem(hat);
                    hat = new InventoryItem();
                    
                }
                if (!isApplied)
                {
                    EquipItem(invItem);
                    hat = invItem;
                }
                break;
        }
    }*/
    /*private void EquipItem(InventoryItem invItem)
    {
        if(invItem.item == null)
            return;
        
        hp.DecreaseResist(CalculateResist((EquipmentItem)(invItem.item)));
        
    }
    private void UnequipItem(InventoryItem invItem)
    {
        if(invItem.item == null)
            return;
        hp.EncreaseResist(CalculateResist((EquipmentItem)(invItem.item)));
        
    }*/
    private float CalculateResist(float _defence)
    {
        Debug.Log((_defence / 100f) * 0.3f);
        return (_defence/ 100f) * 0.3f;
    }
    
}
