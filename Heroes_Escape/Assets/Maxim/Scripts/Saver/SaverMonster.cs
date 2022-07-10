using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SavingSystem/SaverMonster")]
public class SaverMonster : MonoBehaviour
{
    public Vector3 position;
    public bool hasTarget;
    public void UpdateValues()
    {
        TestMonsterData obj = GetComponent<TestMonsterData>();
        if (obj)
        {
            position = obj.GetPosition();
            hasTarget = obj.GetHasTarget();
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
        TestMonsterData obj = GetComponent<TestMonsterData>();
        if (obj)
        {
            obj.SetPosition(position);
            obj.SetHasTarget(hasTarget);
        }
    }
}
