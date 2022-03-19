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

    [SerializeField] private float RotationTime;
    [SerializeField] private float WalkTime;
    private AnimationController KnightGraphicsAnimationController;
    private AnimationController ThiefGraphicsAnimationController;
    private AnimationController ArcherGraphicsAnimationController;
    private AnimationController MageGraphicsAnimationController;
    [SerializeField] private Button[] ActionButtons = new Button[0];

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
            Debug.Log(control.name);
        }
        KnightGraphicsAnimationController = KnightGraphics.GetComponent<AnimationController>();
        ThiefGraphicsAnimationController = ThiefGraphics.GetComponent<AnimationController>();
        ArcherGraphicsAnimationController = ArcherGraphics.GetComponent<AnimationController>();
        MageGraphicsAnimationController = MageGraphics.GetComponent<AnimationController>();

    }

    private void OnTap()
    {
        Debug.Log("Tap!");
        Vector2 position = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        if (position.x < mainCamera.pixelWidth * 0.2)
        {
            Debug.Log("Left Rotate");
            GraphicsLeftRotate();
            leftRotate.Invoke();
            DisableComponent(RotationTime);
        }
        else if (position.x > mainCamera.pixelWidth * 0.8)
        {
            Debug.Log("Right Rotate");
            GraphicsRightRotate();
            rightRotate.Invoke();
            DisableComponent(RotationTime);
        }
    }

    public void OnSwipeStart()
    {
        startPosition = playerInput.PlayerMoving.Position.ReadValue<Vector2>();
        startPosition = mainCamera.ScreenToWorldPoint(startPosition);
        startPosition.z = 0;

        endPosition = Vector3.zero;
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
            GraphicsMove();
            Debug.Log("Right Move");
            DisableComponent(WalkTime);
        }
        else if (angle > 45 && angle < 135)
        {
            topMove.Invoke();
            GraphicsMove();
            Debug.Log("Top Move");
            DisableComponent(WalkTime);
        }
        else if ((angle > 135 && angle < 180) || (angle > -180 && angle < -135))
        {
            leftMove.Invoke();
            GraphicsMove();
            Debug.Log("Left Move");
            DisableComponent(WalkTime);
        }
        else
        {
            downMove.Invoke();
            GraphicsMove();
            Debug.Log("Down Move");
            DisableComponent(WalkTime);
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

    public void GraphicsLeftRotate()
    {
        KnightGraphicsAnimationController.RotateLeftButton();
        ThiefGraphicsAnimationController.RotateLeftButton();
        ArcherGraphicsAnimationController.RotateLeftButton();
        MageGraphicsAnimationController.RotateLeftButton();
    }

    public void GraphicsRightRotate()
    {
        KnightGraphicsAnimationController.RotateRightButton();
        ThiefGraphicsAnimationController.RotateRightButton();
        ArcherGraphicsAnimationController.RotateRightButton();
        MageGraphicsAnimationController.RotateRightButton();
    }

    public void GraphicsMove()
    {
        KnightGraphicsAnimationController.Move();
        ThiefGraphicsAnimationController.Move();
        ArcherGraphicsAnimationController.Move();
        MageGraphicsAnimationController.Move();
    }

    public void DisableComponent(float dur)
    {
        gameObject.GetComponent<TouchPlayerController>().enabled = false;
        gameObject.GetComponent<SliderTimer>().m_duration = dur;
        gameObject.GetComponent<SliderTimer>().RestartTimer();
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionButtons[i].enabled = false;
        }

    }
    public void EnableComponent()
    {
        gameObject.GetComponent<TouchPlayerController>().enabled = true;
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionButtons[i].enabled = true;
        }
    }

}
