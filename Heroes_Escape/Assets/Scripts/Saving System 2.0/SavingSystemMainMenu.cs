using System;
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
}
