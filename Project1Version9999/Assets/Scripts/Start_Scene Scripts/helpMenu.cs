using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MP, IP, BP, AP, LP, SM;
    [SerializeField]
    private GameObject MB, IB, BB, AB, LB;
    private Color32 ActiveColor;
    private Color32 InactiveColor;

    private void Start()
    {
        ActiveColor = new Color32(171, 142, 77, 255);
        InactiveColor = new Color32(75, 75, 75, 255);
    }
    public void Move_button()
    {
        MP.SetActive(true);
        IP.SetActive(false);
        BP.SetActive(false);
        AP.SetActive(false);
        LP.SetActive(false);

        MB.GetComponent<Image>().color = ActiveColor;
        IB.GetComponent<Image>().color = InactiveColor;
        BB.GetComponent<Image>().color = InactiveColor;
        AB.GetComponent<Image>().color = InactiveColor;
        LB.GetComponent<Image>().color = InactiveColor;
    }
    public void Inter_button()
    {
        IP.SetActive(true);
        MP.SetActive(false);
        BP.SetActive(false);
        AP.SetActive(false);
        LP.SetActive(false);

        MB.GetComponent<Image>().color = InactiveColor;
        IB.GetComponent<Image>().color = ActiveColor;
        BB.GetComponent<Image>().color = InactiveColor;
        AB.GetComponent<Image>().color = InactiveColor;
        LB.GetComponent<Image>().color = InactiveColor;
    }
    public void Battel_button()
    {
        BP.SetActive(true);
        IP.SetActive(false);
        MP.SetActive(false);
        AP.SetActive(false);
        LP.SetActive(false);

        MB.GetComponent<Image>().color = InactiveColor;
        IB.GetComponent<Image>().color = InactiveColor;
        BB.GetComponent<Image>().color = ActiveColor;
        AB.GetComponent<Image>().color = InactiveColor;
        LB.GetComponent<Image>().color = InactiveColor;
    }
    public void Abil_button()
    {
        AP.SetActive(true);
        IP.SetActive(false);
        BP.SetActive(false);
        MP.SetActive(false);
        LP.SetActive(false);

        MB.GetComponent<Image>().color = InactiveColor;
        IB.GetComponent<Image>().color = InactiveColor;
        BB.GetComponent<Image>().color = InactiveColor;
        AB.GetComponent<Image>().color = ActiveColor;
        LB.GetComponent<Image>().color = InactiveColor;
    }
    public void Loot_button()
    {
        LP.SetActive(true);
        IP.SetActive(false);
        BP.SetActive(false);
        AP.SetActive(false);
        MP.SetActive(false);

        MB.GetComponent<Image>().color = InactiveColor;
        IB.GetComponent<Image>().color = InactiveColor;
        BB.GetComponent<Image>().color = InactiveColor;
        AB.GetComponent<Image>().color = InactiveColor;
        LB.GetComponent<Image>().color = ActiveColor;
    }
    public void Ex_button()
    {
        SM.SetActive(true);
        gameObject.SetActive(false);
    }
}

