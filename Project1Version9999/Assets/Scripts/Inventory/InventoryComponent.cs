using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using BayatGames.SaveGameFree;
using NaughtyAttributes;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> items;
    [SerializeField] private InventoryRenderer inventoryRenderer;
    [SerializeField] private List<InventoryItem> appliedItems;
    [SerializeField] private int cellsCount = 8;
    [SerializeField] private LootManager lootManager;
    [SerializeField] private ItemBase[] cashItems;


    [SerializeField] private int lvl = 1;
    [SerializeField] private float k = 1; //коэффициент, на который умножается уровень, для выдачи урона оружию

    private void Start()
    {
        if(lootManager != null)
            cashItems = lootManager.GetItems();
    }

    public void AddItem(ItemBase _item)
    {
        if (cellsCount <= items.Count) return;
        if(_item.stackable)
        {
            bool itemIsAdded = false;
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].ReturnItem() == _item)
                {
                    items[i].IncreaseAmount(1);
                    itemIsAdded = true;
                    break;
                }
            }
            if(!itemIsAdded)
            {
                items.Add(new InventoryItem(_item));
            }
        }
        else
        {
            switch (_item.type)
            {
                case ItemType.Weapon:
                    items.Add(new InventoryItem(_item, k, lvl));
                    break;
                case ItemType.Equipment:
                    items.Add(new InventoryItem(_item, k, lvl));
                    break;
                default:
                    items.Add(new InventoryItem(_item));
                    break;
            }
        }
        inventoryRenderer.UpdateInventory(items);
    }

    public List<InventoryItem> ReturnItems()
    {
        return items;
    }

    public void ChangeItem(InventoryItem _item, int _x)
    {
        items[_x] = _item;
    }

    public List<InventoryItem> ReturnAppliedItems()
    {
        return appliedItems;
    }

    public void GetAppliedItems(List<InventoryItem> _appliedItems)
    {
        appliedItems = _appliedItems;
    }

    public bool HasFreeCells()
    {
        return cellsCount > items.Count;
    }

    public void DeleteItem(int _x)
    {
        items.RemoveAt(_x);
    }
    
    
    //saving system
    [Button("SaveInv")]
    public void Save()
    {
        SaveGame.Save<List<InventoryItem>>("InventorySaveFile", items);
        //Debug.Log(items);
    }

    [Button("LoadInv")]
    public void Load()
    {
        //cashItems = lootManager.GetItems();
        items = SaveGame.Load<List<InventoryItem>>("InventorySaveFile");
        foreach (var item in items)
        {
            foreach (var cashItem in cashItems)
            {
                if (cashItem.itemName != item.item.itemName) continue;
                
                item.item = cashItem;
                break;
            }
        }
        //Debug.Log(SaveGame.Load<List<InventoryItem>>("InventorySaveFile"));
        inventoryRenderer.UpdateInventory(items);
        inventoryRenderer.ActiveItemUpdate(null, 0);
        
    }
}


