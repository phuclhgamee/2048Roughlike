using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }
    
    public TileCell[][] cellColumns { get; private set; }
    
    public int Size => cells.Length;
    public int Height => rows.Length;
    public int Width => Size / Height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();

        for (int i = 0; i < cells.Length; i++) {
            cells[i].coordinates = new Vector2Int(i % Width, i / Width);
        }
        
        cellColumns = new TileCell[Width][];

        for (int x = 0; x < Width; x++)
        {
            cellColumns[x] = new TileCell[Height];
        }

        for (int i = 0; i < cells.Length; i++)
        {
            TileCell cell = cells[i];
            int x = cell.coordinates.x;
            int y = cell.coordinates.y;
            cellColumns[x][y] = cell;
        }

    }

    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height) {
            return rows[y].cells[x];
        } else {
            return null;
        }
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].Occupied)
        {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            // all cells are occupied
            if (index == startingIndex) {
                return null;
            }
        }

        return cells[index];
    }

    public List<TileRow> GetFulledTileRows(int value)
    {
        List<TileRow> fulledRows = new List<TileRow>();
        bool isFulledAndCanReversed;
        foreach (TileRow row in rows)
        {
            isFulledAndCanReversed = true;
            foreach (TileCell cell in row.cells)
            {
                if (cell.Empty || cell.tile?.state.number>value)
                {
                    isFulledAndCanReversed = false;
                }
            }

            if (isFulledAndCanReversed)
            {
                fulledRows.Add(row);
            }
        }
        return fulledRows;
    }

    public List<List<TileCell>> GetFulledTileColumns(int value)
    {
        List<List<TileCell>> fulledColumns = new();
        bool isFulledAndCanReversed;
        for (int x = 0; x < cellColumns.Length; x++)
        {
            List<TileCell> column = new List<TileCell>();
            isFulledAndCanReversed = true;
            for (int y = 0; y < cellColumns[x].Length; y++)
            {
                if (cellColumns[x][y].Empty || cellColumns[x][y].tile?.state.number>value)
                {
                    isFulledAndCanReversed = false;
                }
                column.Add(cellColumns[x][y]);
            }

            if (isFulledAndCanReversed)
            {
                fulledColumns.Add(column);
            }
        }
        
        return fulledColumns;
    }

    public TileCell GetBiggestTile(List<TileCell> cells)
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

}


