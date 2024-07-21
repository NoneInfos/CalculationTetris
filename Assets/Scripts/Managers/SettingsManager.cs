using UnityEngine;
using System;

public class SettingsManager : SingletonClass<SettingsManager>
{

    public event Action OnSettingsChanged;

    public float MusicVolume { get; private set; } = 1f;
    public float SFXVolume { get; private set; } = 1f;
    public bool VibrationEnabled { get; private set; } = true;
    public SystemLanguage Language { get; private set; } = SystemLanguage.English;
    public int GraphicsQuality { get; private set; } = 1;

    public bool IsNextBlockPreviewEnabled { get; private set; }


    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string VIBRATION_KEY = "Vibration";
    private const string LANGUAGE_KEY = "Language";
    private const string GRAPHICS_QUALITY_KEY = "GraphicsQuality";
    private const string NEXT_BLOCK_PREVIEW_KEY = "NextBlockPreview";


    
    private void Start()
    {
        LoadSettings();
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, MusicVolume);
        OnSettingsChanged?.Invoke();
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SFXVolume);
        OnSettingsChanged?.Invoke();
    }

    public void SetVibration(bool enabled)
    {
        VibrationEnabled = enabled;
        PlayerPrefs.SetInt(VIBRATION_KEY, VibrationEnabled ? 1 : 0);
        OnSettingsChanged?.Invoke();
    }

    public void SetLanguage(SystemLanguage language)
    {
        Language = language;
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)Language);
        OnSettingsChanged?.Invoke();
    }

    public void SetGraphicsQuality(int quality)
    {
        GraphicsQuality = Mathf.Clamp(quality, 0, 1);
        PlayerPrefs.SetInt(GRAPHICS_QUALITY_KEY, GraphicsQuality);
        QualitySettings.SetQualityLevel(GraphicsQuality);
        OnSettingsChanged?.Invoke();
    }

    private void LoadSettings()
    {
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        VibrationEnabled = PlayerPrefs.GetInt(VIBRATION_KEY, 1) == 1;
        Language = (SystemLanguage)PlayerPrefs.GetInt(LANGUAGE_KEY, (int)SystemLanguage.English);
        GraphicsQuality = PlayerPrefs.GetInt(GRAPHICS_QUALITY_KEY, 1);
        IsNextBlockPreviewEnabled = PlayerPrefs.GetInt(NEXT_BLOCK_PREVIEW_KEY, 1) == 1;
    }
    public void SetNextBlockPreviewEnabled(bool enabled)
    {
        IsNextBlockPreviewEnabled = enabled;
        PlayerPrefs.SetInt(NEXT_BLOCK_PREVIEW_KEY, enabled ? 1 : 0);
        PlayerPrefs.Save();
        OnSettingsChanged?.Invoke();
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}