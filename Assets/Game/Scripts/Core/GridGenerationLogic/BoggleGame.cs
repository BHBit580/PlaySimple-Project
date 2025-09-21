using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// A very simple class which till take valid words and will return 2d char matrix that will be used to form grid
public class BoggleGame
{
    private char[,] board;
    private int rows;
    private int cols;
    private HashSet<string> validWords;
    private List<string> placedWords;

    private static readonly int[] rowDirections = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] colDirections = { -1, 0, 1, -1, 1, -1, 0, 1 };

    private static readonly char[] randomLetters = {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
    };

    public BoggleGame(int rows, int cols, HashSet<string> validWords)
    {
        this.rows = rows;
        this.cols = cols;
        this.board = new char[rows, cols];
        this.placedWords = new List<string>();
        this.validWords = validWords;
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
        List<string> wordsToPlace = validWords.ToList();
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
        for (int attempt = 0; attempt < 50; attempt++)                     // simple algorithm , i could have used more advance stuff but it takes time....
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
