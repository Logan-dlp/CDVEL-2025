using UnityEngine;

public class SaveScoreName : MonoBehaviour
{
    [SerializeField] private DisplayNameTeam _playerNameDisplay;
    [SerializeField] private ScoreScriptableObject _playerScores;
    [SerializeField] private DisplayHighScore _highScoreDisplay;

    private bool scoresSaved = false;

    private void Start()
    {
        _playerNameDisplay.ActivateInput();
    }

    private void Update()
    {
        if (!scoresSaved && _playerNameDisplay.IsConfirmed)
        {
            SaveScores();
            scoresSaved = true;
            _playerNameDisplay.DeactivateInput();

            _highScoreDisplay.UpdateScoreDisplay();
        }
    }

    private void SaveScores()
    {
        Debug.Log("Sauvegarde des scores...");
        SerializeScore.AddScore(_playerNameDisplay.PlayerName, _playerScores.TotalScore);
        Debug.Log($"Score de {_playerNameDisplay.PlayerName} : {_playerScores.TotalScore}");
    }
}