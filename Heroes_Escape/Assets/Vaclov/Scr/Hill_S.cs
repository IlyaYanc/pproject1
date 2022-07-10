using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hill_S : MonoBehaviour
{
    public int S;
    
    public Text C;
    public void Out()
    {
        
        if (S == 0)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        if (S != 0)
            S--;
        C.text = S + " Hill";
    }
    public void Get()
    {
        S++;
        if (S > 0)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        C.text = S + " Hill";
    }
}
