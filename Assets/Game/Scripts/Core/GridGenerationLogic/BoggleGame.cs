using System.Collections.Generic;
using UnityEngine;

// A very simple class which till take valid words and will return 2d char matrix that will be used to form grid
public class BoggleGame
{
    private char[,] board;
    private int rows;
    private int cols;
    private List<string> placedWords;

    private static readonly int[] rowDirections = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] colDirections = { -1, 0, 1, -1, 1, -1, 0, 1 };

    private static readonly char[] randomLetters = {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
    };
    
    // I will use common words other than all english words cuz the reason is it's easier for player to play with common words
    private static List<string> commonWords = new List<string>
    {
        "cat", "dog", "run", "sun", "fun", "big", "red", "hot", "cold", "time",
        "love", "book", "tree", "game", "play", "word", "girl", "boy", "man", "day",
        "way", "new", "old", "good", "bad", "come", "go", "see", "get", "make",
        "take", "give", "hand", "eye", "head", "face", "home", "work", "life", "water",
        "fire", "earth", "wind", "food", "eat", "drink", "walk", "talk", "think", "know",
        "feel", "look", "find", "help", "tell", "ask", "call", "turn", "move", "stop",
        "start", "end", "open", "close", "read", "write", "sing", "dance", "laugh", "cry",
        "happy", "sad", "angry", "calm", "fast", "slow", "high", "low", "long", "short",
        "wide", "thin", "light", "dark", "clean", "dirty", "full", "empty", "rich", "poor",
        "young", "strong", "weak", "safe", "danger", "peace", "war", "hope", "fear",
        "car", "house", "school", "phone", "money", "friend", "family", "mother", "father", "child",
        "apple", "green", "blue", "white", "black", "yellow", "orange", "purple", "brown", "pink",
        "chair", "table", "door", "window", "room", "bed", "wall", "floor", "roof", "garden",
        "bird", "fish", "horse", "cow", "pig", "sheep", "mouse", "bear", "lion", "tiger",
        "rain", "snow", "cloud", "star", "moon", "sky", "sea", "lake", "river", "mountain",
        "morning", "night", "week", "month", "year", "hour", "minute", "today", "tomorrow", "yesterday",
        "small", "large", "easy", "hard", "soft", "rough", "smooth", "sharp", "round", "square",
        "send", "put", "sit", "stand", "lie", "sleep", "wake", "jump", "fly", "swim",
        "cut", "break", "fix", "build", "pull", "push", "throw", "catch", "hit", "kick",
        "city", "town", "road", "street", "park", "shop", "store", "market", "church", "bridge"
    };

    public BoggleGame(int rows, int cols)                   
    {
        this.rows = rows;
        this.cols = cols;
        this.board = new char[rows, cols];
        this.placedWords = new List<string>();
        GenerateBoardWithWords();
    }

    public char[,] AddNewLettersToBoard(char[,] boardWithEmpty)                      //A very simple algo currently in future we can add like sensible names 
    {
        for (int i = 0; i < rows; i++)                                              
        {
            for (int j = 0; j <cols; j++)
            {
                if (boardWithEmpty[i, j] == '?')
                {
                    boardWithEmpty[i, j] = randomLetters[Random.Range(0, randomLetters.Length)];
                }
            }
        }
        
        return boardWithEmpty;
    }
    
    public char[,] GetBoard()
    {
        return board;
    }
    
    public void PrintBoard()
    {
        Debug.Log($"Board ({rows}x{cols}):");
        for (int i = 0; i < rows; i++)
        {
            string rowString = "";
            for (int j = 0; j < cols; j++)
            {
                rowString += board[i, j] + " ";
            }
            Debug.Log(rowString);
        }
    }

    public List<string> GetPlacedWords()
    {
        foreach (var word in placedWords)
        {
            Debug.Log(word);
        }
        
        return placedWords;
    }
    
    private void GenerateBoardWithWords()
    {
        InitializeEmptyBoard();
        List<string> wordsToPlace = commonWords;
        ShuffleList(wordsToPlace);
        foreach (string word in wordsToPlace)
        {
            TryPlaceWord(word.ToLower());
        }
        FillEmptyCells();
    }

    private void InitializeEmptyBoard()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board[i, j] = '?';
            }
        }
    }

    private bool TryPlaceWord(string word)
    {
        for (int attempt = 0; attempt < 50; attempt++)                     // brute force but simple algorithm , i could have used more advance stuff but it takes time....
        {
            int startRow = Random.Range(0, rows);
            int startCol = Random.Range(0, cols);
            int direction = Random.Range(0, 8);
            if (CanPlaceWord(word, startRow, startCol, direction))
            {
                PlaceWord(word, startRow, startCol, direction);
                placedWords.Add(word);
                return true;
            }
        }
        return false;
    }

    private bool CanPlaceWord(string word, int startRow, int startCol, int direction)
    {
        int currentRow = startRow;
        int currentCol = startCol;
        for (int i = 0; i < word.Length; i++)
        {
            if (currentRow < 0 || currentRow >= rows ||
                currentCol < 0 || currentCol >= cols)
            {
                return false;
            }
            char cellLetter = board[currentRow, currentCol];
            char wordLetter = word[i];
            if (cellLetter != '?' && cellLetter != wordLetter)
            {
                return false;
            }
            currentRow += rowDirections[direction];
            currentCol += colDirections[direction];
        }
        return true;
    }

    private void PlaceWord(string word, int startRow, int startCol, int direction)
    {
        int currentRow = startRow;
        int currentCol = startCol;
        for (int i = 0; i < word.Length; i++)
        {
            board[currentRow, currentCol] = word[i];
            currentRow += rowDirections[direction];
            currentCol += colDirections[direction];
        }
    }

    private void FillEmptyCells()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (board[i, j] == '?')
                {
                    char randomLetter = randomLetters[Random.Range(0, randomLetters.Length)];
                    board[i, j] = randomLetter;
                }
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}