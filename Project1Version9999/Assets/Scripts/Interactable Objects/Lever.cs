using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Lever : Interactable
{
    private AudioSource AudS;
   

    [SerializeField] protected activObject obj;
    protected bool isActive = false;
    protected bool hasInteracted = false;

    [SerializeField]
    protected SpriteRenderer sRenderer;

    [Space][SerializeField]
    protected Sprite[] sprites;

    private void Start()
    {
        AudS = gameObject.GetComponent<AudioSource>();
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sprites[0];
    }
    public override void Interact()
    {
        if (!hasInteracted)
        {
            obj.LeverInteract(1);
            hasInteracted = true;
            sRenderer.sprite = sprites[1];
            AudS.Play();
        }
    }

    public bool ReturnHasInterected()
    {
        return hasInteracted;
    }
    
    public virtual void LoadData(LeverSavingData data)
    {
        hasInteracted = data.hasInteracted;
        if (hasInteracted)
        {
            obj.LeverInteract(1);
            sRenderer.sprite = sprites[1];
        }
        
    }
}

public class LeverSavingData
{
    public bool hasInteracted;

    public LeverSavingData( bool _hasInteracted)
    {
        hasInteracted = _hasInteracted;
    }
}
