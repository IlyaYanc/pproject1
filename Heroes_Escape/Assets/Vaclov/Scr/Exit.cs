using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void Yes()
    {
        Time.timeScale = 1f;
    }
    public void No()

    {
        gameObject.SetActive(false);
    }
}
