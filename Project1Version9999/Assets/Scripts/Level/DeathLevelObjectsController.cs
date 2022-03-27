using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLevelObjectsController : MonoBehaviour
{
    [SerializeField] private magicTrap[] MagicTraps = new magicTrap[0];
    [SerializeField] private breacTrap[] BreakTraps = new breacTrap[0];
    [SerializeField] private enemy[] Enemies = new enemy[0];
    //levers
    //doors
    public void DisableLevelObjects()
    {
        for (int i = 0; i < MagicTraps.Length; i++)
        {
            MagicTraps[i].enabled = false;
        }

        for (int i = 0; i < BreakTraps.Length; i++)
        {
            BreakTraps[i].enabled = false;
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].enabled = false;
        }
    }

    public void EnableLevelObjects()
    {
        for (int i = 0; i < MagicTraps.Length; i++)
        {
            MagicTraps[i].enabled = true;
        }

        for (int i = 0; i < BreakTraps.Length; i++)
        {
            BreakTraps[i].enabled = true;
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].enabled = true;
        }
    }
}
