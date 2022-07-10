using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Hill_Posion : MonoBehaviour
{
    private GameObject pl;
    
    
    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }
    [Button]
    public void Hill()
    {
        HP HPComp = pl.GetComponent<HP>();
        if (HPComp)
        {
            HPComp.GetHill(5);
            gameObject.GetComponent<Hill_S>().Out();
        }
        
    }
}
