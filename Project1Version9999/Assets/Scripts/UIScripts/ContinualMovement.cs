using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContinualMovement : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    private bool buttonHeld;

    private void FixedUpdate()
    {
        if (buttonHeld)
        {
            _event.Invoke();
        }
    }

    public void PointerUp()
    {
        buttonHeld = false;
    }

    public void PointerDown()
    {
        buttonHeld = true;
    }
    
    
    
    
    
}
