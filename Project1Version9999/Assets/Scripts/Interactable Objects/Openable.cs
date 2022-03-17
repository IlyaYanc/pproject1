using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Openable : MonoBehaviour
{
    protected int stage; //0-locked; 1-closed; 2-opened;

    public int GetStage()
    {
        return stage;
    }
    public virtual void SetStage(int _stage)
    {
        if (_stage >= 0 && _stage <= 2)
        {
            this.stage = _stage;
        }
        else this.stage = 0;
    }
}
