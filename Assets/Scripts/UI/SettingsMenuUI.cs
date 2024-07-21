using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;
using System.Collections.Generic;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle vibrationToggle;
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private Toggle graphicsQualityToggle;
    [SerializeField] private TMP_Dropdown themeDropdown;
    [SerializeField] private Toggle nextBlockPreviewToggle;
    private void Start()
    {
        InitializeUI();
        AddListeners();
        PopulateThemeDropdown();

        nextBlockPreviewToggle.isOn = SettingsManager.Instance.IsNextBlockPreviewEnabled;
        nextBlockPreviewToggle.onValueChanged.AddListener(OnNextBlockPreviewToggleChanged);
    }
    private void OnNextBlockPreviewToggleChanged(bool isOn)
    {
        SettingsManager.Instance.SetNextBlockPreviewEnabled(isOn);
    }
    private void PopulateThemeDropdown()
    {
        themeDropdown.ClearOptions();
        List<string> themeNames = new List<string>();
        for (int i = 0; i < ThemeManager.Instance.AvailableThemes.Count; i++)
        {
            themeNames.Add(ThemeManager.Instance.AvailableThemes[i].name);
        }
        themeDropdown.AddOptions(themeNames);
        themeDropdown.value = PlayerPrefs.GetInt(ThemeManager.CURRENT_THEME_KEY, 0);
    }

    private void InitializeUI()
    {
        musicVolumeSlider.value = SettingsManager.Instance.MusicVolume;
        sfxVolumeSlider.value = SettingsManager.Instance.SFXVolume;
        vibrationToggle.isOn = SettingsManager.Instance.VibrationEnabled;
        languageDropdown.value = (int)SettingsManager.Instance.Language;
        graphicsQualityToggle.isOn = SettingsManager.Instance.GraphicsQuality == 1;
    }

    private void AddListeners()
    {
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        vibrationToggle.onValueChanged.AddListener(OnVibrationChanged);
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        graphicsQualityToggle.onValueChanged.AddListener(OnGraphicsQualityChanged);

        themeDropdown.onValueChanged.AddListener(OnThemeChanged);
    }
    private void OnThemeChanged(int value)
    {
        ThemeManager.Instance.SetTheme(value);
    }
    private void OnMusicVolumeChanged(float value)
    {
        SettingsManager.Instance.SetMusicVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        SettingsManager.Instance.SetSFXVolume(value);
    }

    private void OnVibrationChanged(bool enabled)
    {
        SettingsManager.Instance.SetVibration(enabled);
    }

    private void OnLanguageChanged(int value)
    {
        SettingsManager.Instance.SetLanguage((SystemLanguage)value);
    }

    private void OnGraphicsQualityChanged(bool isHigh)
    {
        SettingsManager.Instance.SetGraphicsQuality(isHigh ? 1 : 0);
    }

    public void OnCloseButtonClicked()
    {
        SettingsManager.Instance.SaveSettings();
        gameObject.SetActive(false);
    }
}