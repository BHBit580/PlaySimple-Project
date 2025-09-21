using System;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviourSingleton<DataLoader>
{
    [SerializeField] private TextAsset levelsData;
    [SerializeField] private TextAsset randomWordsTxtFile;

    public HashSet<string> PossibleWordsSet = new HashSet<string>();

    private void Start()
    {
        PossibleWordsSet = GetSetOfAllRandomNames();
    }

    public HashSet<string> GetSetOfAllRandomNames()
    {
        var validWords = new HashSet<string>();
        
        if (randomWordsTxtFile != null)
        {
            string[] words = randomWordsTxtFile.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string word in words)
            {
                string cleanWord = word.Trim().ToLower();
                if (!string.IsNullOrEmpty(cleanWord))
                {
                    validWords.Add(cleanWord);
                }
            }
        }
        else
        {
            Debug.LogError("No word list file assigned! Please assign a TextAsset in the inspector.");
        }

        return validWords;
    }

    public LevelData[] LoadLevels()
    {
        LevelData[] allLevels;
        if (levelsData != null)
        {
            try
            {
                string jsonText = levelsData.text;
                LevelsContainer container = JsonUtility.FromJson<LevelsContainer>(jsonText);
                allLevels = container.data;
                
                Debug.Log($"Loaded {allLevels.Length} levels");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse levels JSON: {e.Message}");
                allLevels = new LevelData[0];
            }
        }
        else
        {
            Debug.LogError("No levels data file assigned!");
            allLevels = new LevelData[0];
        }

        return allLevels;
    }
}