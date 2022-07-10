using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Rendering.Universal;

public class PanelFloor : MonoBehaviour
{   //https://answers.unity.com/questions/192895/hideshow-properties-dynamically-in-inspector.html
    
    [SerializeField] private Sprite activePanel;
    [SerializeField] private Sprite inactivePanel;
    private SpriteRenderer spriteRenderer;
    [SerializeField] protected activObject[] activeObjects = new activObject[0];
    [Space] [SerializeField] [Header("(This panel always has same Open Value as Referenced Panel (optional))")]
    private PanelFloor referencedPanel;
    [Space]
    [SerializeField]
    [Range(0, 1)]
    [Header("(0-lock; 1-unlock active objects)")]
    private int openValue;

    [SerializeField] private bool toggleOpenValue;
    [Space]
    public breacTrap[] breakTrapsToActivate = new breacTrap[0];
    [Space]
    public shotTrap[] shotTrapsToActivate = new shotTrap[0];
    [Space]
    public Lever[] leversToActivate = new Lever[0];
    [Space]
    public TimerLever[] timerLeversToActivate = new TimerLever[0];

    private AudioSource AudS;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudS = gameObject.GetComponent<AudioSource>();
    }

    private void ActivateActiveObjects()
    {
        for (int i = 0; i < activeObjects.Length; i++)
        {
            if (activeObjects[i] != null && activeObjects[i].isActiveAndEnabled)
            {
                if (activeObjects[i].GetStage() != 2)
                {
                    /*if (toggleOpenValue)
                    {
                        if (activeObjects[i].GetStage() == 1)
                        {
                            activeObjects[i].SetStage(0);
                        }
                        else
                        {
                            activeObjects[i].SetStage(1);
                        }
                        ChangeOpenValue();
                    }
                    else
                    {
                        activeObjects[i].SetStage(openValue);
                    } */
                    activeObjects[i].SetStage(openValue);
                }
            }
        }

        if (toggleOpenValue)
        {
            ChangeOpenValue();
        }
    }

    private void ActivateBreakTraps()
    {
        for (int i = 0; i < breakTrapsToActivate.Length; i++)
        {
            if (breakTrapsToActivate[i] != null && breakTrapsToActivate[i].isActiveAndEnabled)
            {
                breakTrapsToActivate[i].isActive = true;
            }
        }
    }

    private void ActivateShotTraps()
    {
        for (int i = 0; i < shotTrapsToActivate.Length; i++)
        {
            if (shotTrapsToActivate[i] != null && shotTrapsToActivate[i].isActiveAndEnabled)
            {
                shotTrapsToActivate[i].ActivateTrap();
            }
        }
    }
    
    private void DeactivateShotTraps()
    {
        for (int i = 0; i < shotTrapsToActivate.Length; i++)
        {
            if (shotTrapsToActivate[i] != null && shotTrapsToActivate[i].isActiveAndEnabled)
            {
                shotTrapsToActivate[i].DeactivateTrap();
            }
        }
    }
    private void ActivateLevers()
    {
        for (int i = 0; i < leversToActivate.Length; i++)
        {
            if (leversToActivate[i] != null && leversToActivate[i].isActiveAndEnabled)
            {
                leversToActivate[i].Interact();
            }
        }
    }
    
    private void ActivateTimerLevers()
    {
        for (int i = 0; i < timerLeversToActivate.Length; i++)
        {
            if (timerLeversToActivate[i] != null && timerLeversToActivate[i].isActiveAndEnabled)
            {
                timerLeversToActivate[i].Interact();
            }
        }
    }

    private void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetReferencedValue();
            ChangeSprite(activePanel);
            ActivateActiveObjects();
            ActivateBreakTraps();
            ActivateShotTraps();
            ActivateLevers();
            ActivateTimerLevers();
            AudS.Play();
        }

        if (other.CompareTag("enemy")) // удалить если не интересно
        {
            if (other.gameObject.GetComponent<enemy>().hasDisquiet == true)
            {
                SetReferencedValue();
                ChangeSprite(activePanel);
                ActivateActiveObjects();
                ActivateBreakTraps();
                ActivateShotTraps();
                ActivateLevers();
                ActivateTimerLevers();
                AudS.Play();
            }
        } // до сюда удалить
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeSprite(inactivePanel);
            DeactivateShotTraps();
            AudS.Play();
        }
        
        if (other.CompareTag("enemy")) // удалить если не интересно
        {
            if (other.gameObject.GetComponent<enemy>().hasDisquiet == true)
            {
                ChangeSprite(inactivePanel);
                DeactivateShotTraps();
                AudS.Play();
            }
        }
    }
    private void ChangeOpenValue()
    {
        if (openValue == 1)
        {
            openValue = 0;
            return;
        }

        if (openValue == 0)
        {
            openValue = 1;
            return;
        }
    }

    private void SetReferencedValue()
    {
        if(referencedPanel != null)
        openValue = referencedPanel.openValue;
    }
}
