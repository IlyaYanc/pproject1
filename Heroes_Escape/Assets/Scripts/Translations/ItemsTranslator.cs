using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTranslator : MonoBehaviour
{
    [SerializeField] private ItemsTranslations english;
    [SerializeField] private ItemsTranslations russian;
    [HideInInspector] public ItemsTranslations usedTranslations;

    private void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Russian)
        {
            usedTranslations = russian;
            return;
        }

        if (Application.systemLanguage != SystemLanguage.Russian)
        {
            usedTranslations = english;
        }

    }
}
