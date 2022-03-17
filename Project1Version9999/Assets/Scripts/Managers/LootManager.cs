using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField]
    ItemBase[] itemsList;
    
    public ItemBase[] GetItems()
    {
        return itemsList;
    }
}