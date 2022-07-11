using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotTrapProjectail : MonoBehaviour
{
    private weaponType _dmgForEnemy;
    private DamageInputController.DamageType _dmgForPlayer;
    private float _fireTime;
    private float _damage;
    private float _speed;
    private Transform _transform;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = _transform.position + transform.up * _speed * Time.deltaTime;
        transform.position = newPos;
    }
    public void shotTrapProjectailParameters(float speed, float damage, weaponType weaponType, DamageInputController.DamageType damageType, float fireTime)
    {
        _dmgForEnemy = weaponType;
        _dmgForPlayer = damageType;
        _fireTime = fireTime;
        _damage = damage;
        _speed = speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            EnemyHp hp = collision.gameObject.GetComponent<EnemyHp>();
            hp.gameObject.GetComponent<enemy>().Disquiet(true);
            hp.GetDamage(_damage, _dmgForEnemy, _fireTime, 0);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_dmgForPlayer == DamageInputController.DamageType.rangeFire)
                player.GetComponent<DamageInputController>().TakeDamage(_damage, _fireTime);
            else
                player.GetComponent<DamageInputController>().TakeDamage(transform.position, _damage, _dmgForPlayer);
            Destroy(gameObject);
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("TrainingTrigger") && !collision.gameObject.CompareTag("enemy"))
        {
            //Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
}
