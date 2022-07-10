using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterData : MonoBehaviour
{
    public Vector3 position;
    public bool hasTarget;
    public Vector3 GetPosition()
    {
        return position;
    }

    public bool GetHasTarget()
    {
        return hasTarget;
    }

    public void SetPosition(Vector3 _position)
    {
        position = _position;
    }

    public void SetHasTarget(bool _hasTarget)
    {
        hasTarget = _hasTarget;
    }
}
