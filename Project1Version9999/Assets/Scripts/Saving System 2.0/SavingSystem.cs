using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using NaughtyAttributes;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    //player
    [SerializeField] private PlayerSavingComponent playerSaver;
    
    //inventory
    [SerializeField] private InventoryComponent inventoryComponent;
    //scene
    [SerializeField] private List<activObject> activeObjects;
    [SerializeField] private List<Lever> levers;
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
        if(lvlUI != null)
            playerSaver.GetComponent<TouchPlayerController>().enabled = lvlUI.LoadData();  
    }

    public void SaveLvlUI()
    {
        lvlUI.SaveData();
        Debug.Log("savelvlui"); 
        Debug.Log(transform);   
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
    }
}
