using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Tilemaps;

public class enemy : enemyWalk
{
    private float walkTimer;
    public float attackTimer;
    private Queue<Vector3> q = new Queue<Vector3>();
    Vector3 pos;
    Vector3 rot;
    private bool doConsole;
    [SerializeField]
    private float disAgrDist;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float walkCd;
    [SerializeField]
    private int maxEscapeCount;
    [SerializeField]
    private float attackCd;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackDist;
    [SerializeField]
    private float escapeDist;
    [SerializeField]
    private int damage;
    [SerializeField]
    private DamageInputController.DamageType damageType;
    [SerializeField]
    private float fireTime;
    [SerializeField]
    private GameObject projectail;
    [SerializeField]
    private float projectailSpeed;
    [SerializeField]
    private List<Vector3> patrWay;

    private bool neTupit = true;
    public bool isAttacking = false;
    private bool attackOnEscapeDist = false;
    private DamageInputController playerHp;
    private int escapeCount = 0;
    private bool doingEscape = false;
    void Start()
    {
        for (int i = 0; i < patrWay.Count; i++)
        {
            q.Enqueue(patrWay[i]);
        }
        if (q.Count == 0)
        {
            q.Enqueue(transform.position);
        }
        walkTimer = walkCd;
        pos = transform.position;
        rot = new Vector3(0, 0, 0);
        speed = 1 / speed;
        rotSpeed = 90 / rotSpeed;
        attackTimer = attackTime;
        playerHp = player.GetComponent<DamageInputController>();
    }
    public void CanAttack(bool state)
    {
        neTupit = state;
    }
    void Update()
    {
        if(map.GetTile(map.WorldToCell(gameObject.transform.position))!=ground){
            Destroy(gameObject.transform.parent.gameObject);
        }
        if(neTupit==false)
        {
            isAttacking = false;
        }
        //Vector3 dir = player.transform.position - transform.position;
        //float angDir = Mathf.Round(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg / 90) * 90;
        //if (doConsole)
        //    Debug.Log(angDir);
        if (isAttacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {

                Shoot(attackDist);
                attackTimer = attackTime;
            }
        }
        else
        {
            attackTimer = attackTime;
        }
        if(neTupit)
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        if (rot != new Vector3(0, 0, 0))
        {
            float firstrotAngel = Mathf.Atan2(rot.y, rot.x) - Mathf.Atan2(transform.up.y, transform.up.x);
            /*-1*Vector3.SignedAngle(rot, transform.up,Vector3.forward);*/
            float newRot = Mathf.Atan2(transform.up.normalized.y, transform.up.normalized.x) * Mathf.Rad2Deg - Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            if (newRot > 180)
            {
                newRot -= 360;
            }
            else if (newRot < -180)
            {
                newRot += 360;
            }

            if (Mathf.Abs(newRot) > rotSpeed * Time.deltaTime)
            {
                if (newRot < 0)
                {
                    transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -1 * rotSpeed * Time.deltaTime));
                }
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -1 * newRot));
            }
        }
        if (walkTimer <= 0)
        {
            Vector3 u = player.transform.position - transform.position;
            bool canAttackOnDist = false;
            Vector3 target = transform.position;
            bool doRotation = true;
            if (hasDisquiet)
            {
                if (!doingEscape && Vector3.Distance(transform.position, player.transform.position) <=escapeDist / 2 && Vector3.Distance(transform.position, SavePosition(transform.position,player.transform.position,escapeDist))>=0.1)
                    doingEscape = true;
                if (transform.position == player.transform.position)
                {
                    if (InPlayer(transform.position, transform.up) == transform.position)
                    {
                        canAttackOnDist = true;
                        target = transform.position;
                    }
                    else
                    {
                        canAttackOnDist = false;
                        target = InPlayer(transform.position, transform.up);
                    }
                }
                else if (Vector3.Distance(transform.position, player.transform.position) <= attackDist && Vector3.Distance(transform.position, player.transform.position) > escapeDist)
                {
                    canAttackOnDist = true;
                    target = player.transform.position;
                }
                else if (Vector3.Distance(transform.position, player.transform.position) > attackDist)
                {
                    canAttackOnDist = false;
                    target = player.transform.position;
                }
                else
                {
                    if (escapeCount < maxEscapeCount && doingEscape && Vector3.Distance(player.transform.position, transform.position) < escapeDist)
                    {
                        escapeCount++;
                        canAttackOnDist = false;
                        target = SavePosition(transform.position, player.transform.position, escapeDist);
                        doRotation = false;
                    }
                    else
                    {
                        doingEscape = false;
                        canAttackOnDist = true;
                        target = player.transform.position;
                    }
                }
            }
            if (!(new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) == new Vector3(Mathf.Round(u.normalized.x), Mathf.Round(u.normalized.y), 0) && hasDisquiet && canAttackOnDist && (transform.position.x == player.transform.position.x || transform.position.y == player.transform.position.y)))
            {
                isAttacking = false;
                Vector3 newPos;
                if (hasDisquiet)
                {
                    newPos = ShortWay(transform.position, target);
                }
                else if (transform.position != q.Peek())
                {
                    newPos = ShortWay(transform.position, q.Peek());
                }
                else
                {
                    newPos = ShortWay(transform.position, q.Dequeue());
                    q.Enqueue(newPos);
                }
                if (doRotation)
                {
                    Vector3 newRot = newPos - transform.position;
                    if (newRot == new Vector3(-1, 0, 0) && new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) == new Vector3(1, 0, 0))
                    {
                        walkTimer = walkCd;
                        rot = new Vector3(0, 1, 0);
                    }
                    else if (newRot == new Vector3(0, 1, 0) && new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) == new Vector3(0, -1, 0))
                    {
                        walkTimer = walkCd;
                        rot = new Vector3(1, 0, 0);
                    }
                    else if (newRot == new Vector3(1, 0, 0) && new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) == new Vector3(-1, 0, 0))
                    {
                        walkTimer = walkCd;
                        rot = new Vector3(0, -1, 0);
                    }
                    else if (newRot == new Vector3(0, -1, 0) && new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) == new Vector3(0, 1, 0))
                    {
                        walkTimer = walkCd;
                        rot = new Vector3(-1, 0, 0);
                    }
                    else if (new Vector3(Mathf.Round(transform.up.normalized.x), Mathf.Round(transform.up.normalized.y), 0) != newRot)
                    {
                        walkTimer = walkCd;
                        rot = newRot;
                    }
                    else
                    {
                        pos = newPos;
                        walkTimer = walkCd;
                    }
                }
                else
                {
                    pos = newPos;
                    walkTimer = walkCd;
                }
            }
            else
            {
                if(neTupit)
                isAttacking = true;
                walkTimer = walkCd/(1.5f);
            }
        }
        else
        {
            walkTimer -= Time.deltaTime;
            ////////
        }

        if (!hasDisquiet)
        {
            hasDisquiet = seing();
        }
        if (hasDisquiet && Vector3.Distance(transform.position, player.transform.position) > disAgrDist)
        {
            hasDisquiet = false;
        }
    }

    [Button]
    private void agr()
    {
        hasDisquiet = !hasDisquiet;

    }

    public void Disquiet(bool isActive)
    {
        hasDisquiet = isActive;
    }

    public void Nondisquiet()
    {
        hasDisquiet = false;
    }
    //public bool IsWalking() => walkTimar > 0f;
    public bool isWalking()
    {
        if (walkTimer >= walkCd-1/speed && neTupit)
        {
            return true;
        }
        else
        {
            return false;
        }
        //return (pos == transform.position && walkTimar > 0f);
    }
    private void Shoot(float dist)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, transform.up, attackDist);
        GameObject newProjectail;
        if (hit)
        {
            if (hit.collider.gameObject == player)
            {
                switch (damageType)
                {
                    case DamageInputController.DamageType.melee:
                        playerHp.TakeDamage(transform.position, damage, DamageInputController.DamageType.melee);
                        break;
                    case DamageInputController.DamageType.range:
                        newProjectail = Instantiate(projectail, transform.position, transform.rotation);
                        newProjectail.GetComponent<Projectail>().Values(projectailSpeed, damage, DamageInputController.DamageType.range, fireTime);
                        break;
                    case DamageInputController.DamageType.meleeSplash:
                        playerHp.TakeDamage(transform.position, damage, DamageInputController.DamageType.meleeSplash);
                        break;
                    case DamageInputController.DamageType.rangeSplash:
                        newProjectail = Instantiate(projectail, transform.position, transform.rotation);
                        newProjectail.GetComponent<Projectail>().Values(projectailSpeed, damage, DamageInputController.DamageType.rangeSplash, fireTime);
                        break;
                    case DamageInputController.DamageType.magic:
                        newProjectail = Instantiate(projectail, transform.position, transform.rotation);
                        newProjectail.GetComponent<Projectail>().Values(projectailSpeed, damage, DamageInputController.DamageType.magic, fireTime);
                        break;
                    case DamageInputController.DamageType.fire:
                        playerHp.TakeDamage(damage, fireTime);
                        break;
                    case DamageInputController.DamageType.rangeFire:
                        newProjectail = Instantiate(projectail, transform.position, transform.rotation);
                        newProjectail.GetComponent<Projectail>().Values(projectailSpeed, damage, DamageInputController.DamageType.rangeFire, fireTime);
                        break;
                }
            }
        }
    }
}
