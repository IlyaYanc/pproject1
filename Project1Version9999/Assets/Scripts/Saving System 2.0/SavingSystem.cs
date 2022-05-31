using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using NaughtyAttributes;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    //player
    [SerializeField] private PlayerMoveController playerMoveController;
    [SerializeField] private PlayerSavingComponent playerSaver;
    
    //inventory
    [SerializeField] private InventoryComponent inventoryComponent;
    //scene
    [SerializeField] private List<activObject> activeObjects;
    [SerializeField] private List<Lever> levers;

    [SerializeField] private List<GameObject> enemies;
    //settings
    [SerializeField] private MenuButtons menuBtns;
    [SerializeField] private Options options;

    [SerializeField] private LevelUI lvlUI;

    private void SaveActiveObjects()
    {
        List<ActiveObjectSavingData> savingDatas = new List<ActiveObjectSavingData>();
        foreach (var obj in activeObjects)
        {
            savingDatas.Add(new ActiveObjectSavingData(obj.GetStage()));
        }
        SaveGame.Save("ActiveObjectsData", savingDatas);
    }
    private void LoadActiveObjects()
    {
        if (SaveGame.Exists("ActiveObjectsData"))
        {
            List<ActiveObjectSavingData> savingDatas;
            savingDatas = SaveGame.Load<List<ActiveObjectSavingData>>("ActiveObjectsData");
            
            if(activeObjects.Count != savingDatas.Count)
                return;
            
            for (int i = 0; i < savingDatas.Count; i++)
            {
                activeObjects[i].SetStage(savingDatas[i].stage);
            }
        }
    }

    private void SaveLevers()
    {
        List<LeverSavingData> savingDatas = new List<LeverSavingData>();
        foreach (var obj in levers)
        {
            savingDatas.Add(new LeverSavingData(obj.ReturnHasInterected()));
        }
        SaveGame.Save("LeversData", savingDatas);
    }
    private void LoadLevers()
    {
        if (SaveGame.Exists("LeversData"))
        {
            List<LeverSavingData> savingDatas;
            savingDatas = SaveGame.Load<List<LeverSavingData>>("LeversData");
            
            if(levers.Count != savingDatas.Count)
                return;
            
            for (int i = 0; i < savingDatas.Count; i++)
            {
                levers[i].LoadData(savingDatas[i]);
            }
        }
    }

    public void SaveSettings()
    {
        Time.timeScale = 1;
        if(menuBtns != null)
            SaveGame.Save("MenuData", menuBtns.SaveData());
        if (options != null)
        {
            SaveGame.Save("OptionsData", options.SaveData());
        }
        Time.timeScale = 0;
    }

    public void LoadSettings()
    {
        if (SaveGame.Exists("MenuData") && menuBtns != null)
            menuBtns.LoadData(SaveGame.Load<MenuSavingData>("MenuData"));

        if (SaveGame.Exists("OptionsData") && options != null)
        {
            options.LoadData(SaveGame.Load<OptionsSavingData>("OptionsData"));
        }  
    }

    public void LoadLvlUI()
    {
        if (lvlUI != null)
        {
            lvlUI.LoadData();
        }
    }

    public void SaveLvlUI()
    {
        lvlUI.SaveData();
    }
    
    [Button("Save Game")]
    public void Save()
    {
        playerSaver.Save();
        inventoryComponent.Save();
        SaveActiveObjects();
        SaveLevers();
        SaveSettings();
        lvlUI.SaveData();
        Time.timeScale = 1f;
    }
    
    [Button("Load Game")]
    public void Load()
    {
        playerSaver.Load();
        inventoryComponent.Load();
        LoadActiveObjects();
        LoadLevers();
        LoadSettings();
        LoadLvlUI();
        LoadEnemies();
        playerMoveController.RotateRight();
        //playerMoveController.RotateRight();//aaahhhhhhhhh
    }
    public void SaveInventory()
    {
        inventoryComponent.Save();
    }
    public void LoadInventory()
    {
        inventoryComponent.Load();
    }

    [Button("Load For Next Level")]
    public void LoadForNextLevel()
    {
        playerSaver.LoadForNextLevel();
        inventoryComponent.Load();
        LoadSettings();
        LoadLvlUI();
    }
    private void LoadEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<enemy>().Nondisquiet();
            enemies[i].GetComponent<EnemyHp>().SetMaxHP();
        }
    }
}
