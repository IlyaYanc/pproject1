using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLadder : Interactable
{
    [SerializeField] private bool savesInventory;
    [SerializeField] private InventoryComponent inventoryComponent; 
    [SerializeField] private string NextLevelName;
    [SerializeField] private bool InstantSceneLoad;
    [SerializeField] private DeathAudioSourceController deathAudioSourceController;
    [SerializeField] private DeathLevelObjectsController deathLevelObjectsController;
    [SerializeField] private AudioSource levelMusic;
    [SerializeField] private GameObject levelEndPanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject mainMenuButton;
    private void Start()
    {
        
    }
    public override void Interact()
    {
        if(savesInventory)
        {
            SaveStats();
        }
        
        if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) != -1 && NextLevelName != "Start_Scene")
        {
            PlayerPrefs.SetString("LastLevel", NextLevelName);
        }
        if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) == -1 || InstantSceneLoad == false)
        {
            
            deathAudioSourceController.DisableAudioSources();
            deathLevelObjectsController.DisableLevelObjects();
            levelMusic.Stop();
            levelEndPanel.GetComponent<StartLevelEnd>().Activate();
            if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) == -1)
            {
                mainMenuButton.SetActive(true);
                return;
            }
            if (InstantSceneLoad == false)
            {
                if (continueButton != null)
                {
                    continueButton.SetActive(true);
                    return;   
                }
            }
        }
        else
        {
            LoadNextLevel();
        }
        
    }

    private void SaveStats()
    {
        if (inventoryComponent != null)
        {
            inventoryComponent.Save();
        }
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath(NextLevelName));   
    }
}
