using System.Collections;
using Roughlike2048;
using Roughlike2048.Event;
using TMPro;
using UnityEngine;
using Event = Roughlike2048.Event.Event;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("UI")]
    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI RemainedMovesText;
    [SerializeField] private CanvasGroup UpgradeUI;
    
    [Header("Stats")]
    [SerializeField] private IntegerVariable TargetMoves;
    [SerializeField] private IntegerVariable RemainingMoves;
    [SerializeField] private BooleanVariable IsUpgradeUIActive;
    
    [Header("Events")] 
    [SerializeField] private Event OpenUpgradeUIEvent;
    
    [Header("Manager")] 
    [SerializeField] private UpgradeManager upgradeManager;
    
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }
    
    public void NewGame()
    {
        // reset score
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();
        
        // hide game over screen
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
        
        OpenUpgradeUIEvent.Raise();
    }

    public void Replay()
    {
        upgradeManager.ReplayUpgradeReset();
        board.MoveReplayReset();
        NewGame();
    }
    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

    public void CheckingOpenUpgradeUI(int upgradeMoveCount, int targetMove)
    {
        if (upgradeMoveCount >= targetMove)
        {
            OpenUpgradeUI();
            upgradeMoveCount = 0;
        }
    }

    public void CloseUpgradeUI()
    {
        StartCoroutine(Fade(UpgradeUI, 0f, 1f));
        UpgradeUI.interactable = false;
        UpgradeUI.gameObject.SetActive(false);
    }

    public void OpenUpgradeUI()
    {
        UpgradeUI.gameObject.SetActive(true);
        StartCoroutine(Fade(UpgradeUI, 1f, 0.3f));
        UpgradeUI.interactable = true;
        IsUpgradeUIActive.Value = true;
    }

    public void SetRemainUpgradeText(int remainStep)
    {
        RemainedMovesText.text = $"Next upgrade: {RemainingMoves.Value} moves ";
        if (RemainingMoves.Value == 0)
        {
            OpenUpgradeUIEvent.Raise();
        }
    }

    
    
}
