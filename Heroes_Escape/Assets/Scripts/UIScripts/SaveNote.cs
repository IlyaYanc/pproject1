using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNote : MonoBehaviour
{
    private Timer noteTimer = null;
    private TimerManager manager;

    private void Awake()
    {
        manager = gameObject.GetComponent<TimerManager>();
        noteTimer = new Timer(2f, false, TimerEnd);
        manager.RegisterTimer(noteTimer);
    }

    private void OnEnable()
    {
        if (noteTimer != null)
        {
            noteTimer.Restart();   
        }
    }

    private void TimerEnd(Timer timer)
    {
        gameObject.SetActive(false);
    }
}
