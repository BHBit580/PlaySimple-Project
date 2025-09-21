using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField] private ChallengeHandler challengeHandler;
    [SerializeField] private GridManager gridManager;
    
    public LevelData CurrentLevelData { get; private set; }
    
    private void Start()
    {
        DeployRandomLevel();
    }

    private void DeployRandomLevel()
    {
        var levels = DataLoader.Instance.LoadLevels();
        CurrentLevelData = levels[Random.Range(0 , levels.Length)];
        
        challengeHandler.InitializeChallenge(CurrentLevelData);
        gridManager.InitializeGrid(CurrentLevelData.GetBoard() , CurrentLevelData);
    }
}