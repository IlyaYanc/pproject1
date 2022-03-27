using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLadder : Interactable
{
    [SerializeField] private DeathAudioSourceController deathAudioSourceController;
    [SerializeField] private DeathLevelObjectsController deathLevelObjectsController;
    [SerializeField] private AudioSource levelMusic;
    [SerializeField] private GameObject levelEndPanel;
    private void Start()
    {
        
    }
    public override void Interact()
    {
        if(SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            deathAudioSourceController.DisableAudioSources();
            deathLevelObjectsController.DisableLevelObjects();
            levelMusic.Stop();
            levelEndPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
