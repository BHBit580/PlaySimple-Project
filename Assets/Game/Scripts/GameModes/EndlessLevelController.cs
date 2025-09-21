using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndlessLevelController : MonoBehaviour
{
    [SerializeField] private DataEventChannelSO newWordFormedEvent;
    [SerializeField] private GridManager gridManager;
    private BoggleGame boggleGame;
    private int rowCount = 4;
    private int columnCount = 4;

    private void Start()
    {
        MakeLevelFromBoggleAlgorithm();
        newWordFormedEvent.RegisterListener(OnNewWordFormed);
    }
    
    private void MakeLevelFromBoggleAlgorithm()
    {
        boggleGame = new BoggleGame(rowCount, columnCount, DataLoader.Instance.GetSetOfAllRandomNames());
        gridManager.InitializeGrid(boggleGame.GetBoard());
    }

    private void OnNewWordFormed(object data)
    {
        if (data is ValueTuple<List<LetterTile>, string> wordData)
        {
            List<LetterTile> selectedTiles = wordData.Item1;
            
            char[,] newGridMatrix = boggleGame.AddNewLettersToBoard(CreateBoardWithEmptyPositions(selectedTiles));
            
            gridManager.DestroySelectedLetterTilesAndSpawnNew(selectedTiles, newGridMatrix);
        }
    }
    
    private char[,] CreateBoardWithEmptyPositions(List<LetterTile> tilesToRemove)
    {
        List<LetterTile> allTiles = gridManager.GetAllTiles();
        char[,] boardCopy = new char[rowCount, columnCount];
        foreach (var tile in allTiles)
        {
            if (tile != null)
            {
                boardCopy[tile.Coordinate.x, tile.Coordinate.y] = tile.Letter;
            }
        }
        foreach (var tile in tilesToRemove)
        {
            if (tile != null)
            {
                boardCopy[tile.Coordinate.x, tile.Coordinate.y] = '?';
            }
        }
        
        return boardCopy;
    }
    
    private void OnDestroy()
    {
        newWordFormedEvent.UnregisterListener(OnNewWordFormed);
    }
}