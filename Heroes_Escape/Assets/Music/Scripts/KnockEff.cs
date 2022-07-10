using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockEff : MonoBehaviour
{
    private AudioSource ADS;
    void Start()
    {
        ADS = GetComponent<AudioSource>();
    }
    public void PlayS()
    {
        ADS.Play();
    }
}
