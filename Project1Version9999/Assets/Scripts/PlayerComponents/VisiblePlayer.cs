using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiblePlayer : MonoBehaviour
{
    public GameObject Player;
    private Transform ObjectTransform;
    private Transform PlayerTransform;

    public void Start()
    {
        ObjectTransform = GetComponent<Transform>();
        PlayerTransform = Player.GetComponent<Transform>();
    }
    public void FixedUpdate()
    {
        ObjectTransform.position = PlayerTransform.position;
    }

}
