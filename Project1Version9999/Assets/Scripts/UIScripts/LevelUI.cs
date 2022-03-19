using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    public GameObject WalkUPButton;
    [SerializeField]
    public GameObject WalkDownButton;
    [SerializeField]
    public GameObject WalkLeftButton;
    [SerializeField]
    public GameObject WalkRightButton;
    [SerializeField]
    public GameObject RotateLeftButton;
    [SerializeField]
    public GameObject RotateRightButton;

    [SerializeField]
    public Button[] ActionButtons = new Button[0];


    [SerializeField]
    public GameObject KnightConstantText;
    [SerializeField]
    public GameObject KnightDamageText;

    [SerializeField]
    public GameObject Player;
    private PlayerMoveController MoveController;
    [SerializeField]
    private TouchPlayerController TouchConroller;
    private Abilities Ability;
    private Interact Interact;


    [SerializeField]
    public GameObject Knight;
    private AttackController KnightAttack;
    public GameObject KnightAttackUI;

    [SerializeField]
    public GameObject Thief;
    private AttackController ThiefAttack;
    public GameObject ThiefAttackUI;

    [SerializeField]
    public GameObject Archer;
    private AttackController ArcherAttack;
    public GameObject ArcherAttackUI;

    [SerializeField]
    public GameObject Mage;
    private AttackController MageAttack;
    public GameObject MageAttackUI;
    public GameObject BoostAttackUI;
    public SliderTimer BoostAttackTimer;
    public GameObject BoostHPUI;
    public SliderTimer BoostHPTimer;
    public GameObject BoostResistUI;
    public SliderTimer BoostResistTimer;
    public GameObject DisableAbilitiesMenuButton;
    public GameObject AbilityPlayerChoiceMenu;
    public GameObject SelectedAbilityButton;
    public SliderTimer SelectedAbilityTimer;

    [SerializeField]
    public GameObject CommonPanel;
    [SerializeField]
    public GameObject ChosenGamePanel;
    [SerializeField]
    public GameObject WalkPanel;
    [SerializeField]
    public GameObject FightPanel;

    [SerializeField]
    public GameObject PauseButton;
    [SerializeField]
    public GameObject PausePanel;

    [SerializeField]
    public GameObject MenuPanel;
    [SerializeField]
    public GameObject InventoryPanel;
    [SerializeField]
    public GameObject SettingsPanel;
    [SerializeField]
    public GameObject ExitPanel;

    [SerializeField]
    public Sprite ActiveWalkModeSprite;
    [SerializeField]
    public Sprite InActiveWalkModeSprite;
    [SerializeField]
    public GameObject CrossToggle;
    [SerializeField]
    public GameObject SwipeToggle;
    public bool IsCross = true;

    [SerializeField] private Sprite ActiveInteractSprite, InactiveInteractPicture;
    [SerializeField] public GameObject InteractButtonPicture;

    private bool TouchControllerEnabled;








    private void Start()
    {
        MoveController = Player.GetComponent<PlayerMoveController>();
        //TouchConroller = Player.GetComponent<TouchPlayerController>();
        Interact = Player.GetComponent<Interact>();
        KnightAttack = Knight.GetComponent<AttackController>();
        ThiefAttack = Thief.GetComponent<AttackController>();
        ArcherAttack = Archer.GetComponent<AttackController>();
        MageAttack = Mage.GetComponent<AttackController>();
        ChosenGamePanel = WalkPanel;
        EqualTextSize(KnightDamageText, KnightConstantText);
    }
    public void EqualTextSize(GameObject obj1, GameObject obj2)
    {
        obj1.GetComponent<TextMeshProUGUI>().fontSize = obj2.GetComponent<TextMeshProUGUI>().fontSize;
    }

    public void WalkUP()
    {
        MoveController.MoveUp();
    }
    public void WalkDown()
    {
        MoveController.MoveDown();
    }
    public void WalkLeft()
    {
        MoveController.MoveLeft();
    }
    public void WalkRight()
    {
        MoveController.MoveRight();
    }
    public void RotateLeft()
    {
        MoveController.RotateLeft();
    }
    public void RotateRight()
    {
        MoveController.RotateRight();
    }
    public void TryInteract()
    {
        Interact.TryToInteract();
    }

    public void AttackKnight()
    {
        KnightAttack.Attack();
    }
    public void AttackThief()
    {
        ThiefAttack.Attack();
    }
    public void AttackArcher()
    {
        ArcherAttack.Attack();
    }
    public void AttackMage()
    {
        MageAttack.Attack();
    }

    public void BoostAttackAbility()
    {
        //Ability.DamageBoost();
    }
    public void BoostHPAbility()
    {
        //Ability.HpBoost();
    }
    public void BoostReststAbility()
    {
        //Ability.DefenceBoost();
    }

    public void EnterFightMode()
    {
        FightPanel.SetActive(true);
        WalkPanel.SetActive(false);
        ChosenGamePanel = FightPanel;
    }
    public void EnterWalkMode()
    {
        FightPanel.SetActive(false);
        WalkPanel.SetActive(true);
        ChosenGamePanel = WalkPanel;
    }

    private void PauseGame()
    {
        ChosenGamePanel.SetActive(false);
        CommonPanel.SetActive(false);
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        ChosenGamePanel.SetActive(true);
        CommonPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OpenMenu()
    {
        PausePanel.SetActive(true);
        PauseGame();
    }
    public void ExitMenu()
    {
        PausePanel.SetActive(false);
        ResumeGame();
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void ExitSettings()
    {
        SettingsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void OpenInventory()
    {
        InventoryPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void ExitInventory()
    {
        InventoryPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void OpenExitPanel()
    {
        ExitPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void ExitExitPanel()
    {
        ExitPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void ChangeWalkMode()
    {
        //if(TouchConroller.enabled == false)
        if(TouchControllerEnabled == false)
        {
            WalkUPButton.SetActive(false);
            WalkDownButton.SetActive(false);
            WalkLeftButton.SetActive(false);
            WalkRightButton.SetActive(false);
            RotateLeftButton.SetActive(false);
            RotateRightButton.SetActive(false);
            CrossToggle.GetComponent<Image>().sprite = InActiveWalkModeSprite;
            SwipeToggle.GetComponent<Image>().sprite = ActiveWalkModeSprite;
            TouchControllerEnabled = true;
            //TouchConroller.enabled = true;
            IsCross = false;
            return;
        }
        //if(TouchConroller.enabled == true)
        if(TouchControllerEnabled == true)
        {
            WalkUPButton.SetActive(true);
            WalkDownButton.SetActive(true);
            WalkLeftButton.SetActive(true);
            WalkRightButton.SetActive(true);
            RotateLeftButton.SetActive(true);
            RotateRightButton.SetActive(true);
            CrossToggle.GetComponent<Image>().sprite = ActiveWalkModeSprite;
            SwipeToggle.GetComponent<Image>().sprite = InActiveWalkModeSprite;
            TouchControllerEnabled = false;
            //TouchConroller.enabled = false;
            IsCross = true;
            return;
        }
    }

    private void SetWalkMode(bool _isTouchInput)
    {
        if (_isTouchInput)
        {
            WalkUPButton.SetActive(false);
            WalkDownButton.SetActive(false);
            WalkLeftButton.SetActive(false);
            WalkRightButton.SetActive(false);
            RotateLeftButton.SetActive(false);
            RotateRightButton.SetActive(false);
            CrossToggle.GetComponent<Image>().sprite = InActiveWalkModeSprite;
            SwipeToggle.GetComponent<Image>().sprite = ActiveWalkModeSprite;
            //TouchControllerEnabled = true;
            TouchConroller.enabled = true;
            IsCross = false;
        }
        else
        {
            WalkUPButton.SetActive(true);
            WalkDownButton.SetActive(true);
            WalkLeftButton.SetActive(true);
            WalkRightButton.SetActive(true);
            RotateLeftButton.SetActive(true);
            RotateRightButton.SetActive(true);
            CrossToggle.GetComponent<Image>().sprite = ActiveWalkModeSprite;
            SwipeToggle.GetComponent<Image>().sprite = InActiveWalkModeSprite;
            //TouchControllerEnabled = false;
            TouchConroller.enabled = false;
            IsCross = true;
        }
    }

    public void SaveData()
    {
        SaveGame.Save("InputType", TouchConroller.enabled);
    }

    public bool LoadData()
    {
        if(!SaveGame.Exists("InputType"))
            return TouchConroller.enabled;
        SetWalkMode(SaveGame.Load<bool>("InputType"));
        return SaveGame.Load<bool>("InputType");
    }
    

    public void UpdateTimerMaxValue(GameObject Player, GameObject Slider)   //вызывать при смене оружия
    {
        //Slider.GetComponent<SliderTimer>().m_duration = Player.GetComponent<AttackController>().cooldown;
    }

    public void DisableKnightAttackButton()
    {
        KnightAttackUI.GetComponent<Button>().enabled = false;
    }
    public void DisableThiefAttackButton()
    {
        ThiefAttackUI.GetComponent<Button>().enabled = false;
    }
    public void DisableArcherAttackButton()
    {
        ArcherAttackUI.GetComponent<Button>().enabled = false;
    }
    public void DisableMageAttackButton()
    {
        MageAttackUI.GetComponent<Button>().enabled = false;
    }

    public void DisableAttackBoost()
    {
        BoostAttackUI.GetComponent<Button>().enabled = false;
    }
    public void DisableHPBoost()
    {
        BoostHPUI.GetComponent<Button>().enabled = false;
    }
    public void DisableResistBoost()
    {
        BoostResistUI.GetComponent<Button>().enabled = false;
    }
    public void DisableAbilitySelectMenu()
    {
        DisableAbilitiesMenuButton.SetActive(false);
        AbilityPlayerChoiceMenu.SetActive(false);
    }

    public void AttackBoostChoise()
    {
        SelectedAbilityButton = BoostAttackUI;
        SelectedAbilityTimer = BoostAttackTimer;
        DisableAbilitiesMenuButton.SetActive(true);
    }
    public void HPBoostChoise()
    {
        SelectedAbilityButton = BoostHPUI;
        SelectedAbilityTimer = BoostHPTimer;
        DisableAbilitiesMenuButton.SetActive(true);
    }
    public void ResistBoostChoise()
    {
        SelectedAbilityButton = BoostResistUI;
        SelectedAbilityTimer = BoostResistTimer;
        DisableAbilitiesMenuButton.SetActive(true);
    }
    public void AbilityPlayerChoise()
    {
        SelectedAbilityTimer.RestartTimer();
        SelectedAbilityButton.GetComponent<Button>().enabled = false;
        AbilityPlayerChoiceMenu.SetActive(false);
        DisableAbilitiesMenuButton.SetActive(false);
    }

    public void SetActiveInteractPicture()
    {
        InteractButtonPicture.GetComponent<Image>().sprite = ActiveInteractSprite;
    }
    public void SetInactiveInteractPicture()
    {
        InteractButtonPicture.GetComponent<Image>().sprite = InactiveInteractPicture;
    }

    public void DisableTouchOnButtonPointerDown()
    {
        if(CrossToggle.GetComponent<Image>().sprite == InActiveWalkModeSprite)
        {
            Player.GetComponent<TouchPlayerController>().enabled = false;
        }
    }
    public void EnableTouchOnButtonPointerDown()
    {
        if(CrossToggle.GetComponent<Image>().sprite == InActiveWalkModeSprite)
        {
            Player.GetComponent<TouchPlayerController>().enabled = true;
        }
    }

    public void DisableActionButtons()
    {
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionButtons[i].enabled = false;
        }
        /*WalkUPButton.GetComponent<Button>().enabled = false;
        WalkLeftButton.GetComponent<Button>().enabled = false;
        WalkRightButton.GetComponent<Button>().enabled = false;
        WalkDownButton.GetComponent<Button>().enabled = false;
        RotateLeftButton.GetComponent<Button>().enabled = false;
        RotateRightButton.GetComponent<Button>().enabled = false; */
    }
    public void EnableActionButtons()
    {
        for(int i = 0; i<ActionButtons.Length; i++)
        {
            ActionButtons[i].enabled = true;
        }
        /*WalkUPButton.GetComponent<Button>().enabled = true;
        WalkLeftButton.GetComponent<Button>().enabled = true;
        WalkRightButton.GetComponent<Button>().enabled = true;
        WalkDownButton.GetComponent<Button>().enabled = true;
        RotateLeftButton.GetComponent<Button>().enabled = true;
        RotateRightButton.GetComponent<Button>().enabled = true;*/
    }

    public void PauseTouchController()
    {
        TouchControllerEnabled = TouchConroller.enabled;
        TouchConroller.enabled = false;
    }

    public void UnPauseTouchController()
    {
        TouchConroller.enabled = TouchControllerEnabled;
    }
}

