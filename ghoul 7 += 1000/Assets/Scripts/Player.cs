using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] GameObject cubeick;
    [SerializeField] int speed;
    Rigidbody rb;
    bool right = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        speed = speed + 1;
        if (Input.GetButtonDown("Fire1"))
        {
            right = !right;
        }
        if (right)
        {
            rb.velocity = new Vector3(0f, 0f, speed);
        }
        else
        {
            rb.velocity = new Vector3(speed, 0f, 0f);
        }
    }
}
