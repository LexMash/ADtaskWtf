using UnityEngine.Analytics;

public class UnityAnalyticService
{
    public void ButtonPress()
    {
        AnalyticsResult _result = Analytics.CustomEvent("ADs button pressed");
    }

    public void AdShowComplete()
    {
        AnalyticsResult _result = Analytics.CustomEvent("ADs shown");
    }
}
