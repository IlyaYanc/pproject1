using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SavingSystem/SaverObject")]
public class SaverObject : MonoBehaviour
{
    [SerializeField]
    public int stage;
    public void UpdateValues()
    {
        //TestData obj = GetComponent<TestData>();
        activObject obj = GetComponent<activObject>();
        if (obj)
        {
            stage = obj.GetStage();
        }
    }
    public string ReturnJsonString()
    {
        /*string jsonStr;
        jsonStr = JsonUtility.ToJson(this, false);
        return jsonStr;*/
        return JsonUtility.ToJson(this, false);
    }
    public void LoadValues(string jsonStr)
    {
        JsonUtility.FromJsonOverwrite(jsonStr, this);
        SetLoadedValues();
    }
    public void SetLoadedValues()
    {
        //TestData obj = GetComponent<TestData>();
        activObject obj = GetComponent<activObject>();
        if (obj)
        {
            obj.SetStage(stage);
        }
    }
}
