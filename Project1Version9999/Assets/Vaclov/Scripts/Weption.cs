using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Weption : MonoBehaviour
{
    public int punch;
    [Button]
    public void Gety()
    {
        FIGHT GDComp = GameObject.FindGameObjectWithTag("Player").GetComponent<FIGHT>();
        if (GDComp)
        {
            GDComp.Get(punch);
        }
    }
}
