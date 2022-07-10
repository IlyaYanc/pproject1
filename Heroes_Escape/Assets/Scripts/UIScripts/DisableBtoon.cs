using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableBtoon : MonoBehaviour
{
    public GameObject Button;

    public void Disable()
    {
        Button.GetComponent<Button>().enabled = false;
    }
}
