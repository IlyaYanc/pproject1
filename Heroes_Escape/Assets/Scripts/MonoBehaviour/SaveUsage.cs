using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BayatGames.SaveGameFree;

public class SaveUsage : MonoBehaviour
{
    [SerializeField] private string saveTextKey;

    [SerializeField] private TMP_InputField textInput;

    public void Save()
    {
        SaveGame.Save<string>(saveTextKey, textInput.text);
    }

    public void Load()
    {
        textInput.text = SaveGame.Load<string>(saveTextKey);
    }
}
