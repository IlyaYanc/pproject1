using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public InventoryItem chestPlate;
    public InventoryItem hat;
    [SerializeField] private HP hp;
    [SerializeField] private InventoryRenderer inventoryRenderer;

    [SerializeField] private equpmentClassType classType;
    [SerializeField] private Animator bodyClothesAnimator;
    [SerializeField] private RuntimeAnimatorController bodyClothesAnimatorDefault;
    [SerializeField] private Animator hatClothesAnimator;
    [SerializeField] private RuntimeAnimatorController hatClothesAnimatorDefault;
    
    private float chestPlateDefence = 0f;
    private float hatDefence = 0f;
    

    public void ApplyChestPlate(InventoryItem _invItem, bool isApplied)
    {
        
        if (chestPlate != null)
        {
            hp.EncreaseResist(CalculateResist(chestPlateDefence)); //the lower resist is the lower damage goes
            
        }
        
        if (isApplied || _invItem == null)
        {
            chestPlate = null;
            chestPlateDefence = 0;
            bodyClothesAnimator.runtimeAnimatorController = bodyClothesAnimatorDefault;
            return;
        }
        EquipmentItem item = (EquipmentItem)_invItem.item;
        chestPlate = _invItem;
        chestPlateDefence = _invItem.defence;
        hp.DecreaseResist(CalculateResist(chestPlateDefence));
        bodyClothesAnimator.runtimeAnimatorController = item.animator;
    }
    public void ApplyHat(InventoryItem _invItem, bool isApplied)
    {
        
        if (hat != null)
        {
            hp.EncreaseResist(CalculateResist(hatDefence)); //the lower resist is the lower damage goes
        }
        if (isApplied || _invItem == null)
        {
            hat = null;
            hatDefence = 0;
            hatClothesAnimator.runtimeAnimatorController = hatClothesAnimatorDefault;
            return;
        }
        EquipmentItem item = (EquipmentItem)_invItem.item;
        hat = _invItem;
        hatDefence = _invItem.defence;
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
