using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationSetter : MonoBehaviour
{
    public GameObject Body;
    public GameObject Hat;
    public GameObject Chest;
    public GameObject Weapon;

    private Animator BodyAnimatory;
    private Animator HatAnimator;
    private Animator ChestAnimator;
    private Animator WeaponAnimator;

    public void Start()
    {
        BodyAnimatory = Body.GetComponent<Animator>();
        HatAnimator = Hat.GetComponent<Animator>();
        ChestAnimator = Chest.GetComponent<Animator>();
        WeaponAnimator = Weapon.GetComponent<Animator>();
    }
    public void SetIdle()
    {
        BodyAnimatory.SetInteger("MoveNumber", 0);
        BodyAnimatory.SetBool("Attack", false);
        HatAnimator.SetInteger("MoveNumber", 0);
        HatAnimator.SetBool("Attack", false);
        ChestAnimator.SetInteger("MoveNumber", 0);
        ChestAnimator.SetBool("Attack", false);
        WeaponAnimator.SetInteger("MoveNumber", 0);
        WeaponAnimator.SetBool("Attack", false);
    }
    
}
