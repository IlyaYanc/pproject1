using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEff : MonoBehaviour
{
[SerializeField]
private AudioSource AudS;
   public void PlayEff()
{
AudS.Play();
}
}
