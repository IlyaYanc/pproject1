using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Rendering.Universal;

public class PanelFloor : MonoBehaviour
{
    [SerializeField] private Sprite activePanel;
    [SerializeField] private Sprite inactivePanel;
    private SpriteRenderer spriteRenderer;
    [SerializeField] protected activObject _activeObject;
    [Space]
    [SerializeField]
    [Range(0, 1)]
    [Header("(0-lock; 1-unlock)")]
    private int startValue;
    private AudioSource AudS;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudS = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = activePanel;
            if (_activeObject.GetStage() != 2)
            {
                _activeObject.SetStage(startValue);
                ChangeValue();   
            }
            AudS.Play();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = inactivePanel;
            AudS.Play();
        }
    }

    private void ChangeValue()
    {
        if (startValue == 1)
        {
            startValue = 0;
            return;
        }

        if (startValue == 0)
        {
            startValue = 1;
            return;
        }
    }
}
