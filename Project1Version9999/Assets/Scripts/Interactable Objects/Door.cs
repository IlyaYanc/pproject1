using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

//public class Door : Openable
public class Door : activObject
{
    private AudioSource AudS;
    [SerializeField]
    private SpriteRenderer sRenderer;

    [SerializeField]
    private AudioClip openLock, open;

    [Space]
    [SerializeField]
    [Range(0, 2)]
    [Header("(0-locked; 1-closed; 2-opened)")]
    private int startStage; //0-locked; 1-closed; 2-opened;

    [Space]
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile groundTile;
    public Tile groundWallTile;

    [SerializeField] private MonoBehaviour shadowCasterClosed;
    [SerializeField] private MonoBehaviour shadowCasterOpened;

    [SerializeField] private GameObject[] roomObjects = new GameObject[0];

    private void Start()
    {
        AudS = gameObject.GetComponent<AudioSource>();
        stage = startStage;
        ChangeSprite(stage);
        if (!multipleLevers)
            openValue = 1;
        if (stage != 2)
        {
            PutTile(groundWallTile);
            shadowCasterClosed.enabled = true;
            shadowCasterOpened.enabled = false;
        }
        DeactivateRoomObjects();
    }

    /* private void Update()
     {
         if (Input.GetKeyDown(KeyCode.Alpha1))
         {
             if (stage == 1)
                 OpenDoor();
         }
     } */
    

    public void PutTile(Tile tile)
    {
        Vector3Int gridPos = tilemap.WorldToCell(transform.position);
        tilemap.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), tile);
    }

    public void OpenDoor()
    {
        stage = 2;
        PutTile(groundTile);
        ChangeSprite(stage);
        shadowCasterClosed.enabled = false;
        shadowCasterOpened.enabled = true;
        AudS.clip = open;
        AudS.Play();
        ActivateRoomObjects();
    }
  
    public override void SetStage(int _stage)
    {
        if (_stage == 2 && this.stage !=2)
        {
            OpenDoor();
        }
        if(_stage == 1 && this.stage== 0)
        {
             
        }
        if (_stage >= 0 && _stage <= 2)
        {
            this.stage = _stage;
        }
        else this.stage = 0;
        ChangeSprite(stage);
        }
    private void ChangeSprite(int _stage)
    {
        sRenderer.sprite = sprites[_stage];
    }
    public override void Interact()
    {
        if (stage == 1)
        {
            OpenDoor();
        }
        else if (stage == 0)
        {
            AudS.clip = openLock;
            AudS.Play();
        }
        
    }
    public override void LeverInteract(int _value = 0)
    {
        ChangeValue(_value);
    }

    private void Update()
    {
        if (stage == 2)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        else
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void ActivateRoomObjects()
    {
        for(int i = 0; i<roomObjects.Length; i++)
        {
            roomObjects[i].SetActive(true);
        }
    }

    private void DeactivateRoomObjects()
    {
        for (int i = 0; i < roomObjects.Length; i++)
        {
            roomObjects[i].SetActive(false);
        }
    }
}
