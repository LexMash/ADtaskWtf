using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Action OnAdLoadStartedEvent;
    public Action OnAdLoadFinishedEvent;
    public Action OnAdShowedEvent;
    public Action OnAdShowCompleteEvent;
    public Action OnDestroedEvent;

    public Action<string> OnLoggedEvent;

    [SerializeField] private bool _testMode = true;

    private string _gameId = "4778209";
    private string _adUnitId = "Rewarded_Android";

    private void Awake()
    {
        Advertisement.Initialize(_gameId, _testMode);
    }

    private void OnEnable()
    {
        OnAdShowedEvent?.Invoke();

        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);

        OnLoggedEvent?.Invoke($"Loading Ad:  + {_adUnitId}\n");
        OnAdLoadStartedEvent?.Invoke();       
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        OnLoggedEvent?.Invoke($"Ad Loaded:  + {adUnitId}\n");
        

        if (adUnitId.Equals(_adUnitId))
        {
            OnAdLoadFinishedEvent?.Invoke();
        }
    }

    public void ShowAd()
    {
        OnAdShowedEvent?.Invoke();

        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            OnLoggedEvent?.Invoke("Unity Ads Rewarded Ad Completed\n");

            OnAdShowCompleteEvent?.Invoke();

            Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        OnLoggedEvent?.Invoke($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}\n");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        OnLoggedEvent?.Invoke($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}\n");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        OnDestroedEvent?.Invoke();
    }
}
