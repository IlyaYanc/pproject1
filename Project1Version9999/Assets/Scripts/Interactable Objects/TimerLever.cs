using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(TimerManager))]
public class TimerLever : Lever
{
    [SerializeField] private float m_duration; //время
    private Timer _timer;
    private TimerManager _manager;
    private AudioSource AudS;
    private void Start()
    {
        AudS = gameObject.GetComponent<AudioSource>();
        sRenderer = GetComponent<SpriteRenderer>();
        _manager = GetComponent<TimerManager>();


        _timer = new Timer(m_duration, false, TimerCompleted); //создание таймера
        _manager.RegisterTimer(_timer);

        sRenderer.sprite = sprites[0];
    }

    private void TimerCompleted(Timer timer)
    {
        Debug.Log("Completed!");
        obj.LeverInteract(-1);
        hasInteracted = false;
        sRenderer.sprite = sprites[0];
        AudS.Play();
    }

    [Button]
    public void RestartTimer()
    {
        _timer.Restart();  //запуск
    }

    public override void Interact()
    {
        /*obj.LeverInteract(1);
        hasInteracted = true;
        RestartTimer();*/
        if (!hasInteracted)
        {
            obj.LeverInteract(1);
            hasInteracted = true;
            obj.LeverInteract(1);
            hasInteracted = true;
            sRenderer.sprite = sprites[1];
            AudS.Play();
        }
        RestartTimer();
        sRenderer.sprite = sprites[1];
    }

    public override void LoadData(LeverSavingData data)
    {
    }









    /*[SerializeField]
    protected GameObject activatedObject;

    [SerializeField]
    protected GameObject player;

    [SerializeField] 
    private float waitTime = 5f;

    private activObject scr;
    private float timer;
    private bool timerIsRun = false;
    // Start is called before the first frame update
    void Start()
    {
        scr = activatedObject.GetComponent<activObject>();
    }

    (player.transform.position.x < transform.position.x + 0.2 || player.transform.position.x < transform.position.x - 0.2) &&
       (player.transform.position.y < transform.position.y + 0.2 || player.transform.position.y < transform.position.y - 0.2)
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == transform.position && Input.GetKeyDown(KeyCode.E))
        {
            if (timerIsRun == false)
            {
                timerIsRun = true;
                scr.ChangeValue(1);
            }
            timer = waitTime;
            // Debug.Log("timer is Start!");
        }
        else if (timerIsRun)
        {
            timer -= Time.deltaTime;
        }

        {
            scr.ChangeValue(-1);
            timerIsRun = false;
            //Debug.Log("time is out!");
        }
    }*/




}
