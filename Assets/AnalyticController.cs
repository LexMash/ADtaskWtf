using UnityEngine;

public class AnalyticController : MonoBehaviour
{
    private void OnEnable()
    {
        AdController.Instance.AdShowed += AdShowed;
        AdController.Instance.AdShowComplete += AdShowComplete;
    }

    private void OnDisable()
    {
        AdController.Instance.AdShowed -= AdShowed;
        AdController.Instance.AdShowComplete -= AdShowComplete;
    }

    private void AdShowed() 
    {
        AnalyticServices.ButtonPressed();
    }
    private void AdShowComplete()
    {
        AnalyticServices.AdShowComplete();
    }
}
