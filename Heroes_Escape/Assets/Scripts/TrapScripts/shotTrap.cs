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
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    private SpriteRenderer spriteRenderer;
    private Timer shotTimer;
    private bool timerActive;
    private bool trapActive;
    private TimerManager _manager;
    private GameObject arrow;
    private float xOffset;
    private float yOffset;

    private void Start()
    {
        if(transform.localRotation.eulerAngles.z == 90)
        {
            xOffset = -0.65f;
            yOffset = 0f;
        }

        if (transform.localRotation.eulerAngles.z == 0)
        {
            xOffset = 0;
            yOffset = 0.65f;
        }

        if (transform.localRotation.eulerAngles.z == 270)
        {
            xOffset = 0.65f;
            yOffset = 0f;
        }

        //arrow = new GameObject("arrow", typeof(shotTrapProjectail));
        AudS = GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        spriteRenderer.sprite = activeSprite;
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
        spriteRenderer.sprite = inactiveSprite;
        shotTimer.Pause();
    }
    
    private void TimerComplete(Timer timer)
    {
        timerActive = false;
    }
    public void Shoot()
    {
        GameObject project = Instantiate(_projectail, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z), transform.rotation);
        project.GetComponent<shotTrapProjectail>().shotTrapProjectailParameters(_sped, _damage, _dmgForEnemy, _dmgForPlayer, _fireTime);
        AudS.Play();
    }
    
}
