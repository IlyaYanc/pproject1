using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDS : MonoBehaviour
{
    public GameObject panel;
    public Image loadingbar;
    public Sprite[] a;

    /// <value>
    /// Номер сцены(можно посмотреть в BuildSettings)
    /// </value>
    public int NumberScene;
    // Start is called before the first frame update
    /// <value>Начинает загрузку</value>
    public void SDS()
    {
        panel.SetActive(true);
        
            loadingbar.sprite = a[Random.Range(0,a.Length)];
        
        gameObject.SendMessage("LoadAsync",NumberScene);
    }
}
