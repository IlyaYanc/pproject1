using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hi: MonoBehaviour
{
    public GameObject panel { get; private set; }
    public Image loadingbar { get; private set; }
    public Sprite[] a { get; private set; }

    /// <value>
    /// Номер сцены(можно посмотреть в BuildSettings)
    /// </value>
    public int NumberScene = 1;
    // Start is called before the first frame update
    /// <value>Начинает загрузку</value>
    public void SDS()
    {
        panel.SetActive(true);

        loadingbar.sprite = a[Random.Range(0, a.Length)];

        gameObject.SendMessage("LoadAsync", NumberScene);
    }
}
