using UnityEngine;
using UnityEngine.UI;

public class WidgetAdsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _loadingIndicator;
    [SerializeField] private Fireworks _fireworks;

    private void Awake()
    {
        InteractionOff();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);

        AdController.Instance.OnAdShowedEvent += OnAdShowedEvent;
        AdController.Instance.OnAdLoadFinishedEvent += OnAdLoadFinishedEvent;
        AdController.Instance.OnAdLoadStartedEvent += OnAdLoadStartedEvent;
    }

    private void OnDisable()
    {
        _button.onClick.AddListener(OnClick);
        
        AdController.Instance.OnAdShowedEvent -= OnAdShowedEvent;
        AdController.Instance.OnAdLoadFinishedEvent -= OnAdLoadFinishedEvent;
        AdController.Instance.OnAdLoadStartedEvent -= OnAdLoadStartedEvent;
    }

    private void InteractionOn()
    {
        _button.interactable = true;
    }

    private void InteractionOff()
    {
        _button.interactable = false;
    }

    private void OnClick()
    {
        AdController.Instance.ShowAd();
    }
    
    private void OnAdLoadStartedEvent()
    {
        _loadingIndicator.SetActive(true);
    }
    
    private void OnAdLoadFinishedEvent()
    {
        InteractionOn();
        _loadingIndicator.SetActive(false);
    }

    private void OnAdShowedEvent()
    {
        InteractionOff();
        _fireworks.Play();
    }
}
