using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrap : MonoBehaviour
{
    [SerializeField]
    private Door dr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dr.SetStage(0);
        }
    }

    public void openDoorTrap()
    {
        dr.SetStage(2);
    }
}
