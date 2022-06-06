using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AdController _adController;

    private void OnEnable()
    {
        _adController.OnAdLoadFinishedEvent += InteractionOn;
        _adController.OnDestroedEvent += RemoveListeners;
        _adController.OnAdShowedEvent += InteractionOff;
    }

    private void OnDisable()
    {
        _adController.OnAdLoadFinishedEvent -= InteractionOn;
        _adController.OnDestroedEvent -= RemoveListeners;
        _adController.OnAdShowedEvent -= InteractionOff;
    }

    private void InteractionOn()
    {
        _button.interactable = true;
        _button.onClick.AddListener(_adController.ShowAd);
    }

    private void InteractionOff()
    {
        _button.interactable = false;
    }

    private void RemoveListeners()
    {
        _button.onClick.RemoveAllListeners();
    }
}
