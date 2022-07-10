using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_START : MonoBehaviour
{
    public Text textUI;
    public GameObject but;
    private string text = "Спасибо за тестирование игры! Вы очень нам помогаете! С любовью Unity Laba❤";

    void Start()
    {
        StartCoroutine("showText", text);
    }

    IEnumerator showText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.05f);
        }
        but.SetActive(true);
    }
    
    
}