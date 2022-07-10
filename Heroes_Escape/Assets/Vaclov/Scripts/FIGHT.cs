using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FIGHT : MonoBehaviour
{
    private GameObject pl;
    public int p;
    public AudioSource audioData;
    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("enemy");
        //audioData = GetComponent<AudioSource>();
        
    }
    [Button]
    public void Damege()
    {
        HP HPComp = pl.GetComponent<HP>();
        if (HPComp)
        {
            //HPComp.GetDamege(p, false);
        }
        audioData.Play();
    }
    public void Get(int k)
    {
        p = k;
    }
}
