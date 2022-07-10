using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectail : MonoBehaviour
{
    private float _speed;
    private DamageInputController.DamageType _damageType;
    private float _damage;
    private float _fireTime/*if damageType -- fire*/;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (_damageType == DamageInputController.DamageType.rangeFire)
                player.GetComponent<DamageInputController>().TakeDamage(_damage, _fireTime);
            else
                player.GetComponent<DamageInputController>().TakeDamage(transform.position, _damage, _damageType);
        }
        if (!collision.gameObject.CompareTag("enemy"))
            Destroy(gameObject);
    }
    public void Values(float speed,float damage, DamageInputController.DamageType damageType,float fireTime)
    {
        _damage = damage;
        _speed = speed;
        _fireTime = fireTime;
        _damageType = damageType;
    }
}
