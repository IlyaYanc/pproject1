using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    [SerializeField]
    [Header("(You should kill them all to open doors)")]
    private List<enemy> enemies;
    [SerializeField]
    private List<Door> doors;
    private int openValue;
    private bool doorsWasOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        openValue = enemies.Count;
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for(int i=0;i<openValue;i++)
        {
            if (enemies[i] == null)
                count++;
        }
        if(count >= openValue && !doorsWasOpen)
        {
            for (int i=0; i < doors.Count;i++)
            {
                doors[i].SetStage(1);
            }
            doorsWasOpen = true;
        }
    }
}
