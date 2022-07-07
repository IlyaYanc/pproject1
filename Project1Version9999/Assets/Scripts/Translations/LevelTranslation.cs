using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTranslation", menuName = "LevelTranslation", order = 51)]
public class LevelTranslation : ScriptableObject
{
    [TextArea]
    public string[] textTranslations = new string[0];
}
