using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private List<Timer> _timers = new List<Timer>();

    public void RegisterTimer(Timer timer)
    {
        _timers.Add(timer);
    }

    public void PauseAllTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.Pause();
        }
    }

    public void ResumeAllTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.Resume();
        }
    }

    public void RestartAllTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.Restart();
        }
    }

    private void Update()
    {
        UpdateAllTimers();
    }

    private void UpdateAllTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.Update();
        }
    }
}