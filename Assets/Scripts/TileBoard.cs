using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Roughlike2048;
using Roughlike2048.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Event = Roughlike2048.Event.Event;

public class TileBoard : MonoBehaviour
{
    [Header("Tile board settings")] 
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] tileStates;
    
    [Header("Manager")] 
    [SerializeField] private UpgradeManager upgradeManager;
    
    [Header("Stats")] 
    [SerializeField] private FloatVariable FourLuckyMergeProbability;
    [SerializeField] private FloatVariable SuperEightProbability;
    [SerializeField] private TileStateVariables HighRollerState;
    [SerializeField] private UnderVariable UnderUpgradeStat;
    [SerializeField] private ChangingFourTileVariable ChangingFourTileStat;
    [SerializeField] private FirstDeathVariable FirstDeathStat;
    [SerializeField] private HighRiskHighRewardVariable HighRiskHighRewardStat;
    [SerializeField] private Merge2048Variable Merge2048Stat;
    [SerializeField] private BiggerVariable BiggerStat;
    [SerializeField] private PowerMilestoneVariable PowerMilestoneStat;
    [SerializeField] private ReverseUpgradeVariable ReverseUpgradeStat;
    
    [Space] 
    [SerializeField] private IntegerVariable TargetMoves;
    [SerializeField] private IntegerVariable RemainingMoves;
    [SerializeField] private IntegerVariable NumberOfUpgradeSelected;
    [SerializeField] private BooleanVariable IsUpgradeUIActive;
    [SerializeField] private IntegerVariable BiggestValue;
    [SerializeField] private IntegerVariable CurrentBooms;
    
    [Header("Events")] 
    [SerializeField] private Event OpenUpgradeUIEvent;
    [SerializeField] private Event FirstDeathEvent;
    
    [Header("UI")]
    [SerializeField] private Button UnderButton;
    [SerializeField] private Button BoomButton;
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Button BackgroundButton;
    [SerializeField] private TextMeshProUGUI NextMoveText;
    
    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;
    private Stack<List<StoredTile>> tilesStack;
    private int underMoveCount = 0;
    private bool booming = false;
    
    public int UnderMoveCount
    {
        get { return underMoveCount; }
        set
        {
            underMoveCount = value;
            UnderButton.interactable = false;
            if (underMoveCount >= UnderUpgradeStat.Value.CoolDownTurns)
            {
                UnderButton.interactable = true;
            }
        }
    }
    
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
        tilesStack = new Stack<List<StoredTile>>();
        HighRollerState.Value = tileStates[0];
        RemainingMoves.Value = 10;
    }
    
    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(GetTileCreated(HighRollerState.Value));
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }
    public void CreateTile(Tile createdTile)
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(createdTile.state);
        tile.Spawn(createdTile.cell);
        tiles.Add(tile);
    }
    
    private void Update()
    {
        if (waiting) return;
        if (CheckForGameOver()) return;
        if (IsUpgradeUIActive.Value) return;
        if (booming) return;
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)||InputManager.SwipeUp()) {
            Move(Vector2Int.up, 0, 1, 1, 1);
            
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)||InputManager.SwipeLeft()) {
            Move(Vector2Int.left, 1, 1, 0, 1);
            
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)||InputManager.SwipeDown()) {
            Move(Vector2Int.down, 0, 1, grid.Height - 2, -1);
            
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)||InputManager.SwipeRight()) {
            Move(Vector2Int.right, grid.Width - 2, -1, 0, 1);
        }

        ChangingFourTiles();
        EnableUnderButton();
        BiggestValueChangeTrigger();
        StartCoroutine(ReverseChecking());
    }
    #region UnderEvent
    public void StoreTileInStack(List<Tile> tilesPosition)
    {
        List<StoredTile> storedTiles = new List<StoredTile>();
        foreach (var tile in tilesPosition)
        {
            storedTiles.Add(new StoredTile(tile));
        }
        tilesStack.Push(storedTiles);
    }
    
    public void EnableUnderButton()
    {
        UnderButton.gameObject.SetActive(UnderUpgradeStat.Value.IsActivated);
    }
    
    public void OnClickUnderButton()
    {
        var popedTiles = new List<StoredTile>();
        for (int i = 0; i < UnderUpgradeStat.Value.GoBackTurns; i++)
        {
            popedTiles.Clear();
            popedTiles = tilesStack.Pop();
            RemainingMoves.Value++;
        }
        ClearBoard();
        
        foreach (StoredTile storedTile in popedTiles)
        {
            Tile tile = new Tile(storedTile);
            CreateTile(tile);
        }
        UnderMoveCount = 0;
        
    }
    #endregion
    
    #region HighRollerEvent
    public TileState GetTileCreated(TileState state)
    {
        if(GetByProbability(0.9f))
        {
            return state;
        }
        return tileStates[Mathf.Clamp(IndexOf(state) + 1, 0, tileStates.Length - 1)];
    }
    #endregion
    
    #region ChangingFourTilesUpgradeEvent
    public void ChangingFourTiles()
    {
        int numberOfTiles = ChangingFourTileStat.Value.NumberOfTiles; 
        int tileNumberValue = ChangingFourTileStat.Value.TileNumberValue;
        bool IsChangingFourUpgradeActivated = ChangingFourTileStat.Value.IsActivated;
        int multiple = ChangingFourTileStat.Value.Multiple;
        if (CheckingFourTiles(numberOfTiles, tileNumberValue) && IsChangingFourUpgradeActivated)
        {
            var tileList = tiles.Where(t=>t.state.number == tileNumberValue).ToList();
            foreach (var tile in tileList)
            {
                TileState state = tileStates[Mathf.Clamp(IndexOf(tile.state) + (int)Mathf.Log(multiple,2)
                    , 0, tileStates.Length - 1)];
                tile.SetState(state);
            }
        }
    }
    
    public bool CheckingFourTiles(int numberOfTiles, int tileNumberValue)
    {
        return tiles.Where(t=>t.state.number == tileNumberValue).ToList().Count() == numberOfTiles;
    }
    #endregion
    
    #region FirstDeathEvent

    public void FirstDeathEventResponse()
    {
        DestroyAllExceptForBiggestAndSmallest();
        FirstDeathStat.Value = new FirstDeathUpgradeData(false);
    }

    private void DestroyAllExceptForBiggestAndSmallest()
    {
        Tile biggestTile = GetBiggestTile();
        Tile smallestTile = GetSmallestTile();
        
        ClearBoard();
        CreateTile(biggestTile);
        CreateTile(smallestTile);
        
    }

    private Tile GetBiggestTile()
    {
        Tile tile = tiles[0];
        foreach (var t in tiles) 
        {
            if (t.state.number > tile.state.number)
            {
                tile = t;
            }
        }
        return tile;
    }
    private Tile GetSmallestTile()
    {
        Tile tile = tiles[0];
        foreach (var t in tiles) 
        {
            if (t.state.number < tile.state.number)
            {
                tile = t;
            }
        }
        return tile;
    }
    public void ClearBoard()
    {
        foreach (var cell in grid.cells) {
            cell.tile = null;
        }

        foreach (var tile in tiles) {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }
    #endregion
    
    #region HighRiskHighRewardEvent

    public bool IsInCorner(Tile tile)
    {
        if (tile.cell.coordinates == Vector2Int.zero || tile.cell.coordinates == new Vector2Int(0, 3) ||
            tile.cell.coordinates == new Vector2Int(3, 3) || tile.cell.coordinates == new Vector2Int(3, 0))
        {
            return true;
        }

        return false;
    }
    public bool IsBiggestTileInCorner()
    {
        if (GetBiggestTile().state.number >= HighRiskHighRewardStat.Value.BiggsetValueToTrigger
            && !IsInCorner(GetBiggestTile()))
        {
            return true;
        }

        return false;
    }
    
    #endregion

    #region Merge2048Event

    IEnumerator WaitForMerge2048()
    {
        yield return new WaitUntil(()=>!waiting);
        foreach (var tileValue in Merge2048Stat.Value.CreatedValues)
        {
            TileState state = GetByNumber(tileValue);
            Tile tile = new Tile(grid.GetRandomEmptyCell(),state);
            CreateTile(tile);
        }
    }
    
    #endregion

    #region BiggerEvent && PowerMilestoneEvent

    public void BiggestValueChangeTrigger()
    {
        int biggestTileValue= GetBiggestTile().state.number;
        if (BiggestValue.Value < biggestTileValue && biggestTileValue > 4)
        {
            if (BiggerStat.Value.PercentageValue > 0)
            {
                Tile newTile = new Tile(grid.GetRandomEmptyCell(),GetByNumber((int)(biggestTileValue*BiggerStat.Value.PercentageValue)));
                CreateTile(newTile);
            }
            if ((NumberOfDigits(biggestTileValue) != NumberOfDigits(BiggestValue.Value)) && PowerMilestoneStat.Value.IsActivated)
            {
                OpenUpgradeUIEvent.Raise();
            }
            BiggestValue.Value = biggestTileValue;
        }
        
    }

    public TileState GetByNumber(int number)
    {
        return tileStates.Where(x=>x.number == number).FirstOrDefault();
    }
    public int NumberOfDigits(int number)
    {
        int value = number;
        int count = 0;
        while (value > 0)
        {
            value /= 10;
            count++;
        }
        return count;
    }

    #endregion

    #region BoomEvent

    public void EnableBoomButton()
    {
        BoomButton.gameObject.SetActive(true);
    }
    public void OnClickBoomButton()
    {
        BackgroundImage.color = Color.grey;
        NextMoveText.color = Color.white;
        foreach (Tile tile in tiles)
        {
            Button button = tile.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => { OnClickTileCell(tile); });
        }
        
        booming = true;
        BackgroundButton.onClick.AddListener(()=>AfterSelectTileCell());
    }

    public void OnClickTileCell(Tile tile)
    {
        tile.cell.tile = null;
        tiles.Remove(tile);
        Destroy(tile.gameObject);
        CurrentBooms.Value--;
        AfterSelectTileCell();
    }
    
    public void AfterSelectTileCell()
    {
        foreach (Tile tile in tiles)
        {
            Button button = tile.gameObject.GetComponent<Button>();
            if (button)
            {
                Destroy(button);
            }
        }
        BackgroundImage.color = Color.white;
        NextMoveText.color = new Color(119f/255f, 110f/255f, 101f/255f, 1f);
        StartCoroutine(WaitingForBooming());
        BackgroundButton.onClick.RemoveAllListeners();
    }

    IEnumerator WaitingForBooming()
    {
        yield return new WaitForSeconds(0.1f);
        booming = false;
    }
    #endregion

    #region ReverseEvent

    IEnumerator ReverseChecking()
    {
        yield return new WaitUntil(()=>!waiting);
        List<TileRow> rows = grid.GetFulledTileRows(ReverseUpgradeStat.Value.LimitedValue);
        foreach (TileRow row in rows)
        {
            foreach (TileCell cell in row.cells)
            {
                if (cell.tile != null)
                {
                    if (!cell.IsBiggestInRow)
                    {
                        ClearTile(cell, cell.tile);
                    }
                    else
                    {
                        cell.tile.SetState(GetByNumber(cell.tile.state.number * 2));
                    }
                }
               
            }
        }
    }
    

    #endregion
    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        StoreTileInStack(tiles);
        
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.Width; x += incrementX)
        {  
            for (int y = startY; y >= 0 && y < grid.Height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.Occupied) {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed) {
            StartCoroutine(WaitForChanges());
            if (IsBiggestTileInCorner() && HighRiskHighRewardStat.Value.IsActivated)
            {
                UnderMoveCount += HighRiskHighRewardStat.Value.Move;
                RemainingMoves.Value -= HighRiskHighRewardStat.Value.Move;
            }
            else
            {
                UnderMoveCount++;
                RemainingMoves.Value--;
            }
        }
    }
    
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.Occupied)
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    MergeTiles(tile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.state == b.state && !b.locked;
    }

    private void MergeTiles(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = GetMergeTileIndex(a, b);
        TileState newState = tileStates[index];
        
        b.SetState(newState);
        //Merge2048
        if (newState.number == Merge2048Stat.Value.MergeValue)
        {
            StartCoroutine(WaitForMerge2048());
        }
        GameManager.Instance.IncreaseScore(newState.number);
    }
    
    
    private int GetMergeTileIndex(Tile a, Tile b)
    {
        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        if (GetByProbability(FourLuckyMergeProbability.Value) && a.state.number ==4)
        {
            index = Mathf.Clamp(IndexOf(b.state) + 2, 0, tileStates.Length - 1);
        }
        if (GetByProbability(SuperEightProbability.Value) && a.state.number ==8)
        {
            index = Mathf.Clamp(IndexOf(b.state) + 2, 0, tileStates.Length - 1);
        }
        return index;
    }
    public bool GetByProbability(float probability)
    {
        return UnityEngine.Random.value <= probability;  
    }
    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i]) {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles) {
            tile.locked = false;
        }

        if (tiles.Count != grid.Size) {
            CreateTile();
        }

        if (CheckForGameOver()) {
            if (FirstDeathStat.Value.IsActivated)
            {
                FirstDeathEvent.Raise();
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public bool CheckForGameOver()
    {
        if (tiles.Count != grid.Size) {
            return false;
        }
        
        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);
        
            if (up != null && CanMerge(tile, up.tile)) {
                return false;
            }
        
            if (down != null && CanMerge(tile, down.tile)) {
                return false;
            }
        
            if (left != null && CanMerge(tile, left.tile)) {
                return false;
            }
        
            if (right != null && CanMerge(tile, right.tile)) {
                return false;
            }
        }
        
        return true;
        
    }
    
    public void SetNumberOfUpgradeSelected(int value)
    {
        if (NumberOfUpgradeSelected.Value >= Const.UpgradeSelectsToIncreaseTargetMove)
        {
            NumberOfUpgradeSelected.Value = 0;
            TargetMoves.Value += Const.TargetMoveIncreaseAmount;
        }
    }

    public void ChoosingUpgradeEventRespone()
    {
        NumberOfUpgradeSelected.Value++;
        RemainingMoves.Value = TargetMoves.Value;
        upgradeManager.UpgradeAvailableGroup();
        StartCoroutine(IEWaitForUpgradeUIInactive());
    }
    
    IEnumerator IEWaitForUpgradeUIInactive()
    {
        yield return new WaitForSeconds(0.05f);
        IsUpgradeUIActive.Value = false;
    }

    public void MoveReplayReset()
    {
        UnderMoveCount = 0;
        RemainingMoves.Reset();
        TargetMoves.Reset();
        NumberOfUpgradeSelected.Reset();
        IsUpgradeUIActive.Reset();
        BiggestValue.Reset();
        CurrentBooms.Reset();
        EnableUnderButton();
    }

    public void ClearTile(TileCell tileCell, Tile tile)
    {
        tileCell.tile = null;
        Destroy(tile.gameObject);
        tiles.Remove(tile);
    }
    
}
