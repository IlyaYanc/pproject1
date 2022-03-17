using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GD : MonoBehaviour
{
    private GameObject pl;
    public AudioSource audioData;
    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }
    
    [Button]
    public void Damege()
    {
        HP HPComp = pl.GetComponent<HP>();
        if (HPComp)
        {
            //HPComp.GetDamege(1,false);
        }
        audioData.Play();
    }
}
