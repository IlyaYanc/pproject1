using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hellper : MonoBehaviour
{
    public string[] a;
    public Text g;
    private float t=0, tt = 10f;
    private int i;
    private void Start()
    {
        i = Random.Range(0,a.Length);
    }
    private void Update()
    {
        t -= Time.deltaTime;
        if(t<=0)
        {
            g.text = a[i];
            if (i != a.Length-1)
                i++;
            else
                i = 0;
            t = tt;
        }
    }
}
