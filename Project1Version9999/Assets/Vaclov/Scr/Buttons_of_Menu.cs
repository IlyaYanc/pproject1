using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_of_Menu : MonoBehaviour
{
    public GameObject inv_panel, opt_panel, exit_panel, inv_background;
   public void Open_Inventory()
    {
        inv_panel.SetActive(true);
        inv_background.SetActive(true);
    }
   public void Rusme()
    {
        gameObject.SetActive(false);
    }
    public void Options()
    {
        opt_panel.SetActive(true);
    }
    public void Exit()
    {
        exit_panel.SetActive(true);
    }
}
