using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[RequireComponent(typeof(TimerManager), typeof(Slider))]
public class SliderTimer : MonoBehaviour
{
    // ��� �������� �������� ��� �������� �� ������
    public float m_duration; //�����
    public Timer _timer;
    private TimerManager _manager;
    private Slider _slider;

    [SerializeField] private GameObject AttackButton; //sorry Vlad

    [SerializeField] private AttackController _attackController;
    [SerializeField] private Abilities _abilities;
    private void Start()
    {
        _manager = GetComponent<TimerManager>();
        _slider = GetComponent<Slider>();
        if(_attackController != null)
            m_duration = _attackController.GetCooldown();
        else if (_abilities != null)
            m_duration = _abilities.GetCoolDown();
            
        _timer = new Timer(m_duration, false, TimerCompleted, UpdateTimer); //�������� �������
        _manager.RegisterTimer(_timer);
    }

    public void UpdateTimer(Timer timer)
    {
        _slider.value = timer.RatioComplete;
    }

    public void TimerCompleted(Timer timer)
    {
        //Debug.Log("Completed!");
        if(AttackButton != null)
        {
            AttackButton.GetComponent<Button>().enabled = true;
            AttackButton.GetComponent<FightButton>().timerActive = false;
        }
        _slider.value = 1f;
    }

    [Button]
    public void RestartTimer()
    {
        _timer.Restart();  //������
    }
}
