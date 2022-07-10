using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    void Awake()
    {
        _event.Invoke();
    }
}
