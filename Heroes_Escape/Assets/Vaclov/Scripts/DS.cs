using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DS : MonoBehaviour
{

    public Image lodingbar;
    public Text txt;
    public Text Press;
    public GameObject Panel;
    public int scen;
    /// <value>
    /// Загружает сценну
    /// </value>
    /// <param name="scene">Номер сцены(Просмотреть в настройках билда)</param>
    public void LoadingScene()
    {
        //scen = scene;
        StartCoroutine(LoadAsync());
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation Acy = SceneManager.LoadSceneAsync(2);
        Acy.allowSceneActivation = false;
        while (!Acy.isDone)
        {
            lodingbar.fillAmount = Acy.progress;
            txt.text = (int)(Acy.progress * 100) + "%";
            if (Acy.progress >= .9F && !Acy.allowSceneActivation)
            {
                lodingbar.gameObject.SetActive(false);
                txt.gameObject.SetActive(false);
                Press.gameObject.SetActive(true);
                if (Input.anyKeyDown)
                {
                    Acy.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
