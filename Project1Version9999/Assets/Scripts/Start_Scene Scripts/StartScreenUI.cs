using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject QuestionPanel;
    public GameObject StartPanel;
    public void ExitButton()
    {
        Application.Quit(0);
    }
    public void StartButton()
    {
        Application.LoadLevelAsync(1);
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
        // save
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
