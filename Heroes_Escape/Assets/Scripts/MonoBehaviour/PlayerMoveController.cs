using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onMove;
    [SerializeField] private UnityEvent m_onRightRotate;
    [SerializeField] private UnityEvent m_onLeftRotate;
    [SerializeField] private float m_cellSize;
    [SerializeField] private Tilemap m_tileMap;
    public float m_moveDelay;
    public float m_rotateDelay;
    [SerializeField] private float m_moveAnimDuration;
    [SerializeField] private float m_rotateAnimDuration;

    [SerializeField] private Animator animator;
    [SerializeField] private bool useAnimations;


    public Timer _moveTimer;
    public Timer _rotateTimer;
    public TimerManager _manager;

    private Transform playerTransform;
    private Effects ef;

    private bool isMoveOrRotate => (!_moveTimer.IsCompleted && !_moveTimer.IsPaused) ||
                                   (!_rotateTimer.IsCompleted && !_rotateTimer.IsPaused);

    private void Start()
    {
        playerTransform = transform;
        _moveTimer = new Timer(m_moveDelay);
        _rotateTimer = new Timer(m_rotateDelay);
        ef = gameObject.GetComponent<Effects>();
        _manager = (TimerManager)GameObject.FindObjectOfType(typeof(TimerManager));
        if (_manager != null)
        {
            _manager.RegisterTimer(_moveTimer);
            _manager.RegisterTimer(_rotateTimer);
        }
        else
        {
            //Debug.LogWarning("GameObject with TimerManager script not found!");
        }
    }

    public void MoveUp()
    {
        //Debug.Log("MoveUp");
        if (useAnimations)
        {
            animator.SetBool("WalkUP", true);
            animator.SetBool("WalkD", false);
            animator.SetBool("WalkR", false);
            animator.SetBool("WalkL", false);
        }

        Move(new Vector2(0, m_cellSize));
    }

    public void MoveDown()
    {
        //Debug.Log("MoveDown");
        if (useAnimations)
        {
            animator.SetBool("WalkUP", false);
            animator.SetBool("WalkD", true);
            animator.SetBool("WalkR", false);
            animator.SetBool("WalkL", false);
        }

        Move(new Vector2(0, -m_cellSize));
    }

    public void MoveLeft()
    {
        //Debug.Log("MoveLeft");
        if (useAnimations)
        {
            animator.SetBool("WalkUP", false);
            animator.SetBool("WalkD", false);
            animator.SetBool("WalkR", false);
            animator.SetBool("WalkL", true);
        }

        Move(new Vector2(-m_cellSize, 0));
    }

    public void MoveRight()
    {
        //Debug.Log("MoveRight");
        if (useAnimations)
        {
            animator.SetBool("WalkUP", false);
            animator.SetBool("WalkD", false);
            animator.SetBool("WalkR", true);
            animator.SetBool("WalkL", false);
        }

        Move(new Vector2(m_cellSize, 0));
    }

    public void RotateLeft()
    {
        //Debug.Log("RotateLeft");
        Rotate(90);
    }

    public void RotateRight()
    {
        //Debug.Log("RotateRight");
        Rotate(-90);
    }

    private void Move(Vector2 offset)
    {
        if (isMoveOrRotate)
        {
            return;
        }
        
        Vector2 rotatedOffset = offset; //Quaternion.Euler(0, 0, playerTransform.rotation.eulerAngles.z) * offset;
        if (GetCellGroundType(rotatedOffset + (Vector2)playerTransform.position) == GroundTile.GroundType.Ground)
        {
            playerTransform.DOMove((Vector2)playerTransform.position + rotatedOffset, m_moveAnimDuration);
            ef.MoveEffects();
        }
        
        _moveTimer.Restart();
        m_onMove.Invoke();
    }

    private void Rotate(float angle)
    {
        if (isMoveOrRotate)
        {
            return;
        }

        if (angle >= 0)
        {
            m_onLeftRotate.Invoke();
        }
        else
        {
            m_onRightRotate.Invoke();
        }
        ef.MoveEffects();
        playerTransform.DORotate(new Vector3(0, 0, angle + playerTransform.rotation.eulerAngles.z),
            m_rotateAnimDuration);

        _rotateTimer.Restart();
    }

    private GroundTile.GroundType GetCellGroundType(Vector2 position)
    {
        var cellPostion = m_tileMap.WorldToCell(position);
        var tile = m_tileMap.GetTile(cellPostion);
        if (tile is GroundTile groundTile)
        {
            return groundTile.Type;
        }
        else
        {
            //Debug.LogWarning($"Tile on {position} not a GroundTile");
            return GroundTile.GroundType.Wall;
        }
    }
}