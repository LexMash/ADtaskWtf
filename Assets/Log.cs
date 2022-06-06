using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] private Text _logText;
    [SerializeField] private AdController _adController;

    private void OnEnable()
    {
        _adController.OnLoggedEvent += ShowLogMessage;
    }

    private void OnDisable()
    {
        _adController.OnLoggedEvent -= ShowLogMessage;
    }

    public void ShowLogMessage(string message)
    {
        _logText.text += message;
    }
}
