using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IGMain;

public class UIManager : SingletonClass<UIManager>
{
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject MainMenuPanel;
    public GameObject GamePlayPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject NextBlockPreview;

    [SerializeField] private GameObject achievementNotificationPrefab;
    [SerializeField] private Transform notificationParent;
    [SerializeField] private LeaderboardUI leaderboardUI;

    [SerializeField] private GameObject dailyChallengePanel;
    [SerializeField] private TextMeshProUGUI challengeObjectiveText;
    [SerializeField] private TextMeshProUGUI challengeTargetScoreText;

    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI totalGamesPlayedText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI totalLinesClearedText;
    [SerializeField] private TextMeshProUGUI totalBlocksPlacedText;
    [SerializeField] private TextMeshProUGUI totalPlayTimeText;
    [SerializeField] private TextMeshProUGUI longestGameTimeText;
    [SerializeField] private TextMeshProUGUI totalSquaresClearedText;
    [SerializeField] private TextMeshProUGUI totalChallengesCompletedText;

    [SerializeField] private GameObject nextBlockPreview;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;

        UpdateScoreUI(ScoreManager.Instance.CurrentScore);
        UpdateHighScoreUI();
        UpdateNextBlockPreviewVisibility();
        SettingsManager.Instance.OnSettingsChanged += UpdateNextBlockPreviewVisibility;
    }

    public void ShowAchievementUnlockedNotification(Achievement achievement)
    {
        var notification = Instantiate(achievementNotificationPrefab, notificationParent);
        var notificationUI = notification.GetComponent<AchievementNotificationUI>();
        notificationUI.SetAchievement(achievement);
        Destroy(notification, 3f);
    }
    private void UpdateScoreUI(int score)
    {
        //ScoreText.text = $"Score: {score}";
    }
    private void OnDestroy()
    {
        SettingsManager.Instance.OnSettingsChanged -= UpdateNextBlockPreviewVisibility;
    }

    private void UpdateNextBlockPreviewVisibility()
    {
        //nextBlockPreview.SetActive(SettingsManager.Instance.IsNextBlockPreviewEnabled);
    }

    private void UpdateHighScoreUI()
    {
        //HighScoreText.text = $"High Score: {ScoreManager.Instance.HighScore}";
    }

    private void HandleGameStateChanged(GameState newState)
    {
        MainMenuPanel.SetActive(newState == GameState.MainMenu);
        GamePlayPanel.SetActive(newState == GameState.Playing);
        GameOverPanel.SetActive(newState == GameState.GameOver);
        PausePanel.SetActive(newState == GameState.Paused);
    }

    public void UpdateNextBlockPreview(IGBlock nextBlock)
    {
        if (!SettingsManager.Instance.IsNextBlockPreviewEnabled)
            return;

        if (NextBlockPreview == null)
            return;

        foreach (Transform child in NextBlockPreview.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var node in nextBlock.BlockNodes)
        {
            var previewNode = Instantiate(node, NextBlockPreview.transform);
            previewNode.transform.localPosition = node.transform.localPosition;
        }
    }

    public void ShowLeaderboard()
    {
        leaderboardUI.gameObject.SetActive(true);
        leaderboardUI.ShowLeaderboard();
    }

    public void HideLeaderboard()
    {
        leaderboardUI.gameObject.SetActive(false);
    }

    public void ShowDailyChallengeUI()
    {
        dailyChallengePanel.SetActive(true);
        UpdateDailyChallengeUI();
    }

    private void UpdateDailyChallengeUI()
    {
        var challenge = DailyChallengeManager.Instance.CurrentChallenge;
        challengeObjectiveText.text = $"Objective: {challenge.objective}";
        challengeTargetScoreText.text = $"Target Score: {challenge.targetScore}";
    }

    public void ShowChallengeCompleteUI()
    {
    }

    public void ShowChallengeFailedUI()
    {
    }

    public void ShowStatsUI()
    {
        statsPanel.SetActive(true);
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        var stats = GameStatsManager.Instance.CurrentStats;
        totalGamesPlayedText.text = $"Total Games: {stats.totalGamesPlayed}";
        totalScoreText.text = $"Total Score: {stats.totalScore}";
        highestScoreText.text = $"Highest Score: {stats.highestScore}";
        totalLinesClearedText.text = $"Lines Cleared: {stats.totalLinesCleared}";
        totalBlocksPlacedText.text = $"Blocks Placed: {stats.totalBlocksPlaced}";
        totalPlayTimeText.text = $"Total Play Time: {GameStatsManager.Instance.GetFormattedTotalPlayTime()}";
        longestGameTimeText.text = $"Longest Game: {GameStatsManager.Instance.GetFormattedLongestGameTime()}";
        totalSquaresClearedText.text = $"Squares Cleared: {stats.totalSquaresCleared}";
        totalChallengesCompletedText.text = $"Challenges Completed: {stats.totalChallengesCompleted}";
    }

    public void HideStatsUI()
    {
        statsPanel.SetActive(false);
    }
}