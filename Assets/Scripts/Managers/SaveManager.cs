using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class SaveData
{
    public int HighScore;
    public int TotalGamesPlayed;
    public int TotalLinesCleared;
    public float TotalPlayTime;
    public string LastPlayDate;
}

public class SaveManager : SingletonClass<SaveManager>
{
    private const string SAVE_FILE_NAME = "gameData.json";
    private const string SETTINGS_KEY = "GameSettings";


    public SaveData CurrentSaveData { get; private set; }

    private void Awake()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(CurrentSaveData);
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CurrentSaveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            CurrentSaveData = new SaveData();
        }
    }

    public void UpdateHighScore(int score)
    {
        if (score > CurrentSaveData.HighScore)
        {
            CurrentSaveData.HighScore = score;
            SaveGame();
        }
    }

    public void IncrementGamesPlayed()
    {
        CurrentSaveData.TotalGamesPlayed++;
        SaveGame();
    }

    public void AddLinesCleared(int lines)
    {
        CurrentSaveData.TotalLinesCleared += lines;
        SaveGame();
    }

    public void UpdatePlayTime(float time)
    {
        CurrentSaveData.TotalPlayTime += time;
        CurrentSaveData.LastPlayDate = System.DateTime.Now.ToString();
        SaveGame();
    }

    public void SaveSettings(float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public (float musicVolume, float sfxVolume) LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        return (musicVolume, sfxVolume);
    }
}
