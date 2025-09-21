using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private DataEventChannelSO newWordFormedEvent;
    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private TMP_Text averageScorePerWord;
    
    public int WordsFormed { get; private set; }
    public int TotalScore  { get; private set; }
    
    private void Start()
    {
        newWordFormedEvent.RegisterListener(UpdateScore);
        currentScore.text = "Total Score - " + TotalScore;
    }

    private void UpdateScore(object data)
    {
        var wordData = ((List<LetterTile>, string))data;
        string wordFormed = wordData.Item2;
        
        TotalScore+= wordFormed.Length;
        WordsFormed++;
        currentScore.text = "Total Score - " + TotalScore;
        averageScorePerWord.text = "Average Score per word- " + TotalScore / WordsFormed;
    }
    
    private void OnDestroy()
    {
        newWordFormedEvent.UnregisterListener(UpdateScore);
    }
}
