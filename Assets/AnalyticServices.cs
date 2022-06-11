public static class AnalyticServices
{
    private static UnityAnalyticService _unityAS = new UnityAnalyticService();

    public static void ButtonPressed()
    {
        _unityAS.ButtonPress();
    }

    public static void AdShowComplete()
    {
        _unityAS.AdShowComplete();
    }
}
