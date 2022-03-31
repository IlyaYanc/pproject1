using System.Collections;
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
        }
        else
        {
            Debug.Log("haha");
            SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath(NextLevelName));
        }
    }
}
