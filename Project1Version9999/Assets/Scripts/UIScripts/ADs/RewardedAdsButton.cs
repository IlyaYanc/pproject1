using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button showAdButton;
    [SerializeField] private string androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string iOSAdUnitId = "Rewarded_iOS";
    private string adUnitId  = null;

    private void Awake()
    {
#if UNITY_IOS
        adUnitId = iOSAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
        
        showAdButton.interactable = false;
    }

    private void Start()
    {
        StartCoroutine(LoadRewardedAds());
        //LoadAd();
    }

    private IEnumerator LoadRewardedAds()
    {
        yield return new WaitForSeconds(0.1f);
        LoadAd();
    }
    private void LoadAd()
    {
        Debug.Log("Loading Ad: " +adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        showAdButton.interactable = false;
        Debug.Log("Showing Ad: " +adUnitId);
        Advertisement.Show(adUnitId, this);
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (adUnitId.Equals(placementId))
        {
            showAdButton.onClick.AddListener(ShowAd);
            showAdButton.interactable = true;
            Debug.Log("Loaded Ad: " +adUnitId);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("ShowStart");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("ShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

            // Load another ad:
            Advertisement.Load(adUnitId, this);
        }
    }

    private void OnDestroy()
    {
        showAdButton.onClick.RemoveAllListeners();
    }
}
