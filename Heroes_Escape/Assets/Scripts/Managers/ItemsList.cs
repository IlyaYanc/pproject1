using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Items List", menuName = "Inventory/ItemsList")]
public class ItemsList : ScriptableObject
{
    public ItemBase[] Items;
}
