using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectail : MonoBehaviour
{
    private weaponType _type;
    private float _fireTime;
    private float _damage;
    private float _speed;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = _transform.position + transform.up * _speed * Time.deltaTime;
    transform.position = newPos;
    }
    public void Values(float speed, float damage, weaponType weaponType, float fireTime)
    {
        _type = weaponType;
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
            hp.GetDamage(_damage, _type, _fireTime, 0);
            Destroy(gameObject);
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("TrainingTrigger"))
        {
            //Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
}
