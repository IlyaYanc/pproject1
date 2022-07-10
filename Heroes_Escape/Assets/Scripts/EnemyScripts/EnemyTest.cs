using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] float hp = 10;
    public void GetDamage(float _damage)
    {
        hp -= _damage;
    }
}
