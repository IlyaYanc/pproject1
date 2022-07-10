using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    [SerializeField] private LevelTextsContainer levelTextContainer;
    [SerializeField] private LevelTranslation english;
    void Start()
    {
        // выбрать язык
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            Translate(english);
        }
    }

    public void Translate(LevelTranslation language)
    {
        for(int i=0;i<levelTextContainer.levelTexts.Length;i++)
        {
            levelTextContainer.levelTexts[i].text = language.textTranslations[i];
        }
    }
}
