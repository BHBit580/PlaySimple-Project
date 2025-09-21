using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject letterTilePrefab;
 
    [Header("Grid Configuration")]
    [SerializeField] private float gridStartX = 0f;
    [SerializeField] private float gridStartY = 0f;
    [SerializeField] private float gridXSpacing = 100f;
    [SerializeField] private float gridYSpacing = 100f;

    [Header("Animation Settings")]
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private float dropDuration = 0.8f;
    [SerializeField] private float spawnOffsetY = 300f;
    [SerializeField] private float delayBetweenSpawns = 0.05f;
    [SerializeField] private Ease dropEase = Ease.OutBounce;
    
    private int _rowCount;
    private int _columnCount;
    private LetterTile[,] _gridTiles;
    private char[,] _charMatrix;
    private bool _isProcessingTiles;
    private LevelData _levelData;
    
    public void InitializeGrid(char[,] charMatrix, LevelData levelData = null)               
    {
        _charMatrix = charMatrix;
        _rowCount = charMatrix.GetLength(0);
        _columnCount = charMatrix.GetLength(1);
        _gridTiles = new LetterTile[_rowCount, _columnCount];
        _levelData = levelData;
        MakeGrid();
    }

    private void MakeGrid()
    {
        DestroyAllPreviousChildrenOfGrid();
        
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _columnCount; col++)
            {
                CreateTileAtPosition(row, col);
            }
        }
    }

    private LetterTile CreateTileAtPosition(int row, int col, bool animateFromTop = false, float delay = 0f)
    {
        GameObject tileObj = Instantiate(letterTilePrefab, transform);
        LetterTile tile = tileObj.GetComponent<LetterTile>();
        RectTransform tileRect = tileObj.GetComponent<RectTransform>();

        int siblingIndex = row * _columnCount + col;
        tileObj.transform.SetSiblingIndex(siblingIndex);

        Vector2 targetPosition = GetGridPosition(row, col); // Now this is correct

        if (animateFromTop)
        {
            Vector2 startPosition = new Vector2(targetPosition.x, gridStartY + spawnOffsetY);
            tileRect.anchoredPosition = startPosition;
        
            tileRect.DOAnchorPos(targetPosition, dropDuration)
                .SetEase(dropEase)
                .SetDelay(delay);
        }
        else
        {
            tileRect.anchoredPosition = targetPosition;
        }

        tile.SetPosition(row, col);
        tile.SetLetter(_charMatrix[row, col]);
        if (_levelData != null)
        {
            tile.SetTileType(_levelData.GetTileTypeAt(row, col));
        }
        
        tileObj.name = $"letter_tile_{tile.Letter}";
    
        _gridTiles[row, col] = tile;
    
        return tile;
    }

    private Vector2 GetGridPosition(int row, int col)
    {
        float xPos = gridStartX + (col * gridXSpacing);  
        float yPos = gridStartY - (row * gridYSpacing);  
        return new Vector2(xPos, yPos);
    }

    public LetterTile GetTileAt(int row, int col)
    {
        if (row >= 0 && row < _rowCount && col >= 0 && col < _columnCount)
        {
            return _gridTiles[row, col];
        }
        return null;
    }

    #region EndlessModeAnimations
    
    public void DestroySelectedLetterTilesAndSpawnNew(List<LetterTile> tilesToRemove, char[,] newCharMatrix)
    {
        if (tilesToRemove == null || tilesToRemove.Count == 0) return;
        if (_isProcessingTiles) return;
        _charMatrix = newCharMatrix;
        
        StartCoroutine(ReplaceSelectedTiles(tilesToRemove));
    }
    
    private IEnumerator ReplaceSelectedTiles(List<LetterTile> tilesToRemove)
    {
        _isProcessingTiles = true;
        
        List<Vector2Int> positionsToRefill = new List<Vector2Int>();
        
        foreach (var tile in tilesToRemove)
        {
            if (tile != null)
            {
                positionsToRefill.Add(tile.Coordinate);
                tile.FadeOutAndDestroyItself();
                _gridTiles[tile.Coordinate.x, tile.Coordinate.y] = null;
            }
        }
        
        yield return new WaitForSeconds(fadeOutDuration);
        
        int delayIndex = 0;
        foreach (var pos in positionsToRefill)
        {
            float delay = delayIndex * delayBetweenSpawns;
            CreateTileAtPosition(pos.x, pos.y, true, delay);
            delayIndex++;
        }
        
        yield return new WaitForSeconds(dropDuration + (positionsToRefill.Count * delayBetweenSpawns));
        
        ReorganizeChildOrder();
        
        _isProcessingTiles = false;
    }
    
    
    #endregion

    public List<LetterTile> GetAllTiles()
    {
        List<LetterTile> allTiles = new List<LetterTile>();
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _columnCount; col++)
            {
                if (_gridTiles[row, col] != null)
                {
                    allTiles.Add(_gridTiles[row, col]);
                }
            }
        }
        return allTiles;
    }
    
    public bool IsProcessingTiles()
    {
        return _isProcessingTiles;
    }
    
    private void ReorganizeChildOrder()
    {
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _columnCount; col++)
            {
                if (_gridTiles[row, col] != null)
                {
                    int correctSiblingIndex = row * _columnCount + col;
                    _gridTiles[row, col].transform.SetSiblingIndex(correctSiblingIndex);
                }
            }
        }
    }
    
    private void DestroyAllPreviousChildrenOfGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}