using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weap : MonoBehaviour
{
    private GameObject pl;
    public int damege;
    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }
    public void Hill()
    {
        AttackController AtComp = pl.GetComponent<AttackController>();
        if (AtComp)
        {
            AtComp.Other_Damege(damege);
        }
    }
}
