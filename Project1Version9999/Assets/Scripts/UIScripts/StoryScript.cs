using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class StoryScript : MonoBehaviour
{
    /// <value>
    /// количество текста = количеству картинок
    /// </value>
    [SerializeField]
    private string[] StoryText;
    /// <value>
    /// количество текста = количеству картинок
    /// </value>
    [SerializeField]
    private Sprite[] StoryImage;
    [SerializeField]
    private Image StoryImageUI;
    [SerializeField]
    private TextMeshProUGUI StoryTextUI;
    [SerializeField]
    private Image shirma;
    private int N = 0;

    private void Start()
    {
        StoryImageUI.sprite = StoryImage[0];
        StoryTextUI.text = StoryText[0];
        N++;
    }
    [Button]
    private void NextStory()
    {
        StartCoroutine("VisibleIE");
    }
    IEnumerator VisibleIE()
    {
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            Color color = shirma.color;
            color.a = f;
            shirma.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        if (N == StoryText.Length) gameObject.SetActive(false);
        StoryImageUI.sprite = StoryImage[N];
        StoryTextUI.text = StoryText[N];
        N++;
        StartCoroutine("INVisibleIE");
    }
    IEnumerator INVisibleIE()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            Color color = shirma.color;
            color.a = f;
            shirma.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
