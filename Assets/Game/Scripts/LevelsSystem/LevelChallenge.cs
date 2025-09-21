using System;
using UnityEngine;

public enum ChallengeType
{
    MakeXWords,      
    ReachXScoreInYTime,    
    MakeXWordsInYTime     
}

[Serializable]
public class LevelChallenge
{
    public ChallengeType challengeType;
    public int targetValue;     
    public float timeLimit;     // time limit in seconds (0 = no limit)
    
    [HideInInspector] public int currentProgress;
    [HideInInspector] public float elapsedTime;
    [HideInInspector] public bool isCompleted;
    [HideInInspector] public bool hasFailed;
    
    public void Reset()
    {
        currentProgress = 0;
        elapsedTime = 0;
        isCompleted = false;
        hasFailed = false;
    }
    
    public void UpdateProgress(int wordsFormed, int totalScore, float deltaTime)
    {
        if (isCompleted || hasFailed) return;
        
        // Update time
        if (timeLimit > 0)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= timeLimit)
            {
                hasFailed = true;
                return;
            }
        }
        
        // Update progress based on challenge type
        switch (challengeType)
        {
            case ChallengeType.MakeXWords:
                currentProgress = wordsFormed;
                break;
                
            case ChallengeType.ReachXScoreInYTime:
                currentProgress = totalScore;
                break;
                
            case ChallengeType.MakeXWordsInYTime:
                currentProgress = wordsFormed;
                break;
        }
        
        // Check completion
        if (currentProgress >= targetValue)
        {
            isCompleted = true;
        }
    }
    
    public string GetDescription()
    {
        switch (challengeType)
        {
            case ChallengeType.MakeXWords:
                return $"Make {targetValue} words";
                
            case ChallengeType.ReachXScoreInYTime:
                int timeLeft = Mathf.Max(0, Mathf.CeilToInt(timeLimit - elapsedTime));
                return $"Score {targetValue} points in {timeLeft}s ({currentProgress}/{targetValue})";
                
            case ChallengeType.MakeXWordsInYTime:
                int timeLeft2 = Mathf.Max(0, Mathf.CeilToInt(timeLimit - elapsedTime));
                return $"Make {targetValue} words in {timeLeft2}s ({currentProgress}/{targetValue})";
                
            default:
                return "Unknown challenge";
        }
    }
    
    public float GetProgress()
    {
        if (hasFailed) return 0f;
        return targetValue > 0 ? (float)currentProgress / targetValue : 0f;
    }
}