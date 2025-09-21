using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
    }

    private void OnClickMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
