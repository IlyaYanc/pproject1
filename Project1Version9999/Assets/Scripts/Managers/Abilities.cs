using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public enum AbilityType
{
    HpBoost,
    AtackBoost,
    DefenceBoost
}
[RequireComponent(typeof(TimerManager))]
public class Abilities : MonoBehaviour
{
    [SerializeField] private AttackController[] atckControllers;
    [SerializeField] private HP[] HpControllers;

    [SerializeField] private AbilityType type;

    [SerializeField] int boostCooldown = 90;
    [SerializeField] int boostLength = 30;


    //выбор персонажа, на которого применяются абилки
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private UnityEngine.UI.Button[] heroBtns;

    private bool boostIsReloading = false;

    [SerializeField] private float damageMultiplier = 1.3f;
    [SerializeField] private float hpBoost = 0.3f;
    [SerializeField] private float defenceBoost = 10f;

    private Timer boost_timer;
    private Timer boostCD_timer;
    private TimerManager _manager;

    private int heroNumber;

    //damage texts
    [SerializeField] private TextMeshProUGUI[] dmgTexts;


    private void Start()
    {
        //объявление таймеров
        _manager = GetComponent<TimerManager>();

        boost_timer = new Timer(boostLength, false, boostTimerCompleted);
        _manager.RegisterTimer(boost_timer);
        boostCD_timer = new Timer(boostCooldown, false, boostCDTimerCompleted);
        _manager.RegisterTimer(boostCD_timer);

    }
    private void boostCDTimerCompleted(Timer timer)
    {
        boostIsReloading = false;
    }
    private void boostTimerCompleted(Timer timer)
    {
        switch (type)
        {
            case AbilityType.AtackBoost:
                atckControllers[heroNumber].DivDamageMultiplier(damageMultiplier);
                dmgTexts[heroNumber].text = atckControllers[heroNumber].GetDamage().ToString();
                break;
            case AbilityType.HpBoost:
                break;
            case AbilityType.DefenceBoost:
                HpControllers[heroNumber].EncreaseResist(CalculateResist(defenceBoost));
                break;
        }
    }
    private void RestartBoostCDTimer()
    {
        boostCD_timer.Restart();
    }
    private void RestartBoostTimer()
    {
        boost_timer.Restart();
    }
    public void BoostInvoke()
    {
        for (int i = 0; i < heroBtns.Length; i++)
        {
            heroBtns[i].onClick.RemoveAllListeners();
            int j = i;
            switch (type)
            {
                case AbilityType.AtackBoost:
                    heroBtns[i].onClick.AddListener(() => DamageBoost(j));
                    break;
                case AbilityType.HpBoost:
                    heroBtns[i].onClick.AddListener(() => HpBoost(j));
                    break;
                case AbilityType.DefenceBoost:
                    heroBtns[i].onClick.AddListener(() => DefenceBoost(j));
                    break;
            }

        }
        heroPanel.SetActive(true);
    }
    private void DamageBoost(int _heroNumber)
    {
        if (boostIsReloading)
            return;
        heroNumber = _heroNumber;
        atckControllers[_heroNumber].MultiplyDamageMultiplier(damageMultiplier);
        RestartBoostTimer();
        RestartBoostCDTimer();
        boostIsReloading = true;
        heroPanel.SetActive(false);
        dmgTexts[_heroNumber].text = atckControllers[_heroNumber].GetDamage().ToString();
    }
    private void HpBoost(int _heroNumber)
    {
        if (boostIsReloading)
            return;
        HpControllers[_heroNumber].GetHill(HpControllers[_heroNumber].GetMaxHp() * hpBoost);
        Debug.Log("healing" + HpControllers[_heroNumber]);
        RestartBoostTimer();
        RestartBoostCDTimer();
        boostIsReloading = true;
        heroPanel.SetActive(false);
    }

    private void DefenceBoost(int _heroNumber)
    {
        if (boostIsReloading)
            return;
        heroNumber = _heroNumber;
        HpControllers[_heroNumber].DecreaseResist(CalculateResist(defenceBoost));
        Debug.Log("giving defence to " + HpControllers[_heroNumber]);
        RestartBoostTimer();
        RestartBoostCDTimer();
        boostIsReloading = true;
        heroPanel.SetActive(false);
    }

    public float GetCoolDown()
    {
        return boostCooldown;
    }

    private float CalculateResist(float _defence)
    {
        return (_defence / 100f) * 0.3f;
    }

}
