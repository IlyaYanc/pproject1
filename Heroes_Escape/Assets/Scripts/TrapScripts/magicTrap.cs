using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicTrap : MonoBehaviour
{
    [SerializeField]
    private Sprite notActive;
    [SerializeField]
    private Sprite active;
    [SerializeField]
    private float viewDist;
    [SerializeField]
    private float damagePerSec;
    [SerializeField]
    private float time;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float newDotDelay;
    private float timer;
    private DamageInputController playerHp;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips = new AudioClip[0]; //0 - visible, 1 - active

    // Start is called before the first frame update
    void Start()
    {
        timer = newDotDelay;
        playerHp = player.GetComponent<DamageInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist<=viewDist)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (dist<=0.25 && timer <= 0)
        {
            timer = newDotDelay;
            playerHp.TakeDamage(damagePerSec, time);

        }
        if(dist<=0.25)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = active;
            audioSource.clip = audioClips[1];
            //Debug.Log(timer);
            if(!audioSource.isPlaying && audioSource.enabled == true)
            {
                audioSource.Play();
            }


        }
        else
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == active) audioSource.Stop(); 
            timer = 0; 
            gameObject.GetComponent<SpriteRenderer>().sprite = notActive;
            //audioSource.Stop();

        }
        //if (dist > 0.25 && gameObject.GetComponent<SpriteRenderer>().sprite == active) { audioSource.Stop(); Debug.Log("STOP"); }
    }
}
