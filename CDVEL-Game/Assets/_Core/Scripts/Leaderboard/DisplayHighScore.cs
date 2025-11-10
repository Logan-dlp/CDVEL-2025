using TMPro;
using UnityEngine;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Transform scoreListParent;
    [SerializeField] private RectTransform contentRectTransform;

    private const float scoreEntryHeight = 76f;

    public static int Player1Rank { get; private set; }
    public static int Player2Rank { get; private set; }

    private void Start()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        // 1️⃣ Effacer toutes les entrées existantes avant d’afficher les nouvelles
        foreach (Transform child in scoreListParent)
        {
            Destroy(child.gameObject);
        }

        var allScores = SerializeScore.GetAllScoreDescending();

        if (allScores != null && allScores.Count > 0)
        {
            int rank = 1;
            int totalEntries = 0;

            foreach (var scoreEntry in allScores)
            {
                foreach (var playerScore in scoreEntry.Value)
                {
                    CreateScoreEntry(rank, playerScore.Key, playerScore.Value);
                    rank++;
                    totalEntries++;
                }
            }

            AdjustContentSize(totalEntries);
        }
        else
        {
            // Message vide si aucun score
            CreateScoreEntry(0, "Aucun score enregistré.", 0);
            AdjustContentSize(1);
        }
    }


    private void CreateScoreEntry(int rank, string playerName, float score)
    {
        GameObject scoreEntryObject = Instantiate(scoreEntryPrefab, scoreListParent);
        TextMeshProUGUI[] textComponents = scoreEntryObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (textComponents.Length >= 3)
        {
            textComponents[0].text = rank > 0 ? rank.ToString() : "";
            textComponents[1].text = playerName;
            textComponents[2].text = score > 0 ? score.ToString() : "";
        }
    }

    private void AdjustContentSize(int entryCount)
    {
        float newHeight = scoreEntryHeight * entryCount + 100;
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, newHeight);
    }

    [ContextMenu("Clear Entire Leaderboard")]
    private void ClearEntireLeaderboard()
    {
        SerializeScore.ClearAllScore();
        UpdateScoreDisplay();

        Debug.Log("Leaderboard completely cleared!");
    }
}