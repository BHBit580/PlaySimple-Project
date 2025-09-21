using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class TileSelectionController : MonoBehaviourSingleton<TileSelectionController>
{
    [SerializeField] private DataEventChannelSO onNewWordFormedEvent;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TMP_Text alreadyPickedText;
    [SerializeField] private TMP_Text currentWordText;
    [SerializeField] private float lineWidth = 5f;
    
    private List<LetterTile> _selectedTiles = new();
    private List<GameObject> _connectionLines = new();
    private HashSet<string> _alreadyFormedWords = new();
    private bool _isDragging = false;
    private string _currentWord = "";
    
    private void Update()
    {
        if (_isDragging && !Input.GetMouseButton(0))
        {
            EndSelection();
        }
    }
    
    public void StartSelection(LetterTile tile)
    {
        if (tile == null || _isDragging) return;
        
        ClearSelection();
        _isDragging = true;
        AddTileToSelection(tile);
    }
    
    public void ContinueSelection(LetterTile tile)
    {
        if (tile.IsBlocked) return;
        
        if (!_isDragging || tile == null) return;
        
        if (_selectedTiles.Contains(tile)) return;
        
        if (_selectedTiles.Count > 0)
        {
            LetterTile lastTile = _selectedTiles[_selectedTiles.Count - 1];
            if (IsAdjacent(lastTile, tile))
            {
                AddTileToSelection(tile);
            }
        }
    }
    
    public void EndSelection()
    {
        if (!_isDragging) return;
        
        _isDragging = false;
        
        if (_currentWord.Length >= 3)
        {
            ProcessWord(_currentWord);
            ClearSelection();
        }
        else
        {
            ClearSelection();
        }
    }
    
    private void ProcessWord(string word)
    {
        if (_alreadyFormedWords.Contains(word))
        {
            ShowAlreadyPickedAnimation(word);
            return;
        }
        
        if(!DataLoader.Instance.PossibleWordsSet.Contains(word)) return;
        
        _alreadyFormedWords.Add(word);
        onNewWordFormedEvent?.RaiseEvent((_selectedTiles, word));
    }
    
    private void ShowAlreadyPickedAnimation(string word)
    {
        // Kill any existing animations
        alreadyPickedText.transform.DOKill();
        
        // Set text and show
        alreadyPickedText.text = $"Already found: {word}";
        alreadyPickedText.gameObject.SetActive(true);
        
        // Shake animation followed by hide
        alreadyPickedText.transform.DOShakePosition(0.6f, 10)
            .OnComplete(() => 
            {
                alreadyPickedText.gameObject.SetActive(false);
            });
    }
    
    private void AddTileToSelection(LetterTile tile)
    {
        _selectedTiles.Add(tile);
        tile.SetSelected(true);
        _currentWord += tile.Letter;
        
        if (_selectedTiles.Count > 1)
        {
            CreateConnectionLine(_selectedTiles[_selectedTiles.Count - 2], tile);
        }
        
        UpdateCurrentWordDisplay();
    }
    
    private void CreateConnectionLine(LetterTile fromTile, LetterTile toTile)
    {
        GameObject lineObj = new GameObject("ConnectionLine");
        lineObj.transform.SetParent(transform, false);
            
        Image lineImage = lineObj.AddComponent<Image>();
        lineImage.color = Color.white;
        
        _connectionLines.Add(lineObj);
        
        RectTransform lineRect = lineObj.GetComponent<RectTransform>();
        
        Vector3 fromPos = fromTile.transform.position;
        Vector3 toPos = toTile.transform.position;
        
        lineRect.position = (fromPos + toPos) / 2f;
        
        float distance = Vector3.Distance(fromPos, toPos);
        Vector3 direction = (toPos - fromPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        lineRect.sizeDelta = new Vector2(distance, lineWidth);
        lineRect.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private bool IsAdjacent(LetterTile tile1, LetterTile tile2)
    {
        if (tile1 == null || tile2 == null) return false;
        
        int dx = Mathf.Abs(tile1.Coordinate.x - tile2.Coordinate.x);
        int dy = Mathf.Abs(tile1.Coordinate.y - tile2.Coordinate.y);
        
        return dx <= 1 && dy <= 1 && (dx != 0 || dy != 0);
    }
    
    private void ClearSelection()
    {
        foreach (LetterTile tile in _selectedTiles)
        {
            if (tile != null)
            {
                tile.SetSelected(false);
            }
        }
        
        foreach (GameObject line in _connectionLines)
        {
            if (line != null)
            {
                Destroy(line);
            }
        }
        
        _selectedTiles.Clear();
        _connectionLines.Clear();
        _currentWord = "";
        _isDragging = false;
        
        UpdateCurrentWordDisplay();
    }
    
    private void UpdateCurrentWordDisplay()
    {
        currentWordText.text = string.IsNullOrEmpty(_currentWord) ? "" : _currentWord;
    }
    
    public bool IsDragging()
    {
        return _isDragging;
    }
}