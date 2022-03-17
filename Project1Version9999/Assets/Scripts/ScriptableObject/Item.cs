using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item")]
public class Item : ItemBase
{

    public Sprite dropSprite;
    [SerializeField] int rarity;
}
