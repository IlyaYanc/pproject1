using System.Collections;
using System.Collections.Generic;
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
    private DamageInputController hp;
    private float timer;
    private bool isActive = false;
    private int lastN = -1;
    private GameObject player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] stageAudioClip = new AudioClip[0];
    // Start is called before the first frame update
    void Start()
    {
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
                int n = Mathf.CeilToInt(stageTiles.Count * timer / breakDelay) - 1;
                if (n < 0)
                    n = 0;
                if (lastN != n)
                {
                    lastN = n;
                    audioSource.clip = stageAudioClip[n];
                    audioSource.Play();
                    for (int i = 0; i < holes.Count; i++)
                    {
                        holes[i].sprite = stageTiles[n];
                    }
                }
            }
            else
            {
                for (int i = 0; i < holes.Count; i++)
                {
                    mp.SetTile(mp.WorldToCell(holes[i].transform.position), holeTile);
                    Destroy(holes[i].gameObject);
                    if(Mathf.Abs(holes[i].gameObject.transform.position.x-player.transform.position.x)<=0.5 && Mathf.Abs(holes[i].gameObject.transform.position.y - player.transform.position.y) <= 0.5)
                    {
                        kill();
                    }
                }
                for(int i=0;i<ObjectsOnHoles.Count;i++)
                {
                    Destroy(ObjectsOnHoles[i]);
                }
                isActive = false;
                Destroy(gameObject);
            }
        }
    }
    private void kill()
    {
        hp.killAll();
    }
}
