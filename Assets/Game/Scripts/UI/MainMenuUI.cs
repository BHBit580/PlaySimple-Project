using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button endlessModeButton;
    [SerializeField] private Button levelModeButton;

    private void Start()
    {
        endlessModeButton.onClick.AddListener(OnClickEndlessModeButton);
        levelModeButton.onClick.AddListener(OnClickLevelModeButton);
    }

    private void OnClickEndlessModeButton()
    {
        SceneManager.LoadScene(1);
    }

    private void OnClickLevelModeButton()
    {
        SceneManager.LoadScene(2);
    }
}
