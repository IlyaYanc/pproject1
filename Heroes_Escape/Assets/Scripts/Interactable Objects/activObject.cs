using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activObject : Interactable
{
    [SerializeField] protected int openValue;
    [SerializeField] protected bool multipleLevers = false;
    protected int value = 0;
    protected int stage = 0;

    public void ChangeValue(int _value)
    {
        if (stage != 2)
        {
            if ((value + _value) >= 0 && (value + _value) <= openValue)
            {
                value += _value;
                if (value == openValue)
                {
                    SetStage(1);
                   // Debug.Log("open");
                }
                else
                {
                    SetStage(0);
                   // Debug.Log("close");
                }
            }
        }
    }

    private void Start()
    {
        if (!multipleLevers)
            openValue = 1;
    }

    private bool Open()
    {
        if (stage == 1)
        {
            stage = 2;
            return true;
        }
        else
            return false;
    }
    private void Close()
    {
        stage = 0;
    }

    public virtual void SetStage(int _stage) //это для сохранений нужно
    {
        stage = _stage;
    }
    public virtual int GetStage() //это для сохранений нужно
    {
        return stage;
    }
    public override void Interact()
    {
        Open();
    }
    public virtual void LeverInteract(int _value = 0)
    {
        ChangeValue(_value);
    }
}

public class ActiveObjectSavingData
{
    public int stage;

    public ActiveObjectSavingData(int _stage)
    {
        stage = _stage;
    }
}
