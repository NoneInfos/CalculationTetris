using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGEngine : MonoBehaviour
{
    public IGBlockController _blockController { private set; get; }

    public IGInputController _inputController { private set; get; }

    public IGBoardController _boardController { private set; get; }

    private float gameTime = 0f;

    [SerializeField] private GameObject settingsMenuPanel;

    [SerializeField] Transform TF_Cameras;

    private bool isChallengeModeActive = false;

    private bool _isGameOver = false;

    private float gameStartTime;

    private void Awake()
    {
        PoolManager.Instance.InitializeManager();

        //TF_Cameras.transform.position = new Vector2(IGConfig.SCREEN_WIDTH_HALF - ((IGConfig.TILE_WIDTH_HALF / 2) * 3), 200f); Camera cam = GetComponent<Camera>();

       
    }
    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);

        float verticalSize = (IGConfig.BOARD_ROW * IGConfig.TILE_HEIGHT / 2f);
        float horizontalSize = (IGConfig.BOARD_COL * IGConfig.TILE_WIDTH / 2f);

        // 화면 비율에 따라 적절한 size 선택
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = horizontalSize / verticalSize;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = verticalSize;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = verticalSize * differenceInSize;
        }
    }



    private void Start()
    {
        GameStateManager.Instance.SetGameState(GameState.MainMenu);
        LoadGameData();

        AchievementManager.Instance.OnAchievementUnlocked += OnAchievementUnlocked;
        gameStartTime = Time.time;
    }
    private void LoadGameData()
    {
        StartGame();
    }

    private void OnDestroy()
    {
        AchievementManager.Instance.OnAchievementUnlocked -= OnAchievementUnlocked;
    }
    private void OnAchievementUnlocked(Achievement achievement)
    {
        UIManager.Instance.ShowAchievementUnlockedNotification(achievement);
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
    }
    public void StartDailyChallenge()
    {
        isChallengeModeActive = true;
        UnityEngine.Random.InitState(DailyChallengeManager.Instance.CurrentChallenge.seed);
        StartGame();
    }
    public T CreateObj<T>(Transform inParent, bool isActive = false, string inLayerName = "Default") where T : Component
    {
        GameObject go = new GameObject(typeof(T).Name);
        go.layer = LayerMask.NameToLayer(inLayerName);
        go.transform.SetParent(inParent);
        return go.AddComponent<T>();
    }

    public void OpenSettingsMenu()
    {
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuPanel.SetActive(false);
    }

    public void StartGame()
    {
        _blockController = CreateObj<IGBlockController>(this.transform, true, "InGame");
        _blockController.SetEngine(this);
        _blockController.InitializeController();

        _boardController = CreateObj<IGBoardController>(this.transform, true, "InGame");
        _boardController.SetEngine(this);
        _boardController.InitializeController();

        SetupCamera();
        GameStateManager.Instance.SetGameState(GameState.Playing);

        DifficultyManager.Instance.ResetDifficulty();
        SaveManager.Instance.IncrementGamesPlayed();
        AudioManager.Instance.Play("GameStart");
    }
    public void EndGame()
    {
        GameStateManager.Instance.SetGameState(GameState.GameOver);
        SaveManager.Instance.UpdateHighScore(ScoreManager.Instance.CurrentScore);
        SaveManager.Instance.UpdatePlayTime(gameTime);
        gameTime = 0f;
        UpdateAchievements();
        UpdateLeaderboard();

        if (isChallengeModeActive)
        {
            CheckChallengeCompletion();
        }

        isChallengeModeActive = false;

        int score = ScoreManager.Instance.CurrentScore;
        int linesCleared = _boardController.GetTotalClearedLines();
        int blocksPlaced = _blockController.GetTotalPlacedBlocks();
        int gameTimeSeconds = Mathf.RoundToInt(Time.time - gameStartTime);
        int squaresCleared = _boardController.GetTotalClearedSquares();
        bool challengeCompleted = isChallengeModeActive && DailyChallengeManager.Instance.IsChallengeClear(score, linesCleared, blocksPlaced, squaresCleared);

        GameStatsManager.Instance.UpdateStats(score, linesCleared, blocksPlaced, gameTimeSeconds, squaresCleared, challengeCompleted);
        AudioManager.Instance.Play("GameOver");
    }

    private void CheckChallengeCompletion()
    {
        int score = ScoreManager.Instance.CurrentScore;
        int clearedLines = _boardController.GetTotalClearedLines();
        int placedBlocks = _blockController.GetTotalPlacedBlocks();
        int clearedSquares = _boardController.GetTotalClearedSquares();

        bool isChallengeClear = DailyChallengeManager.Instance.IsChallengeClear(score, clearedLines, placedBlocks, clearedSquares);

        if (isChallengeClear)
        {
            UIManager.Instance.ShowChallengeCompleteUI();
        }
        else
        {
            UIManager.Instance.ShowChallengeFailedUI();
        }
    }
    private void UpdateLeaderboard()
    {
        int score = ScoreManager.Instance.CurrentScore;
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        LeaderboardManager.Instance.AddEntry(playerName, score);
    }
    private void UpdateAchievements()
    {
        int score = ScoreManager.Instance.CurrentScore;
        int clearedLines = _boardController.GetTotalClearedLines();
        int gamesPlayed = SaveManager.Instance.CurrentSaveData.TotalGamesPlayed;

        AchievementManager.Instance.UpdateAchievement("SCORE_1000", score);
        AchievementManager.Instance.UpdateAchievement("SCORE_5000", score);
        AchievementManager.Instance.UpdateAchievement("CLEAR_100_LINES", clearedLines);
        AchievementManager.Instance.UpdateAchievement("PLAY_10_GAMES", gamesPlayed);
    }

    private void Update()
    {
        if (!_isGameOver)
        {
            _blockController?.UpdateController();
            _boardController?.UpdateController();
        }

        if (GameStateManager.Instance.CurrentState == GameState.Playing)
        {
            gameTime += Time.deltaTime;

            _blockController?.UpdateController();
            _boardController?.UpdateController();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStateManager.Instance.CurrentState == GameState.Playing)
            {
                GameStateManager.Instance.SetGameState(GameState.Paused);
            }
            else if (GameStateManager.Instance.CurrentState == GameState.Paused)
            {
                GameStateManager.Instance.SetGameState(GameState.Playing);
            }
        }
    }

    public void RestartGame()
    {
        _blockController.ClearController();
        _boardController.ClearController();
        StartGame();
    }
}
