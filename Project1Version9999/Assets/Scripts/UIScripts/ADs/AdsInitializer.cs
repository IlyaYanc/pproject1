using UnityEngine;
using UnityEngine.Advertisements;
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iOSGameId;
    [SerializeField] private bool testMode;
    
    private string gameId;

    private void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ?iOSGameId : androidGameId;
        Advertisement.Initialize(gameId, testMode, true);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}