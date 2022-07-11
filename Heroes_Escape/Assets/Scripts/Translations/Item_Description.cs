using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_DescriptionPair", menuName = "Item_DescriptionPair", order = 51)]
public class Item_Description : ScriptableObject
{
    public ItemBase item;
    [TextArea]
    public string description;
    public string itemname;
}
