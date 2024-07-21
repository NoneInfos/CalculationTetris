using UnityEngine;
using TMPro;

public class AchievementNotificationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetAchievement(Achievement achievement)
    {
        titleText.text = achievement.title;
        descriptionText.text = achievement.description;
    }
}