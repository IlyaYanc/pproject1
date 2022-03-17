using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Bonfire : Interactable
{
    [SerializeField] private SavingSystem savingSystem;
    //[SerializeField] private int healAmmount;
    //[SerializeField] private GameObject player;
    /*private void Start()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
    }*/

    /*void Hill()
    {
        HP HPComp = player.GetComponent<HP>();
        if (HPComp)
        {
            HPComp.GetHill(healAmmount);
        }
    }*/
    public override void Interact()
    {
        //Debug.Log("interact");
        //Hill();
        
        savingSystem.Save();
    }
}
