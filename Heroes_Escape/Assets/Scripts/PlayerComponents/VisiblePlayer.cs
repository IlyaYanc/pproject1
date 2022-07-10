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
        if(ObjectTransform != null)
        {
            ObjectTransform.position = PlayerTransform.position;
        }
        else
        {
            gameObject.GetComponent<VisiblePlayer>().enabled = false;
        }
        
    }

}
