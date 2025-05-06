using System;
using UnityEngine;

public class TileRow : MonoBehaviour
{
    public TileCell[] cells { get; private set; }

    private void Awake()
    {
        cells = GetComponentsInChildren<TileCell>();
        
    }
    public TileCell GetBiggestTileInRow()
    {
        TileCell tilecell = cells[0];
        foreach (TileCell cell in cells)
        {
            if (cell.tile != null && cell.tile.state != null)
            {
                if (tilecell == null || 
                    tilecell.tile == null || tilecell.tile.state == null || 
                    cell.tile.state.number > tilecell.tile.state.number)
                {
                    tilecell = cell;
                }
            }
        }
        return tilecell;
    }

    public void ResetTileCell()
    {
        TileCell tilecell = GetBiggestTileInRow();
        foreach (TileCell cell in cells) cell.IsBiggestInRow = false;
        tilecell.IsBiggestInRow = true;
    }
    private void Update()
    {
        ResetTileCell();
    }
}
