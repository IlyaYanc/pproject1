using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Death_Screen : MonoBehaviour
{
    public GameObject MainPanel;
    public Image s_image;
    public GameObject ds;
    private AudioSource Ads;
    private void Start()
    {
        StartCoroutine("VisibleIE");
        Ads = MainPanel.GetComponent<AudioSource>();
        Ads.Play();
    }
    IEnumerator VisibleIE()
    {
        for (float f = 0.03f; f <= 1f; f += 0.03f)
        {
            Color color = s_image.color;
            color.a = f;
            s_image.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        ds.SetActive(true);
        StartCoroutine("INVisibleIE");
    }
    IEnumerator INVisibleIE()
    {
        for (float f = 1f; f >= 0.03f; f -= 0.03f)
        {
            Color color = s_image.color;
            color.a = f;
            s_image.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        
        s_image.gameObject.SetActive(false);

    }


}
