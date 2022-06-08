using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    private const string PrefabPath = "AdsController";
    
    [SerializeField] private string _gameId = "4778209";
    [SerializeField] private string _adUnitId = "Rewarded_Android";
    [SerializeField] private bool _testMode = true;

    public static AdController Instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<AdController>(PrefabPath);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private static AdController _instance;
    
    public event Action OnAdLoadStartedEvent;
    public event Action OnAdLoadFinishedEvent;
    public event Action OnAdShowedEvent;
    public event Action OnAdShowCompleteEvent;
    public event Action OnDestroyedEvent;
    public event Action<string> OnLoggedEvent;

    private bool _isAdLoading;
    private bool _isAdShowing;
    
    private void Awake()
    {
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    private void OnEnable()
    {
        OnAdShowedEvent?.Invoke();

        if (Advertisement.isInitialized)
        {
            LoadAd();
        }
    }

    public void LoadAd()
    {
        if (!_isAdLoading)
        {
            _isAdLoading = true;
            
            Advertisement.Load(_adUnitId, this);

            OnLoggedEvent?.Invoke($"Loading Ad:  + {_adUnitId}\n");
            OnAdLoadStartedEvent?.Invoke(); 
        }
    }

    public void ShowAd()
    {
        if (!_isAdShowing)
        {
            OnAdShowedEvent?.Invoke();

            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            OnLoggedEvent?.Invoke("Unity Ads Rewarded Ad Completed\n");

            OnAdShowCompleteEvent?.Invoke();

            Advertisement.Load(_adUnitId, this);
        }

        _isAdShowing = false;
    }
    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        _isAdLoading = false;
        
        OnLoggedEvent?.Invoke($"Ad Loaded:  + {adUnitId}\n");
        
        if (adUnitId.Equals(_adUnitId))
        {
            OnAdLoadFinishedEvent?.Invoke();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        _isAdLoading = false;
        
        OnLoggedEvent?.Invoke($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}\n");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        _isAdShowing = false;
        
        OnLoggedEvent?.Invoke($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}\n");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        OnDestroyedEvent?.Invoke();
    }

    public void OnInitializationComplete()
    {
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("AD Initialization failed");
    }
}
