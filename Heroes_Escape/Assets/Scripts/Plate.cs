using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject activatedObject;
    [SerializeField] private float waitTime = 5f;

    private activObject scr;
    private float timer;
    private bool timerIsRun = false;
    // Start is called before the first frame update
    void Start()
    {
        scr = activatedObject.GetComponent<activObject>();
    }

    /*(player.transform.position.x < transform.position.x + 0.2 || player.transform.position.x < transform.position.x - 0.2) &&
       (player.transform.position.y < transform.position.y + 0.2 || player.transform.position.y < transform.position.y - 0.2)*/
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position==transform.position) 
        {
            if (timerIsRun == false)
            {
                timerIsRun = true;
                scr.ChangeValue(1);
            }
            timer = waitTime;
           // Debug.Log("timer is Start!");
        }
        else if (timerIsRun)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && timerIsRun)
        {
            scr.ChangeValue(-1);
            timerIsRun = false;
            //Debug.Log("time is out!");
        }
    }
}
