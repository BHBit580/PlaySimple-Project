using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFailed : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO levelFailedEvent;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button tryAgainButton;

    private void Start()
    {
        canvas.enabled = false;
        levelFailedEvent.RegisterListener(ShowLevelFailed);
        tryAgainButton.onClick.AddListener(OnClickTryAgain);
    }

    private void ShowLevelFailed()
    {
        canvas.enabled = true;
    }

    private void OnClickTryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        levelFailedEvent.UnregisterListener(ShowLevelFailed);
    }
}
