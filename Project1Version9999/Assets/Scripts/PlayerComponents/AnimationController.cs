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
    public Vector2[] ProjectailOffset = new Vector2[4];
    public int ShootPosition;

    public HP CharacterHP;
    private bool WasDead = false;
    [SerializeField]private string lastClipName;
    private AnimatorClipInfo[] clipsInfo;
    private RuntimeAnimatorController[] equipmentAnimatorControllers = new RuntimeAnimatorController[4];
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
        SetRuntimeAnimatorControllers();
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
            SetLastClipName("WL");
        }
        if (Player.transform.localEulerAngles.z >= 89f && Player.transform.localEulerAngles.z <= 91f)
        {
            ChangeDirection(2);
            CharacterTransform.localScale = WatchRight;
            SetLastClipName("WD");
        }
        if (Player.transform.localEulerAngles.z >= 179f && Player.transform.localEulerAngles.z <= 181f)
        {
            ChangeDirection(1);
            SetLastClipName("WR");
        }
        if (Player.transform.localEulerAngles.z >= 269f && Player.transform.localEulerAngles.z <= 271f)
        {
            ChangeDirection(0);
            SetLastClipName("WU");
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
            SetLastClipName("WR");
        }
        if (Player.transform.localEulerAngles.z >= 89f && Player.transform.localEulerAngles.z <= 91f)
        {
            ChangeDirection(0);
            SetLastClipName("WU");
            CharacterTransform.localScale = WatchRight;
        }
        if (Player.transform.localEulerAngles.z >= 179f && Player.transform.localEulerAngles.z <= 181f)
        {
            ChangeDirection(3);
            SetLastClipName("WL");
            CharacterTransform.localScale = WatchLeft;

        }
        if (Player.transform.localEulerAngles.z >= 269f && Player.transform.localEulerAngles.z <= 271f)
        {
            ChangeDirection(2);
            SetLastClipName("WD");
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
        if(CharacterPhysics.GetComponent<AttackController>().projectail != null)
        {
            CharacterPhysics.GetComponent<AttackController>().projectail.GetComponent<BoxCollider2D>().offset = ProjectailOffset[pos];
            if(pos == 2)
            {
                CharacterPhysics.GetComponent<AttackController>().projectail.GetComponent<SpriteRenderer>().sortingOrder = 31;
            }
            else
            {
                CharacterPhysics.GetComponent<AttackController>().projectail.GetComponent<SpriteRenderer>().sortingOrder = 25;
            }
        }

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
        CharacterBody.GetComponent<SpriteRenderer>().sortingOrder = 9;
        CharacterHat.GetComponent<SpriteRenderer>().sortingOrder = 20;
        CharacterChest.GetComponent<SpriteRenderer>().sortingOrder = 13;
        CharacterWeapon.GetComponent<SpriteRenderer>().sortingOrder = 24;
    }

    public void BackOrderInLayer()
    {
        CharacterBody.GetComponent<SpriteRenderer>().sortingOrder = 27;
        CharacterHat.GetComponent<SpriteRenderer>().sortingOrder = 29;
        CharacterChest.GetComponent<SpriteRenderer>().sortingOrder = 28;
        CharacterWeapon.GetComponent<SpriteRenderer>().sortingOrder = 30;
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

    public void ResetAnimationClip()
    {
        if(equipmentAnimatorControllers[0] != CharacterBodyAnimator.runtimeAnimatorController)
        {
            Time.timeScale = 1f;
            CharacterBodyAnimator.SetInteger("DirectionNumber", SetDirectionAfterChange());
            CharacterBodyAnimator.Play(lastClipName);
        }

        if (equipmentAnimatorControllers[1] != CharacterHatAnimator.runtimeAnimatorController)
        {
            Time.timeScale = 1f;
            CharacterHatAnimator.SetInteger("DirectionNumber", SetDirectionAfterChange());
            CharacterHatAnimator.Play(lastClipName);
        }

        if (equipmentAnimatorControllers[2] != CharacterChestAnimator.runtimeAnimatorController)
        {
            Time.timeScale = 1f;
            CharacterChestAnimator.SetInteger("DirectionNumber", SetDirectionAfterChange());
            CharacterChestAnimator.Play(lastClipName);
        }

        if (equipmentAnimatorControllers[3] != CharacterWeaponAnimator.runtimeAnimatorController)
        {
            Time.timeScale = 1f;
            CharacterWeaponAnimator.SetInteger("DirectionNumber", SetDirectionAfterChange());
            CharacterWeaponAnimator.Play(lastClipName);
        }
        
        SetRuntimeAnimatorControllers();
    }

    private void SetLastClipName(string name)
    {
        lastClipName = name;
    }

    private void SetRuntimeAnimatorControllers()
    {
        equipmentAnimatorControllers[0] = CharacterBodyAnimator.runtimeAnimatorController;
        equipmentAnimatorControllers[1] = CharacterHatAnimator.runtimeAnimatorController;
        equipmentAnimatorControllers[2] = CharacterChestAnimator.runtimeAnimatorController;
        equipmentAnimatorControllers[3] = CharacterWeaponAnimator.runtimeAnimatorController;
    }

    private int SetDirectionAfterChange()
    {
        if (lastClipName == "WU")
            return 0;
        if (lastClipName == "WR")
            return 1;
        if (lastClipName == "WD")
            return 2;
        if (lastClipName == "WL")
            return 3;

        return 0;
    }
}
