using UnityEngine;
using System;

[System.Serializable]
public class DailyChallenge
{
    public int seed;
    public string objective;
    public int targetScore;
    public System.DateTime date;

    public DailyChallenge(int seed, string objective, int targetScore)
    {
        this.seed = seed;
        this.objective = objective;
        this.targetScore = targetScore;
        this.date = System.DateTime.Now.Date;
    }
}
public class DailyChallengeManager : SingletonClass<DailyChallengeManager>
{
    private const string LAST_CHALLENGE_DATE_KEY = "LastChallengeDate";
    private const string CHALLENGE_SEED_KEY = "ChallengeSeed";
    private const string CHALLENGE_OBJECTIVE_KEY = "ChallengeObjective";
    private const string CHALLENGE_TARGET_SCORE_KEY = "ChallengeTargetScore";

    public DailyChallenge CurrentChallenge { get; private set; }

    private void Awake()
    {
        LoadOrGenerateChallenge();
    }

    private void LoadOrGenerateChallenge()
    {
        DateTime lastChallengeDate = DateTime.Parse(PlayerPrefs.GetString(LAST_CHALLENGE_DATE_KEY, DateTime.MinValue.ToString()));

        if (lastChallengeDate.Date != DateTime.Now.Date)
        {
            GenerateNewChallenge();
        }
        else
        {
            LoadExistingChallenge();
        }
    }

    private void GenerateNewChallenge()
    {
        int seed = UnityEngine.Random.Range(0, 1000000);
        string objective = GenerateRandomObjective();
        int targetScore = GenerateTargetScore();

        CurrentChallenge = new DailyChallenge(seed, objective, targetScore);
        SaveChallenge();
    }

    private void LoadExistingChallenge()
    {
        int seed = PlayerPrefs.GetInt(CHALLENGE_SEED_KEY, 0);
        string objective = PlayerPrefs.GetString(CHALLENGE_OBJECTIVE_KEY, "Score as high as possible");
        int targetScore = PlayerPrefs.GetInt(CHALLENGE_TARGET_SCORE_KEY, 1000);

        CurrentChallenge = new DailyChallenge(seed, objective, targetScore);
    }

    private void SaveChallenge()
    {
        PlayerPrefs.SetString(LAST_CHALLENGE_DATE_KEY, CurrentChallenge.date.ToString());
        PlayerPrefs.SetInt(CHALLENGE_SEED_KEY, CurrentChallenge.seed);
        PlayerPrefs.SetString(CHALLENGE_OBJECTIVE_KEY, CurrentChallenge.objective);
        PlayerPrefs.SetInt(CHALLENGE_TARGET_SCORE_KEY, CurrentChallenge.targetScore);
        PlayerPrefs.Save();
    }

    private string GenerateRandomObjective()
    {
        string[] objectives = new string[]
        {
            "Clear 10 lines",
            "Score 5000 points",
            "Place 50 blocks",
            "Clear 3 squares"
        };

        return objectives[UnityEngine.Random.Range(0, objectives.Length)];
    }

    private int GenerateTargetScore()
    {
        return UnityEngine.Random.Range(1000, 10000);
    }

    public bool IsChallengeClear(int score, int clearedLines, int placedBlocks, int clearedSquares)
    {
        switch (CurrentChallenge.objective)
        {
            case "Clear 10 lines":
                return clearedLines >= 10;
            case "Score 5000 points":
                return score >= 5000;
            case "Place 50 blocks":
                return placedBlocks >= 50;
            case "Clear 3 squares":
                return clearedSquares >= 3;
            default:
                return score >= CurrentChallenge.targetScore;
        }
    }
}