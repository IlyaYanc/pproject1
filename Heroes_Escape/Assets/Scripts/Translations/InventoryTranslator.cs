using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTranslator : MonoBehaviour
{
    [HideInInspector] public string typeText;
    [HideInInspector] public string baseDamageText;
    [HideInInspector] public string equipmentText;
    [HideInInspector] public string weaponText;
    [HideInInspector] public string foodText;
    [HideInInspector] public string applyText;
    [HideInInspector] public string takeOffText;
    [HideInInspector] public string useText;
    [HideInInspector] public string removeText;
    void Start()
    {
        Translate();
    }

    public void Translate()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            typeText = "ТИП:";
            baseDamageText = "\nБАЗОВЫЙ УРОН: ";
            equipmentText = "ЭКИПИРОВКА";
            weaponText = "ОРУЖИЕ";
            foodText = "ЕДА";
            applyText = "НАДЕТЬ";
            takeOffText = "СНЯТЬ";
            useText = "ИСПОЛЬЗОВАТЬ";
            removeText = "ВЫКИНУТЬ";
        }
        if (Application.systemLanguage != SystemLanguage.Russian)
        {
            typeText = "TYPE:";
            baseDamageText = "\nBASE DAMAGE: ";
            equipmentText = "EQUIPMENT";
            weaponText = "WEAPON";
            foodText = "FOOD";
            applyText = "APPLY";
            takeOffText = "TAKE OFF";
            useText = "USE";
            removeText = "REMOVE";
        }
    }
}
