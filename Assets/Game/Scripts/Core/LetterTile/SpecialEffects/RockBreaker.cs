using System.Collections.Generic;

public class RockBreaker
{
    public void CheckForRockBreaking(List<LetterTile> usedTiles, GridManager gridManager)
    {
        HashSet<LetterTile> rocksToBreak = new HashSet<LetterTile>();
        
        foreach (var tile in usedTiles)
        {
            // Check all 8 adjacent positions
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Skip the tile itself
                    
                    int newRow = tile.Coordinate.x + dx;
                    int newCol = tile.Coordinate.y + dy;
                    
                    LetterTile adjacentTile = gridManager.GetTileAt(newRow, newCol);
                    if (adjacentTile != null && adjacentTile.IsBlocked)
                    {
                        rocksToBreak.Add(adjacentTile);
                    }
                }
            }
        }
        
        // Break all found rocks
        foreach (var rock in rocksToBreak)
        {
            BreakRock(rock);
        }
    }
    
    private void BreakRock(LetterTile rockTile)
    {
        rockTile.SetTileType(TileType.Normal); 
    }
}