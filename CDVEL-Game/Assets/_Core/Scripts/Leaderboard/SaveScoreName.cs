using UnityEngine;

public class SaveScoreName : MonoBehaviour
{
    [SerializeField] private DisplayNameTeam playerNameDisplay;
    [SerializeField] private ScoreScriptableObject playerScores;
    [SerializeField] private DisplayHighScore highScoreDisplay;

    private bool scoresSaved = false;

    private void Start()
    {
        playerNameDisplay.ActivateInput();
    }

    private void Update()
    {
        if (!scoresSaved && playerNameDisplay.IsConfirmed)
        {
            SaveScores();
            scoresSaved = true;
            playerNameDisplay.DeactivateInput();

            highScoreDisplay.UpdateScoreDisplay();
        }
    }

    private void SaveScores()
    {
        Debug.Log("Sauvegarde des scores...");
        SerializeScore.AddScore(playerNameDisplay.PlayerName, playerScores.TotalScore);
        Debug.Log($"Score de {playerNameDisplay.PlayerName} : {playerScores.TotalScore}");
    }
}