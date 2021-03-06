using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class breacTrap : MonoBehaviour
{
    [SerializeField]
    private Tilemap mp;
    [SerializeField]
    [Header("0 - preHole, last - unhurt, another - interim")]
    private List<Sprite> stageTiles;
    [SerializeField]
    private Tile holeTile;
    [SerializeField]
    [Header("all positions of breaking floar, if 0 -- trap position")]
    private List<SpriteRenderer> holes;
    [SerializeField]
    private List<GameObject> ObjectsOnHoles;
    [SerializeField]
    private float breakDelay;
    [SerializeField]
    private Vector3 ADS_spawningPos;
    private DamageInputController hp;
    public float timer;
    public int n;
    public bool isActive = false;
    private int lastN = -1;
    private GameObject player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] stageAudioClip = new AudioClip[0];
    private TileBase floarTile;
    private bool killed = false;
    // Start is called before the first frame update
    void Start()
    {
        floarTile = mp.GetTile(mp.WorldToCell(gameObject.transform.position));
        timer = breakDelay;
        player = GameObject.FindGameObjectWithTag("Player");
        hp = player.GetComponent<DamageInputController>();
        if(holes.Count == 0)
        {
            holes.Add(gameObject.GetComponent<SpriteRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 0.3 && !isActive)
        {
            isActive = true;
        }
        if(isActive)
        {
            if(timer>0)
            {
                timer -= Time.deltaTime;
                n = Mathf.CeilToInt(stageTiles.Count * timer / breakDelay) - 1;
                if (n < 0)
                    n = 0;
                if (lastN != n)
                {
                    lastN = n;
                    audioSource.clip = stageAudioClip[n];
                    audioSource.Play();
                    for (int i = 0; i < holes.Count; i++)
                    {
                        if(n!=stageTiles.Count-1)
                        {
                            if (holes[i].sprite==stageTiles[n+1])
                            {
                                holes[i].sprite = stageTiles[n];
                            }
                        }
                        else
                        holes[i].sprite = stageTiles[n];
                    }
                }
            }
            else
            {
                for (int i = 0; i < holes.Count; i++)
                {
                    mp.SetTile(mp.WorldToCell(holes[i].transform.position), holeTile);
                    holes[i].gameObject.SetActive(false);
                    if (Mathf.Abs(holes[i].gameObject.transform.position.x-player.transform.position.x)<=0.5 && Mathf.Abs(holes[i].gameObject.transform.position.y - player.transform.position.y) <= 0.5)
                    {
                        kill();
                    }
                }
                for(int i=0;i<ObjectsOnHoles.Count;i++)
                {
                    ObjectsOnHoles[i].SetActive(false);
                }
                isActive = false;
            }
        }
    }
    private void kill()
    {
        if(killed == false)
        {
            hp.DeathOnBreakTrap(ADS_spawningPos, gameObject);
            killed = true;
        }
        
    }

    [Button]
    private void ActivateTrap()
    {
        isActive = true;
    }
    public void Activate()
    {
        for (int i = 0; i < holes.Count; i++)
        {
            holes[i].gameObject.SetActive(true);
            mp.SetTile(mp.WorldToCell(holes[i].transform.position), floarTile);
            holes[i].sprite = stageTiles[stageTiles.Count-1];
        }
        for (int i = 0; i < ObjectsOnHoles.Count; i++)
        {
            ObjectsOnHoles[i].SetActive(true);
        }
        isActive = false;
        killed = false;
        timer = breakDelay;
    }
}
