using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBodeButtons : MonoBehaviour
{
    public GameObject WalkPanel;
    public GameObject FightPanel;
    public GameObject ChosenPanel;
    void Start()
    {
        ChosenPanel = WalkPanel;
    }

    public void FightModeButton()
    {
        WalkPanel.SetActive(false);
        FightPanel.SetActive(true);
        ChosenPanel = FightPanel;
    }

    public void WalkModeButton()
    {
        WalkPanel.SetActive(true);
        FightPanel.SetActive(false);
        ChosenPanel = WalkPanel;
    }
}
