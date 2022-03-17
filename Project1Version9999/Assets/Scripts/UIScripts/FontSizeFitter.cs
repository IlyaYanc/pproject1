using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontSizeFitter : MonoBehaviour
{
    [SerializeField] private GameObject ReverenceObject;
    [SerializeField] private int ObjectsAmount;
    [SerializeField] private GameObject[] TextObjects = new GameObject[1];
    private float ReverenceObjectTMPFontSize;

    private void Start()
    {
        ReverenceObject.GetComponent<TextMeshProUGUI>().ForceMeshUpdate(false);
        ReverenceObjectTMPFontSize = ReverenceObject.GetComponent<TextMeshProUGUI>().fontSize;
        FitFontSize();
    }

    void OnEnable()
    {

        ReverenceObject.GetComponent<TextMeshProUGUI>().ForceMeshUpdate(false);
        ReverenceObjectTMPFontSize = ReverenceObject.GetComponent<TextMeshProUGUI>().fontSize;
        FitFontSize();
    }

   public void FitFontSize()
    { 
        for (int i = 0; i < ObjectsAmount; i++)
        {
            TextObjects[i].GetComponent<TextMeshProUGUI>().fontSize = ReverenceObjectTMPFontSize;
        }
    }

    public void EnableFontSizeFitter()
    {
        gameObject.SetActive(true);
    }
}
