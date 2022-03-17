using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Character;
    public GameObject VisiblePlayer;
    public GameObject CharacterPhysics;

    public GameObject CharacterBody;
    public GameObject CharacterChest;
    public GameObject CharacterHat;
    public GameObject CharacterWeapon;

    private Animator CharacterBodyAnimator;
    private Animator CharacterChestAnimator;
    private Animator CharacterHatAnimator;
    private Animator CharacterWeaponAnimator;
    public int DirectionNumber;

    private Vector3 WatchLeft;
    private Vector3 WatchRight;
    private Transform VisiblePlayerTransform;
    private Transform CharacterTransform;

    public Vector3[] CharacterPositions = new Vector3[4];
    public int PositionNumber;

    public Vector3[] ShootPositions = new Vector3[4];
    public int ShootPosition;

    public HP CharacterHP;
    private bool WasDead = false;

    public void Start()
    {
        CharacterBodyAnimator = CharacterBody.GetComponent<Animator>();
        CharacterChestAnimator = CharacterChest.GetComponent<Animator>();
        CharacterHatAnimator = CharacterHat.GetComponent<Animator>();
        CharacterWeaponAnimator = CharacterWeapon.GetComponent<Animator>();
        WatchLeft = new Vector3(-1f, 1f, 1f);
        WatchRight = new Vector3(1f, 1f, 1f);
        VisiblePlayerTransform = VisiblePlayer.GetComponent<Transform>();
        CharacterTransform = Character.GetComponent<Transform>();
        CharacterPositions[0] = new Vector3(-0.25f, 0f, 0f);
        CharacterPositions[1] = new Vector3(-0.25f, 0.6f, 0f);
        CharacterPositions[2] = new Vector3(0.25f, 0.6f, 0f);
        CharacterPositions[3] = new Vector3(0.25f, 0f, 0f);
        CharacterPhysics.transform.localPosition = ShootPositions[0];
    }

    private void Update()
    {
        if(CharacterHP.Hp() <= 0f)
        {
            WasDead = true;
            SetDeathState();
        }
        else
        {
            if(WasDead == true)
            {
                WasDead = false;
                SetAliveState();
            }
        }
    }

    public void RotateLeftButton()
    {
        if (Player.transform.localEulerAngles.z >= -1f && Player.transform.localEulerAngles.z <= 1f)
        {
            ChangeDirection(3);
            CharacterTransform.localScale = WatchLeft;
        }
        if (Player.transform.localEulerAngles.z >= 89f && Player.transform.localEulerAngles.z <= 91f)
        {
            ChangeDirection(2);
            CharacterTransform.localScale = WatchRight;
        }
        if (Player.transform.localEulerAngles.z >= 179f && Player.transform.localEulerAngles.z <= 181f)
        {
            ChangeDirection(1);
        }
        if (Player.transform.localEulerAngles.z >= 269f && Player.transform.localEulerAngles.z <= 271f)
        {
            ChangeDirection(0);
        }
        PositionNumber--;
        if (PositionNumber == -1)
        {
            PositionNumber = 3;
        }
        if (PositionNumber == 0 || PositionNumber == 3)
        {
            BackOrderInLayer();
        }
        else
        {
            FrontOrderInLayer();
        }
        ChangePosition(PositionNumber);

        ShootPosition--;
        if (ShootPosition == -1)
        {
            ShootPosition = 3;
        }
        ChangeShootingPosition(ShootPosition);
    }
    public void RotateRightButton()
    {
        if (Player.transform.localEulerAngles.z >= -1f && Player.transform.localEulerAngles.z <= 1f)
        {
            ChangeDirection(1);
        }
        if (Player.transform.localEulerAngles.z >= 89f && Player.transform.localEulerAngles.z <= 91f)
        {
            ChangeDirection(0);
            CharacterTransform.localScale = WatchRight;
        }
        if (Player.transform.localEulerAngles.z >= 179f && Player.transform.localEulerAngles.z <= 181f)
        {
            ChangeDirection(3);
            CharacterTransform.localScale = WatchLeft;

        }
        if (Player.transform.localEulerAngles.z >= 269f && Player.transform.localEulerAngles.z <= 271f)
        {
            ChangeDirection(2);
        }
        PositionNumber++;
        if (PositionNumber == 4)
        {
            PositionNumber = 0;
        }
        if (PositionNumber == 0 || PositionNumber == 3)
        {
            BackOrderInLayer();
        }
        else
        {
            FrontOrderInLayer();
        }
        ChangePosition(PositionNumber);
        ShootPosition++;
        if (ShootPosition == 4)
        {
            ShootPosition = 0;
        }
        ChangeShootingPosition(ShootPosition);
    }

    private void ChangePosition(int pos)
    {
        CharacterTransform.localPosition = CharacterPositions[pos];
    }
    private void ChangeShootingPosition(int pos)
    {
        CharacterPhysics.transform.localPosition = ShootPositions[pos];
    }

    private void ChangeDirection(int a)
    {
        CharacterBodyAnimator.SetInteger("DirectionNumber", a);
        CharacterHatAnimator.SetInteger("DirectionNumber", a);
        CharacterChestAnimator.SetInteger("DirectionNumber", a);
        CharacterWeaponAnimator.SetInteger("DirectionNumber", a);
    }

    public void Move()
    {
        CharacterBodyAnimator.SetInteger("MoveNumber", 1);
        CharacterHatAnimator.SetInteger("MoveNumber", 1);
        CharacterChestAnimator.SetInteger("MoveNumber", 1);
        CharacterWeaponAnimator.SetInteger("MoveNumber", 1);
    }

    public void Attack()
    {
        CharacterBodyAnimator.SetBool("Attack", true);
        CharacterHatAnimator.SetBool("Attack", true);
        CharacterChestAnimator.SetBool("Attack", true);
        CharacterWeaponAnimator.SetBool("Attack", true);
    }

    public void FrontOrderInLayer()
    {
        CharacterBody.GetComponent<SpriteRenderer>().sortingOrder = 6;
        CharacterHat.GetComponent<SpriteRenderer>().sortingOrder = 17;
        CharacterChest.GetComponent<SpriteRenderer>().sortingOrder = 10;
        CharacterWeapon.GetComponent<SpriteRenderer>().sortingOrder = 21;
    }

    public void BackOrderInLayer()
    {
        CharacterBody.GetComponent<SpriteRenderer>().sortingOrder = 22;
        CharacterHat.GetComponent<SpriteRenderer>().sortingOrder = 24;
        CharacterChest.GetComponent<SpriteRenderer>().sortingOrder = 23;
        CharacterWeapon.GetComponent<SpriteRenderer>().sortingOrder = 25;
    }

    private void SetDeathState()
    {
            CharacterBodyAnimator.SetBool("Dead", true);
            CharacterChestAnimator.SetBool("Dead", true);
            CharacterHatAnimator.SetBool("Dead", true);
            CharacterWeaponAnimator.SetBool("Dead", true);
    }

    private void SetAliveState()
    {
        CharacterBodyAnimator.SetBool("Dead", false);
        CharacterChestAnimator.SetBool("Dead", false);
        CharacterHatAnimator.SetBool("Dead", false);
        CharacterWeaponAnimator.SetBool("Dead", false);
    }

}
