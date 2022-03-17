using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Code : MonoBehaviour
{
    private string code;
    public string tr="1234";
    private int c=0;
    public Text txt;
    
    public void But(int i)
    {
        code = code + i;
        txt.text = code;
        c++;
        if (c == 4)
        {
            if (code == tr)
                Win();
            else
                Louse();
        }
    }
    
    private void Win()
    {
        Debug.Log("Win");
        code = null;
        c = 0;
        txt.text = code;
        gameObject.SetActive(false);
    }
    private void Louse()
    {
        Debug.Log("Louse");
        code = null;
        c = 0;
        txt.text = code;
    }
}
