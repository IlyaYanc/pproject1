using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class TrainingLevel : MonoBehaviour
{
    public GameObject[] butttonsToBlink = new GameObject[0];
    public Color ActiveUIColor;
    public Color InactiveUIColor;
    private bool UIBlinking = false;

    public GameObject[] levelObjectsToBlink = new GameObject[0];
    public Color ActiveObjectColor;
    public Color InactiveObjectColor;
    private bool ObjectBlinking = false;

    public float colorChangeSpeed;

    public GameObject[] trapsToActivate = new GameObject[0];
    public bool waitsForPlayer;
    public GameObject[] doorsToClose = new GameObject[0];

    public GameObject[] objectsToSetActive = new GameObject[0];
    public GameObject[] objectsToSetInactive = new GameObject[0];



    private bool hasEnteredTrigger = false;

    private void Update()
    {
        BlinkAllButtons();
        BlinkAllObjects();
        ActivateTraps();
    }

    public void BlinkUiButton(GameObject uiButton)
    {
        //Debug.Log(UIBlinking);
        if (UIBlinking)
        {
            if (hasEnteredTrigger)
                uiButton.GetComponent<Image>().color = Color.Lerp(InactiveUIColor, ActiveUIColor, Mathf.PingPong(Time.time * colorChangeSpeed, 1));
        }
        if(!UIBlinking)
        {
            uiButton.GetComponent<Image>().color = InactiveUIColor;
        }
    }

    public void BlinkAllButtons()
    {
        for (int i = 0; i < butttonsToBlink.Length; i++)
        {
            BlinkUiButton(butttonsToBlink[i]);
        }
    }

    public void BlinkObject(GameObject levelobject)
    {
        if (ObjectBlinking)
        {
            if(hasEnteredTrigger)
                levelobject.GetComponent<SpriteRenderer>().color = Color.Lerp(InactiveObjectColor, ActiveObjectColor, Mathf.PingPong(Time.time * colorChangeSpeed, 1));
        }
        if (!ObjectBlinking)
        {
            levelobject.GetComponent<SpriteRenderer>().color = InactiveObjectColor;
        }
    }

    public void BlinkAllObjects()
    {
        for (int i = 0; i < levelObjectsToBlink.Length; i++)
        {
            BlinkObject(levelObjectsToBlink[i]);
        }
    }

    public void ActivateTraps()
    {
        for (int i = 0; i < trapsToActivate.Length; i++)
        {
            if(trapsToActivate[i] != null)
            {
                if (hasEnteredTrigger)
                    trapsToActivate[i].GetComponent<breacTrap>().isActive = true;
                if (trapsToActivate[i].GetComponent<breacTrap>().n == 0)
                {
                    if (waitsForPlayer)
                    {
                        if(trapsToActivate[i].GetComponent<breacTrap>().timer <= 0.1f)
                            trapsToActivate[i].GetComponent<breacTrap>().timer += Time.deltaTime;
                    }

                }
            }

        }
    }

    public void ClosePreviousDoors()
    {
        for(int i = 0; i < doorsToClose.Length; i++)
        {
            doorsToClose[i].GetComponent<Door>().SetStage(0);
            doorsToClose[i].GetComponent<Door>().PutTile(doorsToClose[i].GetComponent<Door>().groundWallTile);
        }
    }

    public void StopUIBlinking()
    {
        UIBlinking = false;
    }

    public void StopObjectsBlinking()
    {
        ObjectBlinking = false;
    }

    public void SetObjectsActive()
    {
        for (int i = 0; i < objectsToSetActive.Length; i++)
        {
            objectsToSetActive[i].SetActive(true);
        }
    }

    public void SetObjectsInactive()
    {
        for (int i = 0; i < objectsToSetInactive.Length; i++)
        {
            objectsToSetInactive[i].SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasEnteredTrigger = true;
            UIBlinking = true;
            ObjectBlinking = true;
            ClosePreviousDoors();
            SetObjectsActive();
            SetObjectsInactive();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIBlinking = false;
            ObjectBlinking = false;
            waitsForPlayer = false;
        }
    }

}
