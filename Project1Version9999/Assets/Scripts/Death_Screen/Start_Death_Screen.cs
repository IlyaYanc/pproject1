using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Death_Screen : MonoBehaviour
{
    //public Image s_image;
    //public GameObject ds;
    [SerializeField]
    private GameObject fon;
    [SerializeField]
    private Image shirma;
    [SerializeField]
    private GameObject ADSBut;
    [SerializeField]
    private GameObject menuBut;
    [SerializeField]
    private DeathAudioSourceController deathAudioSourceController;
    private AudioSource Ads;
    private void Start()
    {
        Ads = gameObject.GetComponent<AudioSource>();
        Act(false);
        shirma.gameObject.SetActive(false);
    }
    public void Activate()
    {
        Ads.Play();
        shirma.gameObject.SetActive(true);
        StartCoroutine("VisibleIE");
        Act(true);
        StartCoroutine("INVisibleIE");
        shirma.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    private void Act (bool condition)
    {
        menuBut.SetActive(condition);
        ADSBut.SetActive(condition);
        fon.SetActive(condition);
    }
    IEnumerator VisibleIE()
    {
        for (float f = 0.03f; f <= 1f; f += 0.03f)
        {
            Color color = shirma.color;
            color.a = f;
            shirma.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator INVisibleIE()
    {
        for (float f = 1f; f >= 0.03f; f -= 0.03f)
        {
            Color color = shirma.color;
            color.a = f;
            shirma.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void DisActivate()
    {
        Act(false);
        deathAudioSourceController.EnableAudioSource();
    }


}
