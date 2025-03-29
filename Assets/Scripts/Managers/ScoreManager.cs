using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonClass<ScoreManager>
{
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    public event System.Action<int> OnScoreChanged;

    private void Awake()
    {
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
       
    }

    public void AddScore(int clearedLines)
    {
        int scoreToAdd = clearedLines * clearedLines * 100; 
        CurrentScore += scoreToAdd;

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}