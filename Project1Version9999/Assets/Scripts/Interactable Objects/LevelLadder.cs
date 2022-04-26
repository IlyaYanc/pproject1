﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLadder : Interactable
{
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
        if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) == -1 || InstantSceneLoad == false)
        {
            deathAudioSourceController.DisableAudioSources();
            deathLevelObjectsController.DisableLevelObjects();
            levelMusic.Stop();
            levelEndPanel.SetActive(true);
            if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) != -1 && InstantSceneLoad == false)
            {
                continueButton.SetActive(true);
            }
            else if (SceneUtility.GetBuildIndexByScenePath(NextLevelName) == -1)
            {
                mainMenuButton.SetActive(false);
            }
            
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath(NextLevelName));   
    }
}
