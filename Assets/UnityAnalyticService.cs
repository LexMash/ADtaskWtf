using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticService
{
    public void ButtonPressed()
    {
        AnalyticsResult _result = Analytics.CustomEvent("ADs button pressed");
        /*Debug.Log("ADs button pressed " + _result);
        Debug.Log(Analytics.IsCustomEventEnabled("ADs button pressed"));*/
    }

    public void AdShowComplete()
    {
        AnalyticsResult _result = Analytics.CustomEvent("ADs shown");
        /*Debug.Log("ADs shown " + _result);
        Debug.Log(Analytics.IsCustomEventEnabled("ADs shown"));*/
    }
}
