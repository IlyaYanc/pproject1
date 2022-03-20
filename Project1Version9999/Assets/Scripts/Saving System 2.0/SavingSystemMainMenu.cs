using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystemMainMenu : MonoBehaviour
{
    [SerializeField] private SavingSystem savingSystem;
    [SerializeField] private Slider MS, ES;
    
    
    private void Start()
    {
        savingSystem.LoadSettings();
        savingSystem.LoadLvlUI();
    }

    public void Save()
    {
        Debug.Log("1");
        savingSystem.SaveSettings();
        savingSystem.SaveLvlUI();
    }
    public void Load()
    {
        savingSystem.LoadSettings();
        savingSystem.LoadLvlUI();
    }
}
