using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(TimerManager))]
public class AttackController : MonoBehaviour
{
    [SerializeField]
    private float defaultDamage;
    [SerializeField]
    private weaponType weaponType;
    [SerializeField]
    private float fireTime;
    [SerializeField]
    private float armorBreakTime;
    [SerializeField]
    private float attackDistance = 1f; //дистанция удара в юнитах
    [SerializeField]
    private GameObject projectail;
    [SerializeField]
    private float projectailSpeed;

    public float cooldown;
    [SerializeField]
    private Vector3 attackDirection = new Vector3(0, 1, 0); //направление удара в локальных! координатах
    [SerializeField]
    private string enemyTag = "enemy";
    [SerializeField]
    private WeaponItem activeWeapon;
    [SerializeField]
    private AudioClip miss, dam;

    private bool canAttack = true;
    private AudioSource AudS;
    private Timer _timer;
    private TimerManager _manager;
    private float damage = 0;
    private float damageMultiplier = 1;
    
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private RuntimeAnimatorController weaponAnimatorDefault;
    private void Start()
    {
        AudS = gameObject.GetComponent<AudioSource>();
        _manager = GetComponent<TimerManager>();
        _timer = new Timer(cooldown, false, TimerCompleted); //создание таймера
        _manager.RegisterTimer(_timer);

        damage = defaultDamage;
        if (activeWeapon != null)
        {
            damage += activeWeapon.damage;
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    private void TimerCompleted(Timer timer)
    {
        //Debug.Log("Completed!");
        canAttack = true;
    }
    
    [Button]
    public void RestartTimer()
    {
        _timer.Restart();  //запуск
    }
    
    public weaponType WeaponType()
    {
        return weaponType;
    }
    [Button]
    public void Attack()
    {
        if (canAttack && gameObject.GetComponent<HP>().Hp()>0)
        {
            if (weaponType == weaponType.Archer || weaponType == weaponType.Mage)
            {
                GameObject project = Instantiate(projectail, transform.position, transform.rotation);
                project.GetComponent<PlayerProjectail>().Values(projectailSpeed, damage*damageMultiplier, weaponType, fireTime);
                AudS.clip = dam;
                AudS.Play();
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(attackDirection), attackDistance); //бьем луч
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag(enemyTag)) //если коснулся врага
                    {
                        EnemyHp EnemyHP = hit.collider.GetComponent<EnemyHp>();
                        if (EnemyHP && gameObject.GetComponent<HP>().Hp() > 0)
                        {
                            EnemyHP.GetDamage(damage * damageMultiplier, weaponType, 0, armorBreakTime);
                            EnemyHP.gameObject.GetComponent<enemy>().Disquiet(true);
                        }
                        AudS.clip = dam;
                    }
                    else AudS.clip = miss;
                }
                else AudS.clip = miss;
                AudS.Play();
            }
            canAttack = false;
            _timer.Restart();

        }
    }

    public void SetActiveWeapon(WeaponItem weapon, float _damage, bool isAlreadyActive)
    {
        if (activeWeapon != null)
        {
            damage -= activeWeapon.damage;
            activeWeapon = null;
        }

        if (isAlreadyActive)
        {
            weaponAnimator.runtimeAnimatorController = weaponAnimatorDefault;
            return;
        }
        
        activeWeapon = weapon;
        activeWeapon.damage = _damage;
        damage += activeWeapon.damage;
        if (weapon != null)
        {
            weaponAnimator.runtimeAnimatorController = weapon.animator;
        }
        else
        {
            weaponAnimator.runtimeAnimatorController = weaponAnimatorDefault;
        }
    }
    public void Other_Damege(int od)
    {
        damage += od;
    }


    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }
    public void MultiplyDamageMultiplier(float multiplier)
    {
        damageMultiplier *= multiplier;
    }
    public void DivDamageMultiplier(float multiplier)
    {
        damageMultiplier /= multiplier;
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public float GetDamage()
    {
        return damage*damageMultiplier;
    }
}
