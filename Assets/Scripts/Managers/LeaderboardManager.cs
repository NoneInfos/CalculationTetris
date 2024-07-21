using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;
    public System.DateTime date;

    public LeaderboardEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
        this.date = System.DateTime.Now;
    }
}
public class LeaderboardManager : SingletonClass<LeaderboardManager>
{

    private const string LEADERBOARD_KEY = "Leaderboard";
    private const int MAX_ENTRIES = 10;

    private List<LeaderboardEntry> leaderboard;

    private void Awake()
    {
        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        string json = PlayerPrefs.GetString(LEADERBOARD_KEY, "[]");
        leaderboard = JsonUtility.FromJson<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(leaderboard);
        PlayerPrefs.SetString(LEADERBOARD_KEY, json);
        PlayerPrefs.Save();
    }

    public void AddEntry(string playerName, int score)
    {
        leaderboard.Add(new LeaderboardEntry(playerName, score));
        leaderboard = leaderboard.OrderByDescending(e => e.score).Take(MAX_ENTRIES).ToList();
        SaveLeaderboard();
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return new List<LeaderboardEntry>(leaderboard);
    }

    public int GetPlayerRank(int score)
    {
        return leaderboard.Count(e => e.score > score) + 1;
    }
}