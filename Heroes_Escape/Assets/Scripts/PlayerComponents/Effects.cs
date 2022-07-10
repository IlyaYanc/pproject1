using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
   
    [SerializeField] private AudioSource adS;
    [SerializeField] private AudioClip walk;
    public void MoveEffects()
    {
        adS.clip = walk;
        adS.Play();
        StartCoroutine(Pl());
    }
    IEnumerator Pl()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }
        adS.Stop();
    }
}
