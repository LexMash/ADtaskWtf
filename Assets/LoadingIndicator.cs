using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform _loadingImagePosition;
    [SerializeField] private AdController _adController;

    private Coroutine _loadingIndicator;

    private void OnEnable()
    {
        _adController.OnAdLoadStartedEvent += ActiovateLoadingIndicator;
        _adController.OnAdLoadFinishedEvent += DeactivateLoadingIndicator;
    }

    private void OnDisable()
    {
        _adController.OnAdLoadStartedEvent -= ActiovateLoadingIndicator;
        _adController.OnAdLoadFinishedEvent -= DeactivateLoadingIndicator;
    }

    private void ActiovateLoadingIndicator()
    {
        _loadingIndicator = StartCoroutine(Indicator());
    }

    private void DeactivateLoadingIndicator()
    {
        _loadingImagePosition.GetComponent<Image>().enabled = false;

        StopCoroutine(_loadingIndicator);
    }

    private IEnumerator Indicator()
    {
        var rotation = Quaternion.Euler(0f, 0f, 30f);

        var pause = new WaitForSeconds(0.2f);

        _loadingImagePosition.GetComponent<Image>().enabled = true;

        for (; ; )
        {
            _loadingImagePosition.localRotation *= rotation;
            yield return pause;
        }
    }
}
