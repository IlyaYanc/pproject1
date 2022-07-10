using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Search_Script: MonoBehaviour
{
    //Ярик, если читаешь этот код, то почему то у меня OnTriggerStay2D не рвботает, так что... Делай что знаешь
    private bool TF;
    private int shootDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (TF)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, shootDist);
            
            
                if (hit == GameObject.FindGameObjectWithTag("Player"))
                {
                    Search();
                }
            
        }
    }

    /// <value>
    /// Метод действий после нахождения
    /// </value>
    private void Search()
    {
        Debug.Log("Find!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            TF = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            TF = false;
    }
    
}
/// <summary>
/// Name?
/// </summary>
/// <remarks>
/// Описание?
/// </remarks>
/// <value>
/// Свойство?
/// </value>