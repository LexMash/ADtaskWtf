using UnityEngine;
using UnityEngine.UI;

public class WidgetAdsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _loadingIndicator;

    #region MONO
    private void Awake()
    {
        InteractionOff();
    }
  
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }
    #endregion

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
    
    public void AdLoadStarted()
    {
        _loadingIndicator.SetActive(true);
    }
    
    public void AdLoadFinished()
    {
        InteractionOn();
        _loadingIndicator.SetActive(false);
    }

    public void AdShowed()
    {
        InteractionOff();        
    }
}
