using System;
using System.Collections.Generic;
using UnityEngine;

// ideally this is bad but i had to do it cuz of time constrain 
// best way is to make an dedicated ability management system there could be n number of abilities
public class TileAbilityManager : MonoBehaviour
{
    [SerializeField] private DataEventChannelSO onNewWordFormedEvent;
    [SerializeField] private VoidEventChannelSO bugsCollectedEvent;
    [SerializeField] private GridManager gridManager;
    private readonly RockBreaker _rockBreaker = new();
    
    private void Start()
    {
        onNewWordFormedEvent.RegisterListener(OnNewWordFormed);
    }
    
    private void OnNewWordFormed(object data)                    
    {
        if (data is (List<LetterTile> selectedTiles, string word))
        {
            ProcessBugCollection(selectedTiles);
            ProcessRockCollection(selectedTiles);
        }
    }

    private void ProcessRockCollection(List<LetterTile> selectedTiles)
    {
        _rockBreaker.CheckForRockBreaking(selectedTiles, gridManager);
    }
    
    private void ProcessBugCollection(List<LetterTile> selectedTiles)
    {
        foreach(var tile in selectedTiles)
        {
            if(tile.CurrentTileType == TileType.Bug)
            {
                tile.SetTileType(TileType.Normal);
                bugsCollectedEvent?.RaiseEvent();
            }
        }
    }
    
    private void OnDestroy()
    {
        onNewWordFormedEvent.UnregisterListener(OnNewWordFormed);
    }
}