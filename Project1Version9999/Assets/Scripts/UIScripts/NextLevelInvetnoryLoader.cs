using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelInvetnoryLoader : MonoBehaviour
{
    [SerializeField] private SavingSystem savingManager;
    [SerializeField] private bool traningScene = false;
    private void Start()
    {
        if(!traningScene)
            savingManager.LoadInventory();
    }
}
