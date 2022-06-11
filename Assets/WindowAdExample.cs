using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class WindowAdExample : MonoBehaviour
{
	[SerializeField] private WidgetAdsButton _buttonRunWidgetAd;
	[SerializeField] private Text _textLog;
	[SerializeField] private RotatingObject _rotatingObject;
	[SerializeField] private Fireworks _fireworks;

	private string _adUnitId;

    #region MONO
    private void Start()
    {
		_adUnitId = AdController.Instance.ADUnitID;

		_rotatingObject.gameObject.SetActive(false);
	}

    private void OnEnable()
	{
		AdController.Instance.AdLoadStarted += AdLoadStarted;
		AdController.Instance.AdShowComplete += AdShowComplete;
        AdController.Instance.AdLoadFinished += AdLoadFinished;
        AdController.Instance.AdFailedToLoad += AdFailedToLoad;
        AdController.Instance.AdShowFailure += AdShowFailure;
	}

    private void OnDisable()
	{
		AdController.Instance.AdLoadStarted -= AdLoadStarted;
		AdController.Instance.AdShowComplete -= AdShowComplete;
		AdController.Instance.AdLoadFinished -= AdLoadFinished;
		AdController.Instance.AdFailedToLoad -= AdFailedToLoad;
		AdController.Instance.AdShowFailure -= AdShowFailure;
	}

    #endregion

    private void AdLoadStarted()
	{
		_textLog.text += $"Loading Ad:  + {_adUnitId}\n";

		_rotatingObject.gameObject.SetActive(true);

		_buttonRunWidgetAd.AdLoadStarted();
	}

	private void AdLoadFinished()
	{
		_textLog.text += $"Ad Loaded:  + {_adUnitId}\n";

		_rotatingObject.gameObject.SetActive(false);

		_buttonRunWidgetAd.AdLoadFinished();
	}

	private void AdShowComplete()
	{
		_textLog.text += "Unity Ads Rewarded Ad Completed\n";

        if (!_fireworks.gameObject.activeSelf)
        {
			_fireworks.gameObject.SetActive(true);
		}

		_buttonRunWidgetAd.AdShowed();

		_fireworks.Play();
	}

	private void AdShowFailure(UnityAdsShowError error, string message)
	{
		_textLog.text += $"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}\n";
	}

	private void AdFailedToLoad(UnityAdsLoadError error, string message)
	{
		_textLog.text += $"Error loading Ad Unit {_adUnitId}: {error.ToString()} - {message}\n";
	}
}
