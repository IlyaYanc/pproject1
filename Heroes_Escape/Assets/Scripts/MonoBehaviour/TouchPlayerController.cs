using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchPlayerController : MonoBehaviour
{
    [SerializeField] private UnityEvent leftMove;
    [SerializeField] private UnityEvent rightMove;
    [SerializeField] private UnityEvent topMove;
    [SerializeField] private UnityEvent downMove;
    [SerializeField] private UnityEvent leftRotate;
    [SerializeField] private UnityEvent rightRotate;
    
    [SerializeField] private GameObject KnightGraphics;
    [SerializeField] private GameObject ThiefGraphics;
    [SerializeField] private GameObject ArcherGraphics;
    [SerializeField] private GameObject MageGraphics;

    private float RotationCD;
    private float WalkCD;
    private AnimationController KnightGraphicsAnimationController;
    private AnimationController ThiefGraphicsAnimationController;
    private AnimationController ArcherGraphicsAnimationController;
    private AnimationController MageGraphicsAnimationController;
    [SerializeField] private GameObject[] FightButtons = new GameObject[0];

    public Timer WalkCDTimer;
    public Timer RotateCDTimer;
    public Timer touchHoldTimer;
    public TimerManager _manager;

    private bool swipeHold = false;
    

    public enum Direction
    {
        Top,
        Down,
        Left,
        Right
    }

    private Main playerInput;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Camera mainCamera;

    private void Awake()
    {
        playerInput = new Main();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        playerInput.PlayerMoving.Swipe.started += (context) => OnSwipeStart();
        playerInput.PlayerMoving.Swipe.performed += (context) => OnSwipeEnd();
        playerInput.PlayerMoving.Tap.performed += (context) => OnTap();
        foreach(var control in playerInput.controlSchemes)
        {
            //Debug.Log(control.name);
        }
        KnightGraphicsAnimationController = KnightGraphics.GetComponent<AnimationController>();
        ThiefGraphicsAnimationController = ThiefGraphics.GetComponent<AnimationController>();
        ArcherGraphicsAnimationController = ArcherGraphics.GetComponent<AnimationController>();
        MageGraphicsAnimationController = MageGraphics.GetComponent<AnimationController>();
        RotationCD = gameObject.GetComponent<PlayerMoveController>().m_rotateDelay;
        WalkCD = gameObject.GetComponent<PlayerMoveController>().m_moveDelay;
        _manager = gameObject.GetComponent<TimerManager>();
        WalkCDTimer = new Timer(WalkCD, false, EnableComponent);
        _manager.RegisterTimer(WalkCDTimer);
        RotateCDTimer = new Timer(RotationCD, false, EnableComponent);
        _manager.RegisterTimer(RotateCDTimer);
        touchHoldTimer = new Timer(0.25f, false, OnTouchHold);
        _manager.RegisterTimer(touchHoldTimer);
    }

    private void OnTap()
    {
        Vector2 position = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        if (position.x < mainCamera.pixelWidth * 0.2)
        {
            leftRotate.Invoke();
        }
        else if (position.x > mainCamera.pixelWidth * 0.8)
        {
            rightRotate.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (swipeHold == true)
        {
            OnSwipeHold();
        }
    } 

    public void OnSwipeStart()
    {
        startPosition = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        startPosition = mainCamera.ScreenToWorldPoint(startPosition);
        startPosition.z = 0;
        endPosition = Vector3.zero;
    }

    public void OnTouchHold(Timer timer)
    {
        swipeHold = true;
    }
    public void OnSwipeEnd()
    {
        endPosition = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        endPosition = mainCamera.ScreenToWorldPoint(endPosition);
        endPosition.z = 0;

        Vector3 diff = endPosition - startPosition;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle < 45)
        {
            rightMove.Invoke();
        }
        else if (angle > 45 && angle < 135)
        {
            topMove.Invoke();
        }
        else if ((angle > 135 && angle < 180) || (angle > -180 && angle < -135))
        {
            leftMove.Invoke();
        }
        else
        {
            downMove.Invoke();
        }
    }

    public void OnSwipeHold()
    {
        endPosition = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        endPosition = mainCamera.ScreenToWorldPoint(endPosition);
        endPosition.z = 0;

        Vector3 diff = endPosition - startPosition;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle < 45)
        {
            rightMove.Invoke();
        }
        else if (angle > 45 && angle < 135)
        {
            topMove.Invoke();
        }
        else if ((angle > 135 && angle < 180) || (angle > -180 && angle < -135))
        {
            leftMove.Invoke();
        }
        else
        {
            downMove.Invoke();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(startPosition, 0.1f);
        if (endPosition != Vector3.zero)
        {
            Gizmos.DrawSphere(endPosition, 0.1f);
            Gizmos.DrawLine(startPosition, endPosition);
        }
    }
    public void DisableComponent(Timer timer)
    {
        timer.Restart();
        gameObject.GetComponent<TouchPlayerController>().enabled = false;
        for (int i = 0; i < FightButtons.Length; i++)
        {
            FightButtons[i].GetComponent<Button>().enabled = false;
        }

    }
    public void EnableComponent(Timer timer)
    {
        gameObject.GetComponent<TouchPlayerController>().enabled = true;
        for (int i = 0; i < FightButtons.Length; i++)
        {
            if(FightButtons[i].GetComponent<FightButton>().timerActive == false)
            {
                FightButtons[i].GetComponent<Button>().enabled = true;
            }
        }
    }

}
