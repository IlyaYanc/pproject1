using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject QuestionPanel;
    public GameObject StartPanel;
    public GameObject TrainingPanel;

    private void Start()
    {
        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetString("LastLevel", "level1");
        }
    }
    public void ExitButton()
    {
        Application.Quit(0);
    }
    public void StartButton()
    {
        if(!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            TrainingPanel.SetActive(true);
            StartPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            if(PlayerPrefs.HasKey("LastLevel"))
                SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LastLevel")); // nado last level
            else 
                SceneManager.LoadSceneAsync("level1");
        }
    }
    public void TrainingYes()
    {
        SceneManager.LoadSceneAsync("TrainingScene");
    }

    public void TrainingNo()
    {
        SceneManager.LoadSceneAsync("level1");
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
        StartPanel.SetActive(false);
    }
    public void ExitSettings()
    {
        SettingsPanel.SetActive(false);
        StartPanel.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Questions()
    {
        QuestionPanel.SetActive(true);
        StartPanel.SetActive(false);
    }
    public void ExitQuestions()
    {
        QuestionPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

}
