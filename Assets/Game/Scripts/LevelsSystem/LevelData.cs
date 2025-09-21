using System;

// ============= DATA CLASSES MATCHING JSON STRUCTURE =============

[Serializable]
public class GridSize
{
    public int x;
    public int y;
}

[Serializable]
public class TileData
{
    public int tileType;
    public string letter;  
    public char Letter => !string.IsNullOrEmpty(letter) ? char.ToLower(letter[0]) : '\0';
}

[Serializable]
public class LevelData
{
    public int bugCount;
    public int wordCount;
    public int timeSec;
    public int totalScore;
    public GridSize gridSize;
    public TileData[] gridData;
    
    public char[,] GetBoard()
    {
        char[,] board = new char[gridSize.y, gridSize.x];
    
        for (int i = 0; i < gridData.Length; i++)
        {
            int row = i / gridSize.x;
            int col = i % gridSize.x;
            board[row, col] = gridData[i].Letter;  // Use the property instead
        }
    
        return board;
    }
    
    // Helper method to get tile type at specific position
    public TileType GetTileTypeAt(int row, int col)
    {
        int index = row * gridSize.x + col;
        if (index >= 0 && index < gridData.Length)
        {
            return (TileType)gridData[index].tileType;
        }
        return TileType.Normal; // Default normal tile
    }
    
    // Check if level has time limit
    public bool HasTimeLimit()
    {
        return timeSec > 0;
    }
    
    // Check if level has score requirement
    public bool HasScoreRequirement()
    {
        return totalScore > 0;
    }
    
    // Check if level has bug collection requirement
    public bool HasBugRequirement()
    {
        return bugCount > 0;
    }

    public ChallengeType GetChallengeTypeOfThisLevel()
    {
        if (HasTimeLimit())
        {
            if (HasScoreRequirement())
            {
                return ChallengeType.ReachXScoreInYTime;
            }
            else if (wordCount > 0)
            {
                return ChallengeType.MakeXWordsInYTime;
            }
            else
            {
                return ChallengeType.MakeXWordsInYTime;
            }
        }
        else
        {
            return ChallengeType.MakeXWords;
        }
    }
}

[Serializable]
public class LevelsContainer
{
    public LevelData[] data;
}