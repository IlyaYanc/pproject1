using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class helpMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MP, IP, BP, AP, LP, SM, TrainingPanel;
    [SerializeField]
    private GameObject MB, IB, BB, AB, LB, TrainingButton, exitButton;
    private Color32 ActiveColor;
    private Color32 InactiveColor;
    private GameObject ActivePanel, ActiveButton;
    private void Start()
    {
        ActiveColor = new Color32(171, 142, 77, 255);
        InactiveColor = new Color32(75, 75, 75, 255);
        ActivePanel = MP;
        ActiveButton = MB;
    }

    public void Move_button()
    {
        DeactivatePanels(MP);
        DeactivateButtons(MB);
    }

    public void Inter_button()
    {
        DeactivatePanels(IP);
        DeactivateButtons(IB);
    }

    public void Battel_button()
    {
        DeactivatePanels(BP);
        DeactivateButtons(BB);
    }

    public void Abil_button()
    {
        DeactivatePanels(AP);
        DeactivateButtons(AB);
    }

    public void Loot_button()
    {
        DeactivatePanels(LP);
        DeactivateButtons(LB);
    }

    public void Training_Button()
    {
        TrainingPanel.SetActive(true);
        ActivePanel.SetActive(false);
        DisableButtons();
    }
    public void TrainingYes()
    {
        SceneManager.LoadSceneAsync("TrainingScene");
    }
    public void TrainingNo()
    {
        TrainingPanel.SetActive(false);
        ActivePanel.SetActive(true);
        EnableButtons();
    }

    public void Ex_button()
    {
        SM.SetActive(true);
        gameObject.SetActive(false);
    }

    private void DeactivatePanels(GameObject activePanel)
    {
        LP.SetActive(false);
        IP.SetActive(false);
        BP.SetActive(false);
        AP.SetActive(false);
        MP.SetActive(false);
        activePanel.SetActive(true);
        ActivePanel = activePanel;
    }

    private void DeactivateButtons(GameObject activeButton)
    {
        MB.GetComponent<Image>().color = InactiveColor;
        IB.GetComponent<Image>().color = InactiveColor;
        BB.GetComponent<Image>().color = InactiveColor;
        AB.GetComponent<Image>().color = InactiveColor;
        LB.GetComponent<Image>().color = InactiveColor;
        activeButton.GetComponent<Image>().color = ActiveColor;
        ActiveButton = activeButton;
    }

    private void EnableButtons()
    {
        MB.SetActive(true);
        IB.SetActive(true);
        BB.SetActive(true);
        AB.SetActive(true);
        LB.SetActive(true);
        TrainingButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private void DisableButtons()
    {
        MB.SetActive(false);
        IB.SetActive(false);
        BB.SetActive(false);
        AB.SetActive(false);
        LB.SetActive(false);
        TrainingButton.SetActive(false);
        exitButton.SetActive(false);
    }
}

