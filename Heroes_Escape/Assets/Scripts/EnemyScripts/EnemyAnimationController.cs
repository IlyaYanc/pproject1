using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{

    // 1 - up, 2 - right, 3 - down, 4 - left 
    private int rotDirection;

    private bool isWalking;


    private enemy enemy;
    private Animator anim;
    private EnemyHp hps;
    private enemy enemycomp;
    [SerializeField]private GameObject VisibleEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<enemy>();
        anim = VisibleEnemy.GetComponent<Animator>();
        hps = gameObject.GetComponent<EnemyHp>();
        enemycomp = gameObject.GetComponent<enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (new Vector3(Mathf.Round(gameObject.transform.up.normalized.x), Mathf.Round(gameObject.transform.up.normalized.y), 0) == Vector3.up)
        {
            rotDirection = 1;
        }
        else if (new Vector3(Mathf.Round(gameObject.transform.up.normalized.x), Mathf.Round(gameObject.transform.up.normalized.y), 0) == Vector3.right)
        {
            rotDirection = 2;
        }
        else if (new Vector3(Mathf.Round(gameObject.transform.up.normalized.x), Mathf.Round(gameObject.transform.up.normalized.y), 0) == -1 * Vector3.up)
        {
            rotDirection = 3;
        }
        else
        {
            rotDirection = 4;
        }
        if(enemycomp.isAttacking &&  enemycomp.attackTimer > 0f)
        {
            anim.SetBool("IsAttacking", true);
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }

        isWalking = enemy.isWalking();
        if(anim.GetBool("IsAttacking") == false)
        {
            anim.SetBool("isWalking", isWalking);
        }

        anim.SetInteger("rotDirection", rotDirection);
        if(hps.ReturnHP()<=0f)
        {
            anim.SetBool("IsDead", true);
            VisibleEnemy.GetComponent<VisiblePlayer>().enabled = false;
            Destroy(gameObject, 2);
        }

    }
}
