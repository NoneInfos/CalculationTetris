using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject entryPrefab;

    public void ShowLeaderboard()
    {
        ClearLeaderboard();
        var entries = LeaderboardManager.Instance.GetLeaderboard();

        foreach (var entry in entries)
        {
            GameObject entryGO = Instantiate(entryPrefab, entryContainer);
            entryGO.GetComponentInChildren<TextMeshProUGUI>(true).text = entry.playerName;
            entryGO.GetComponentsInChildren<TextMeshProUGUI>(true)[1].text = entry.score.ToString();
            entryGO.GetComponentsInChildren<TextMeshProUGUI>(true)[2].text = entry.date.ToShortDateString();
        }
    }

    private void ClearLeaderboard()
    {
        foreach (Transform child in entryContainer)
        {
            Destroy(child.gameObject);
        }
    }
}