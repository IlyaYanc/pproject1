using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightButton : MonoBehaviour
{
    public bool timerActive;
    public SliderTimer cooldownTimer;

    public void StartCooldownTimer()
    {
        timerActive = true;
        cooldownTimer.RestartTimer();
        gameObject.GetComponent<Button>().enabled = false;
    }
}
