using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStrings : MonoBehaviour
{
    public string[] jsonStr;
    public string[] jsonStr2;
    public void GetStrings()
    {
        GetComponent<SaverObjectsList>().UpdateValues();
        GetComponent<SaverMonstersList>().UpdateValues();
        jsonStr = new string[GetComponent<SaverObjectsList>().ReturnListLength()];
        jsonStr = GetComponent<SaverObjectsList>().ReturnJsonStrings();
        jsonStr2 = new string[GetComponent<SaverMonstersList>().ReturnListLength()];
        jsonStr2 = GetComponent<SaverMonstersList>().ReturnJsonStrings();
    }
    public string[] ReturnStrings()
    {
        return jsonStr;
    }
    public string[] ReturnStrings2()
    {
        return jsonStr2;
    }

    public void LoadData()
    {
        GetComponent<SaverObjectsList>().LoadValues();
        GetComponent<SaverMonstersList>().LoadValues();
    }
}
