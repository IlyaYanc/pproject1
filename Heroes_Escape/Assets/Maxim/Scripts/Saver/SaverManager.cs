using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

[AddComponentMenu("SavingSystem/SaverManager")]
[RequireComponent(typeof(SaverObjectsList))]
[RequireComponent(typeof(SaverMonstersList))]
[RequireComponent(typeof(SaveStrings))]
public class SaverManager : MonoBehaviour
{
    [SerializeField]
    SaveStrings saveStrings;

    private string data;

    private void Start()
    {
        if (!saveStrings)
        {
            saveStrings = GetComponent<SaveStrings>();
        }

    }

    /*private void Update() //Input нажатий для вызова Сохранения/Загрузки (Переписать под андроид!!)
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Load();
        }
    }*/
    
    public void Save()
    {
        saveStrings.GetStrings();
        data = JsonUtility.ToJson(saveStrings, true);
        File.WriteAllText("D:/save.txt", data);
    }

    public void Load()
    {
        if (File.ReadAllText("D:/save.txt") != null && File.ReadAllText("D:/save.txt") != "")
        {
            data = File.ReadAllText("D:/save.txt");
            JsonUtility.FromJsonOverwrite(data, saveStrings);
            saveStrings.LoadData();
        }
    }
    public void Reset()
    {
        File.WriteAllText("D:/save.txt", null);
    }

}


