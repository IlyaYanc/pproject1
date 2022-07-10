using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudioSourceController : MonoBehaviour
{
    [SerializeField] private AudioSource[] PlayerAudioSource = new AudioSource[4];
    [SerializeField] private AudioSource[] EnemyAudioSource = new AudioSource[0];
    [SerializeField] private AudioSource[] TrapsAudioSource = new AudioSource[0];
    [SerializeField] private AudioSource[] FireplaceAudioSource = new AudioSource[0];

    public void DisableAudioSources()
    {
        for(int i = 0; i < PlayerAudioSource.Length; i++)
        {
            if (PlayerAudioSource[i] != null)
                PlayerAudioSource[i].enabled = false;
        }

        for (int i = 0; i < EnemyAudioSource.Length; i++)
        {
            if(EnemyAudioSource[i] != null)
                EnemyAudioSource[i].enabled = false;

        }

        for (int i = 0; i < TrapsAudioSource.Length; i++)
        {
            if (TrapsAudioSource[i] != null)
                TrapsAudioSource[i].enabled = false;
        }

        for (int i = 0; i < FireplaceAudioSource.Length; i++)
        {
            FireplaceAudioSource[i].enabled = false;
        }
    }

    public void EnableAudioSource()
    {
        for (int i = 0; i < PlayerAudioSource.Length; i++)
        {
            if (PlayerAudioSource[i] != null)
                PlayerAudioSource[i].enabled = true;
        }

        for (int i = 0; i < EnemyAudioSource.Length; i++)
        {
            if (EnemyAudioSource[i] != null)
                EnemyAudioSource[i].enabled = true;
        }

        for (int i = 0; i < TrapsAudioSource.Length; i++)
        {
            if (TrapsAudioSource[i] != null)
                TrapsAudioSource[i].enabled = true;
        }

        for (int i = 0; i < FireplaceAudioSource.Length; i++)
        {
            FireplaceAudioSource[i].enabled = true;
        }
    }
}
