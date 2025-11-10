using UnityEngine;
using System.Linq;
using Players; // si tes PlayerScoreHandler sont là

public class SaveTotalScore : MonoBehaviour
{
    [SerializeField] private ScoreScriptableObject _scoreScriptable;

    public void SaveScoreTotal()
    {
        // Récupère tous les ScoreHandler dans la scène
        var allScoreHandlers = FindObjectsOfType<Scores.ScoreHandler>();

        if (allScoreHandlers == null || allScoreHandlers.Length == 0)
        {
            Debug.LogWarning("Aucun ScoreHandler trouvé dans la scène !");
            return;
        }

        // Additionne tous les scores
        int total = allScoreHandlers.Sum(handler => handler.GetScore());

        // Sauvegarde dans le ScriptableObject
        _scoreScriptable.TotalScore = total;

        Debug.Log($"Total score saved in ScriptableObject: {total}");
    }

    public void Start()
    {
        _scoreScriptable.ResetScore();
        Debug.Log("Total score reset in ScriptableObject");
    }
}