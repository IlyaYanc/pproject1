using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTrainingLevel : MonoBehaviour
{
    public GameObject[] TextPanels = new GameObject[0];
    [Header("please just set the amount, same as for textpanels")]
    public bool[] ActivatedTexts = new bool[0];
    public void EnableActiveTexts()
    {
        for(int i = 0; i< TextPanels.Length;i++)
        {
            if(ActivatedTexts[i] == true)
            {
                TextPanels[i].SetActive(true);
            }
        }
    }
    public void DisableActiveTexts()
    {
        for (int i = 0; i < TextPanels.Length; i++)
        {
            if (TextPanels[i].activeInHierarchy)
            {
                ActivatedTexts[i] = true;
            }
            else
            {
                ActivatedTexts[i] = false;
            }
            TextPanels[i].SetActive(false);
        }
    }
}
