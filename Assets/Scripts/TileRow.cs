using UnityEngine;

public class TileRow : MonoBehaviour
{
    public TileCell[] cells { get; private set; }

    private void Awake()
    {
        cells = GetComponentsInChildren<TileCell>();
        TileCell tilecell = GetBiggestTileInRow();
        tilecell.IsBiggestInRow = true;
    }

    public TileCell GetBiggestTileInRow()
    {
        TileCell tilecell = cells[0];
        foreach (TileCell cell in cells)
        {
            if (cell.tile?.state.number > tilecell.tile?.state.number)
            {
                tilecell = cell;
            }
        }
        return tilecell;
    }
    
    
}
