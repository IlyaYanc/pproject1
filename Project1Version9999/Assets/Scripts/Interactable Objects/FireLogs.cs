using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogs : Interactable
{
    [SerializeField] 
    private SavingSystem saving;

    [SerializeField]
    private GameObject saveNote;

    public override void Interact()
    {
        saving.Save();
        saveNote.SetActive(true);
    }
}
