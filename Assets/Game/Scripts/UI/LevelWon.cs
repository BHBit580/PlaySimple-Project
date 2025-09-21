using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelWon : MonoBehaviour
{ 
    [SerializeField] private VoidEventChannelSO levelWonEvent;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button goToMainMenuButton;
    
    private void Start()
    {
        canvas.enabled = false;
        levelWonEvent.RegisterListener(ShowLevelWon);
        playAgainButton.onClick.AddListener(OnClickPlayAgain);
        goToMainMenuButton.onClick.AddListener(OnClickMainMenu);
    }

    private void ShowLevelWon()
    {
        canvas.enabled = true;
    }

    private void OnClickPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    private void OnDestroy()
    {
        levelWonEvent.UnregisterListener(ShowLevelWon);
    }
}
