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
    
    [Header("Event")]
    [SerializeField] private Event FourLuckyMergeEvent;
    [SerializeField] private Event SuperEightEvent;

    [Header("Stats")] 
    [SerializeField] private FloatVariable FourLuckyMergeProbability;
    [SerializeField] private UpgradeGroup FourLuckeyMergeGroup;
    [SerializeField] private FloatVariable SuperEightProbability;
    [SerializeField] private UpgradeGroup SuperEightGroup;
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
        
        // FourLuckyMergeEvent.Raise();
        // SuperEightEvent.Raise();
    }
    
    public void ListenEventFourLuckyMerge()
    {
        FourLuckyMergeUpgrade fourLuckyMerge =(FourLuckyMergeUpgrade) FourLuckeyMergeGroup.Upgrades[0];
        FourLuckyMergeProbability.Value = fourLuckyMerge.Probability;
    }
    public void ListenEventSuperEight()
    {
        SuperEightUpgrade superEight =(SuperEightUpgrade) SuperEightGroup.Upgrades[0];
        SuperEightProbability.Value = superEight.Probability;
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

}
