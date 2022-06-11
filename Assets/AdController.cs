using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    private const string PrefabPath = "AdsController";
    
    [SerializeField] private string _gameId = "4778209";
    [SerializeField] private string _adUnitId = "Rewarded_Android";
    [SerializeField] private bool _testMode = true;
    [SerializeField] private GameObject _adControllerPrefab;

    public string ADUnitID => _adUnitId;

    private static AdController _instance;
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

    public event Action AdLoadStarted;
    public event Action AdLoadFinished;
    public event Action AdShowed;
    public event Action AdShowComplete;
    public event Action Destroyed;

    public delegate void AdsLoadEvent(UnityAdsLoadError error, string message);
    public event AdsLoadEvent AdFailedToLoad;

    public delegate void AdsShowEvent(UnityAdsShowError error, string message);
    public event AdsShowEvent AdShowFailure;

    private bool _isAdLoading;
    private bool _isAdShowing;

    private void Awake()
    {
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    private void OnEnable()
    {
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

            AdLoadStarted?.Invoke(); 
        }
    }

    public void ShowAd()
    {
        if (!_isAdShowing)
        {
            AdShowed?.Invoke();

            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            AdShowComplete?.Invoke();

            Advertisement.Load(_adUnitId, this);
        }

        _isAdShowing = false;
    }
    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        _isAdLoading = false;      
        
        if (adUnitId.Equals(_adUnitId))
        {
            AdLoadFinished?.Invoke();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        _isAdLoading = false;

        AdFailedToLoad?.Invoke(error, message);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        _isAdShowing = false;

        AdShowFailure?.Invoke(error, message);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        Destroyed?.Invoke();
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
