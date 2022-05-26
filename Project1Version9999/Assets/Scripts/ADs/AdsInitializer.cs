using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;

    [SerializeField] private Button rewardedAdsBtn;

    [SerializeField]
    private ADS_spawn adsSpawn;
    private void Awake()
    {
        rewardedAdsBtn.interactable = false;
        rewardedAdsBtn.onClick.AddListener(ShowRewardedAds);
        if (Advertisement.isInitialized)
        {
           // Debug.Log("Advertisement is Initialized");
            LoadRewardedAd();
        }
        else
        {
            InitializeAds();
        }
    }

    public void ShowRewardedAds()
    {
        Advertisement.Show("Rewarded_Android",this);
    }
    
    
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
       Debug.Log("Unity Ads initialization complete.");
        //LoadInerstitialAd();
        LoadRewardedAd();
        //LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
      //  Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        rewardedAdsBtn.onClick.RemoveAllListeners();
        if(adsSpawn != null)
            rewardedAdsBtn.onClick.AddListener(adsSpawn.Spawn);
        rewardedAdsBtn.interactable = true;
    }

    public void LoadInerstitialAd()
    {
        Advertisement.Load("Interstitial_Android", this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load("Rewarded_Android", this);
        
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
      //  Debug.Log("OnUnityAdsAdLoaded");
        if (placementId.Equals("Rewarded_Android"))
        {
            rewardedAdsBtn.interactable = true;
        }
        //Advertisement.Show(placementId,this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
      //  Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
       // Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //Debug.Log("OnUnityAdsShowStart");
        Time.timeScale = 0;
        //Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
       // Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //Debug.Log("OnUnityAdsShowComplete "+showCompletionState);
        if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("rewared Player");
            
            //
            if(adsSpawn != null)
                adsSpawn.Spawn();
            //
            
            rewardedAdsBtn.interactable = false;
            LoadRewardedAd();
        }
        Time.timeScale = 1;
        //Advertisement.Banner.Show("Banner_Android");
    }



    /*public void LoadBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load("Banner_Android",
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show("Banner_Android");
    }

    void OnBannerError(string message)
    {

    }*/

}