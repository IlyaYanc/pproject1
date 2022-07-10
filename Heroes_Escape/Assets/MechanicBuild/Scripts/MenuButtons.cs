using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject ChosenPanel;
    public GameObject CommonPanel;
    public GameObject Cross;
    public GameObject MainCanvas;
    public GameObject InventoryPanel;
    public GameObject SettingsPanel;
    public GameObject Player;

    public bool Control; //0 - cross, 1 - touch
    public GameObject ToggleCross;
    public GameObject ToggleTouch;
    public Image CrossRender;
    public Image TouchRender;
    public Sprite ToggleOn;
    public Sprite ToggleOff;
    void Start()
    {
        Control = false; //потом сохранять
        CrossRender = ToggleCross.GetComponent<Image>();
        TouchRender = ToggleTouch.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuButton()
    {
        Time.timeScale = 0f;
        MenuPanel.SetActive(true);
        CommonPanel.SetActive(false);
        ChosenPanel = MainCanvas.GetComponent<ChangeBodeButtons>().ChosenPanel;
        ChosenPanel.SetActive(false);
    }

    public void ExitMenu()
    {
        Time.timeScale = 1f;
        MenuPanel.SetActive(false);
        CommonPanel.SetActive(true);
        ChosenPanel = MainCanvas.GetComponent<ChangeBodeButtons>().ChosenPanel;
        ChosenPanel.SetActive(true);
    }



    public void InventoryButton()
    {
        InventoryPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void ExitInventory()
    {
        InventoryPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void SettingsButton()
    {
        SettingsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void ExitSettings()
    {
        SettingsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void ControlButton()
    {
        bool Done = false;
        if(Control == false && Done == false)
        {
            Debug.Log("ahaha");
            Control = true;
            Done = true;
            Cross.SetActive(false);
            Player.GetComponent<TouchPlayerController>().enabled = true;
            TouchRender.sprite = ToggleOn;
            CrossRender.sprite = ToggleOff;
        }
        if(Control == true && Done == false)
        {
            Debug.Log("ohohoh");
            Done = true;
            Control = false;
            Cross.SetActive(true);
            Player.GetComponent<TouchPlayerController>().enabled = false;
            TouchRender.sprite = ToggleOff;
            CrossRender.sprite = ToggleOn;
        }
    }


    public MenuSavingData SaveData()
    {
        MenuSavingData data = new MenuSavingData(Control);
        return data;
    }

    public void LoadData(MenuSavingData _data)
    {
        Control = _data.Control;
    }
}

public class MenuSavingData
{
    public bool Control;

    public MenuSavingData(bool _control)
    {
        Control = _control;
    }
}