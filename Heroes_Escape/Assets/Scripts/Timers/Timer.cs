using System;
using UnityEngine;

public class Timer
{
    public float Duration { get; private set; }
    public bool IsLooped { get; private set; }

    public bool IsCompleted { get; private set; }
    public bool IsPaused { get; private set; }

    public float TimeElapsed { get; private set; }
    public float StartTime { get; private set; }

    public event Action<Timer> OnComplete;
    public event Action<Timer> OnUpdate;

    public float WorldTime => Time.realtimeSinceStartup;
    public float TimeDelta => WorldTime - _lastUpdateTime;
    public float TimeRemaining => Duration - TimeElapsed;
    public float RatioComplete => TimeElapsed / Duration;
    public float RatioRemaining => TimeRemaining / Duration;

    private float _lastUpdateTime;

    public Timer(float duration, bool isLooped = false, Action<Timer> onComplete = null, Action<Timer> onUpdate = null)
    {
        this.Duration = duration;
        this.OnComplete = onComplete;
        this.OnUpdate = onUpdate;
        this.IsLooped = isLooped;

        StartTime = WorldTime;
        _lastUpdateTime = StartTime;
        IsPaused = true;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
        _lastUpdateTime = WorldTime;
    }

    public void Restart()
    {
        IsCompleted = false;
        IsPaused = false;
        _lastUpdateTime = WorldTime;
        StartTime = WorldTime;
        TimeElapsed = 0;
    }

    public void Update()
    {
        if (IsCompleted)
        {
            return;
        }

        if (!IsPaused)
        {
            if (OnUpdate != null)
            {
                OnUpdate(this);
            }

            TimeElapsed += TimeDelta;

            if (TimeElapsed > Duration)
            {
                if (OnComplete != null)
                {
                    OnComplete(this);
                }

                if (IsLooped)
                {
                    StartTime = WorldTime;
                    TimeElapsed = 0;
                }
                else
                {
                    IsCompleted = true;
                }
            }
        }
        _lastUpdateTime = WorldTime;
    }
}