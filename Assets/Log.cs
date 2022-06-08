using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] private Text _logText;

    private void OnEnable()
    {
        AdController.Instance.OnLoggedEvent += ShowLogMessage;
    }

    private void OnDisable()
    {
        AdController.Instance.OnLoggedEvent -= ShowLogMessage;
    }

    public void ShowLogMessage(string message)
    {
        _logText.text += message;
    }
}
