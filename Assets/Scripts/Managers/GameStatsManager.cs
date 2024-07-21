using UnityEngine;
using System;

[System.Serializable]
public class GameStats
{
    public int totalGamesPlayed;
    public int totalScore;
    public int highestScore;
    public int totalLinesCleared;
    public int totalBlocksPlaced;
    public int totalPlayTimeSeconds;
    public int longestGameTimeSeconds;
    public int totalSquaresCleared;
    public int totalChallengesCompleted;
}
public class GameStatsManager : SingletonClass<GameStatsManager>
{

    private const string STATS_KEY = "GameStats";

    public GameStats CurrentStats { get; private set; }

    private void Awake()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        string json = PlayerPrefs.GetString(STATS_KEY, "");
        if (string.IsNullOrEmpty(json))
        {
            CurrentStats = new GameStats();
        }
        else
        {
            CurrentStats = JsonUtility.FromJson<GameStats>(json);
        }
    }

    private void SaveStats()
    {
        string json = JsonUtility.ToJson(CurrentStats);
        PlayerPrefs.SetString(STATS_KEY, json);
        PlayerPrefs.Save();
    }

    public void UpdateStats(int score, int linesCleared, int blocksPlaced, int gameTimeSeconds, int squaresCleared, bool challengeCompleted)
    {
        CurrentStats.totalGamesPlayed++;
        CurrentStats.totalScore += score;
        CurrentStats.highestScore = Mathf.Max(CurrentStats.highestScore, score);
        CurrentStats.totalLinesCleared += linesCleared;
        CurrentStats.totalBlocksPlaced += blocksPlaced;
        CurrentStats.totalPlayTimeSeconds += gameTimeSeconds;
        CurrentStats.longestGameTimeSeconds = Mathf.Max(CurrentStats.longestGameTimeSeconds, gameTimeSeconds);
        CurrentStats.totalSquaresCleared += squaresCleared;
        if (challengeCompleted) CurrentStats.totalChallengesCompleted++;

        SaveStats();
    }

    public string GetFormattedTotalPlayTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(CurrentStats.totalPlayTimeSeconds);
        return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", time.Hours, time.Minutes, time.Seconds);
    }

    public string GetFormattedLongestGameTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(CurrentStats.longestGameTimeSeconds);
        return string.Format("{0:D2}m:{1:D2}s", time.Minutes, time.Seconds);
    }
}