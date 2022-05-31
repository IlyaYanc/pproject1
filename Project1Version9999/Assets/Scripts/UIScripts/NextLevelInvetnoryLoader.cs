using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelInvetnoryLoader : MonoBehaviour
{
    [SerializeField] private SavingSystem savingManager;

    private void Awake()
    {
        savingManager.LoadInventory();
    }
}
