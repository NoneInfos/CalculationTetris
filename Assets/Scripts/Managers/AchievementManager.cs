using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Achievement
{
    public string id;
    public string title;
    public string description;
    public int requiredValue;
    public bool isUnlocked;
}

public class AchievementManager : SingletonClass<AchievementManager>
{
    [SerializeField] private List<Achievement> achievements;

    public event Action<Achievement> OnAchievementUnlocked;

    private void Awake()
    {
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.isUnlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
        }
    }

    public void UpdateAchievement(string id, int value)
    {
        var achievement = achievements.Find(a => a.id == id);
        if (achievement != null && !achievement.isUnlocked)
        {
            if (value >= achievement.requiredValue)
            {
                UnlockAchievement(achievement);
            }
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        achievement.isUnlocked = true;
        PlayerPrefs.SetInt(achievement.id, 1);
        PlayerPrefs.Save();
        OnAchievementUnlocked?.Invoke(achievement);
    }

    public List<Achievement> GetAllAchievements()
    {
        return achievements;
    }
}