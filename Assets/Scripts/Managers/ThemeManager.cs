using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Theme
{
    public string name;
    public Color backgroundColor;
    public Color gridLineColor;
    public Color blockColor;
    public Color textColor;
    public Color uiBackgroundColor;
}
public class ThemeManager : SingletonClass<ThemeManager>
{
    [SerializeField] private List<Theme> availableThemes;

    public List<Theme> AvailableThemes { get { return availableThemes; } }
    public Theme CurrentTheme { get; private set; }

    public delegate void ThemeChangedHandler(Theme newTheme);
    public event ThemeChangedHandler OnThemeChanged;

    public const string CURRENT_THEME_KEY = "CurrentTheme";

    private void Start()
    {
        //LoadCurrentTheme();
    }

    private void LoadCurrentTheme()
    {
        int currentThemeIndex = PlayerPrefs.GetInt(CURRENT_THEME_KEY, 0);
        CurrentTheme = availableThemes[Mathf.Clamp(currentThemeIndex, 0, availableThemes.Count - 1)];
        ApplyTheme(CurrentTheme);
    }

    public void SetTheme(int themeIndex)
    {
        if (themeIndex >= 0 && themeIndex < availableThemes.Count)
        {
            CurrentTheme = availableThemes[themeIndex];
            PlayerPrefs.SetInt(CURRENT_THEME_KEY, themeIndex);
            ApplyTheme(CurrentTheme);
        }
    }

    private void ApplyTheme(Theme theme)
    {
        Camera.main.backgroundColor = theme.backgroundColor;

        OnThemeChanged?.Invoke(theme);
    }
}