using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ST : MonoBehaviour
{
    public GameObject MG;
    [Button]
    public void STMG()
    {
        MG.SetActive(true);
    }
}
