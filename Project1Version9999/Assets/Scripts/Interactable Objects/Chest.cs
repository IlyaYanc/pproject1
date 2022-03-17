using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : activObject
{
    //loot
    [SerializeField]
    LootManager lootManager;
    [SerializeField]
    InventoryComponent inventory;

    [SerializeField]
    private GameObject lootEmpty;

    [SerializeField]
    private AudioClip close, open, take;

    [SerializeField]
    private SpriteRenderer sRenderer;

    [Space][SerializeField][Range(0, 2)][Header("(0-locked; 1-closed; 2-opened)")]
    private int startStage; //0-locked; 1-closed; 2-opened;

    [Space][SerializeField]
    private Sprite[] sprites;

    private ItemBase droppedLoot;

    private AudioSource AudS;

    private void Start()
    {
        AudS = gameObject.GetComponent<AudioSource>();
        stage = startStage;
        ChangeSprite(stage);
        lootEmpty.SetActive(false);
        if (!multipleLevers)
            openValue = 1;
    }

    
    public void OpenChest()
    {
        stage = 2;
        ChangeSprite(stage);
        GenerateLoot();
    }
    void GenerateLoot()
    {
        ItemBase[] loot = lootManager.GetItems();
        droppedLoot = loot[Random.Range(0, loot.Length)];
        lootEmpty.GetComponent<SpriteRenderer>().sprite = droppedLoot.inventorySprite;
        lootEmpty.SetActive(true);
    }

    public override void SetStage(int _stage)
    {
        if (_stage >= 0 && _stage <= 2)
        {
            this.stage = _stage;
        }
        else this.stage = 0;
        ChangeSprite(stage);
        if(stage != 2)
            lootEmpty.SetActive(false);
    }
    private void ChangeSprite(int _stage)
    {
        //Debug.Log(_stage);
        sRenderer.sprite = sprites[_stage];
    }
    public override void Interact()
    {
        if (stage == 1)
        {
            OpenChest();
            AudS.clip = open;
            AudS.Play();
        }
        else if (stage == 2 && lootEmpty.activeSelf)
        {
            PickLoot();
            AudS.clip = take;
            AudS.Play();
        }
        else if (stage == 0)
        {
            AudS.clip = close;
            AudS.Play();
        }
        
    }

    private void PickLoot()
    {
        if (!inventory.HasFreeCells()) return;
        lootEmpty.SetActive(false);
        inventory.AddItem(droppedLoot);
    }

    public override void LeverInteract(int _value = 0)
    {
        /*if (!multipleLevers)
        {
            SetStage(1);
        }
        else
        {
            ChangeValue(_value);
        }*/
        ChangeValue(_value);
    }
}
