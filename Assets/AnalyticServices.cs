public static class AnalyticServices
{
    private static UnityAnalyticService _unityAS = new UnityAnalyticService();

    public static void ButtonPressed()
    {
        _unityAS.ButtonPressed();
    }

    public static void AdShowComplete()
    {
        _unityAS.AdShowComplete();
    }
}
