using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAdd : MonoBehaviour
{
    [SerializeField] private int maxAdsAmount;
    [SerializeField] private float durationBetweenADs;
    [SerializeField] private float adDuration;
    [SerializeField] private GameObject adButton;
    [SerializeField] private GameObject dropImage;
    [SerializeField] private Text timerText;
    private ItemBase item;
    private int adsPlayed = 0;
    [SerializeField] InventoryComponent inventory;
    [SerializeField] ItemBase[] dropItemsList = new ItemBase[0];

    private Timer adDurationTimer;
    private Timer durationBetweenADsTimer;
    private TimerManager timerManager;

    void Start()
    {
        timerManager = GetComponent<TimerManager>();
        adDurationTimer = new Timer(adDuration, false, AdTimerCompleted);
        durationBetweenADsTimer = new Timer(durationBetweenADs, false, TimerBetweenADsCompleted);
        timerManager.RegisterTimer(adDurationTimer);
        durationBetweenADsTimer.Restart();
    }
    private void Update()
    {
        if(adButton.activeInHierarchy)
        {
            timerText.text = "" + adDurationTimer.RatioRemaining * adDuration;
        }
    }
    private void AdTimerCompleted(Timer timer)
    {
        adButton.SetActive(false);
        durationBetweenADsTimer.Restart();
    }

    private void TimerBetweenADsCompleted(Timer timer)
    {
        adButton.SetActive(true);
        GenerateLoot();
        adDurationTimer.Restart();
    }

    private void GenerateLoot()
    {
        item = dropItemsList[(int)Random.Range(0, dropItemsList.Length - 1)];
        dropImage.GetComponent<Image>().sprite = item.inventorySprite;
    }

    public void AdButtonClick()
    {
        // launch ad
        adsPlayed++;
    }
}
