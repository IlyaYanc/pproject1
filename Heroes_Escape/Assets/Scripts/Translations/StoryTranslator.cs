using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTranslator : MonoBehaviour
{
    [SerializeField] private StoryScript story;
    [SerializeField] private LevelTranslation english;

    private void Start()
    {
        if (Application.systemLanguage != SystemLanguage.Russian)
        {
            Translate(story.StoryText, english);
        }
    }

    public void Translate(string[] storyTexts, LevelTranslation language)
    {
        for(int i=0;i<storyTexts.Length;i++)
        {
            storyTexts[i] = language.textTranslations[i];
        }
    }
}
