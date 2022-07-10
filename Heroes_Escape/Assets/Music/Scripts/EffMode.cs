using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffMode : MonoBehaviour
{
    [SerializeField]
    private AudioClip AtCLIP, NorCLIP;
    private AudioSource ADS;
    void Start()
    {
        ADS = gameObject.GetComponent<AudioSource>();
    }
    public void AtMODE()
    {
        ADS.clip = AtCLIP;
        ADS.Play();
    }
    public void NorMODE()
    {
        ADS.clip = NorCLIP;
        ADS.Play();
    }
}
