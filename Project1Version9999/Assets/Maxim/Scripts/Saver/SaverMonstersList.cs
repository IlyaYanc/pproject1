using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverMonstersList : MonoBehaviour
{
    [SerializeField]
    private SaverMonster[] saveList;

    public string[] jsonStr;

    public void UpdateValues()
    {
        jsonStr = new string[saveList.Length];
        for (int i = 0; i < saveList.Length; i++)
        {
            saveList[i].UpdateValues();
            jsonStr[i] = saveList[i].ReturnJsonString();
        }
    }
    public string[] ReturnJsonStrings()
    {
        return jsonStr;
    }
    public int ReturnListLength()
    {
        return saveList.Length;
    }

    public void LoadValues()
    {
        jsonStr = GetComponent<SaveStrings>().ReturnStrings2();
        for (int i = 0; i < saveList.Length; i++)
        {
            saveList[i].LoadValues(jsonStr[i]);
        }
    }
}
