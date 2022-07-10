using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotTrap : MonoBehaviour
{
    public GameObject _projectail;
    [SerializeField] private weaponType _dmgForEnemy;
    [SerializeField] private DamageInputController.DamageType _dmgForPlayer;
    [SerializeField] private float _fireTime;
    [SerializeField] private float _damage;
    [SerializeField] private float _sped;
    [SerializeField] private bool automaticShooting;
    private AudioSource AudS;
    [SerializeField] private float timerDuration;
    private Timer shotTimer;
    private bool timerActive;
    private bool trapActive;
    private TimerManager _manager;
    private GameObject arrow;

    private void Start()
    {
        arrow = new GameObject("arrow", typeof(shotTrapProjectail));
        AudS = GetComponent<AudioSource>();
        _manager = GetComponent<TimerManager>();
        shotTimer = new Timer(timerDuration, false, TimerComplete);
        _manager.RegisterTimer(shotTimer);
        //_projectail.GetComponent<shotTrapProjectail>().(_sped, _damage, _dmgForEnemy, _dmgForPlayer, _fireTime);
        //arrow = new shotTrapProjectail(_sped, _damage, _dmgForEnemy, _dmgForPlayer, _fireTime);
    }

    private void FixedUpdate()
    {
        if (trapActive)
        {
            if (automaticShooting)
            {
                if (!timerActive)
                {
                    Shoot();
                    timerActive = true;
                    shotTimer.Restart();
                }
            }
        }
    }

    public void ActivateTrap()
    {
        trapActive = true;
        if(!automaticShooting)
        {
            Shoot();
        }
        else
        {
            timerActive = false;
        }
    }

    public void DeactivateTrap()
    {
        trapActive = false;
        shotTimer.Pause();
    }
    
    private void TimerComplete(Timer timer)
    {
        timerActive = false;
    }
    public void Shoot()
    {
        //GameObject project = Instantiate(arrow, transform.position, transform.rotation);
        AudS.Play();
    }
    
}
