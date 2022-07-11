using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsTranslation", menuName = "ItemsTranslation", order = 51)]
public class ItemsTranslations : ScriptableObject
{
    public Item_Description[] translations = new Item_Description[0];

    public string FindDescription(ItemBase key)
    {
        for(int i=0;i<translations.Length;i++)
        {
            if(translations[i].item == key)
            {
                return translations[i].description;
            }
        }
        return "";
    }

    public string FindName(ItemBase key)
    {
        for (int i = 0; i < translations.Length; i++)
        {
            if (translations[i].item == key)
            {
                return translations[i].itemname;
            }
        }
        return "";
    }


}
