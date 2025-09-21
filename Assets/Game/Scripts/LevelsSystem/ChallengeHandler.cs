using TMPro;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO levelFailedEvent;
    [SerializeField] private VoidEventChannelSO levelCompletedEvent;
    [SerializeField] private TextMeshProUGUI challengeText;
    [SerializeField] private ScoreManager scoreManager;
    
    private LevelChallenge _currentChallenge;
    private bool _challengeEnded = false;
    
    public void InitializeChallenge(LevelData levelData)
    {
        _currentChallenge = new LevelChallenge
        {
            challengeType = levelData.GetChallengeTypeOfThisLevel(),
            timeLimit = levelData.timeSec
        };

        switch (_currentChallenge.challengeType)
        {
            case ChallengeType.MakeXWords:
            case ChallengeType.MakeXWordsInYTime:
                _currentChallenge.targetValue = levelData.wordCount > 0 ? levelData.wordCount : 5;
                break;
                
            case ChallengeType.ReachXScoreInYTime:
                _currentChallenge.targetValue = levelData.totalScore;
                break;
        }
        
        _currentChallenge.Reset();
    }
    
    private void Update()
    {
        if (_challengeEnded || _currentChallenge == null) return;
        
        _currentChallenge.UpdateProgress(scoreManager.WordsFormed, scoreManager.TotalScore, Time.deltaTime);

        challengeText.text = _currentChallenge.GetDescription();
        
        if (_currentChallenge.isCompleted)
        {
            levelCompletedEvent?.RaiseEvent();
            Debug.Log("Yooo hoo you did the challenge");
            _challengeEnded = true;
        }
        else if (_currentChallenge.hasFailed)
        {
            levelFailedEvent?.RaiseEvent();
            Debug.Log("You failed that");
            _challengeEnded = true;
        }
    }
}
