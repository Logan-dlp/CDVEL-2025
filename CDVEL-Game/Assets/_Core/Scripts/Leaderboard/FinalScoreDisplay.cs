using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _player1Entry;
    [SerializeField] private ScoreScriptableObject _playerScores;

    private void Start()
    {
        DisplayFinalScores();
    }

    public void DisplayFinalScores()
    {
        var allScores = SerializeScore.GetAllScoreDescending();

        int player1Score = _playerScores.TotalScore;

        int player1Rank = GetRank(player1Score, allScores);

        UpdateScoreEntry(_player1Entry, player1Rank, "Entrez votre nom", player1Score);
    }

    private int GetRank(int score, Dictionary<int, Dictionary<string, float>> allScores)
    {
        int rank = 1;
        int countEqualScores = 0;

        foreach (var scoreEntry in allScores)
        {
            foreach (var playerScore in scoreEntry.Value)
            {
                if (playerScore.Value > score)
                {
                    rank++;
                }
                else if (playerScore.Value == score)
                {
                    countEqualScores++;
                }
            }
        }

        if (countEqualScores > 0)
        {
            rank += countEqualScores;
        }

        return rank;
    }

    private void UpdateScoreEntry(GameObject entry, int rank, string playerName, int score)
    {
        TextMeshProUGUI[] textComponents = entry.GetComponentsInChildren<TextMeshProUGUI>();

        if (textComponents.Length == 3)
        {
            textComponents[0].text = rank.ToString();
            textComponents[1].text = playerName;
            textComponents[2].text = score.ToString();
        }
        else
        {
            Debug.LogError("Le GameObject ne contient pas exactement trois TextMeshProUGUI.");
        }
    }

    private void OnApplicationQuit()
    {
        ResetPlayerScores();
    }

    private void ResetPlayerScores()
    {
        PlayerPrefs.SetInt("GameRestarted", 1);
        PlayerPrefs.Save();
    }
}