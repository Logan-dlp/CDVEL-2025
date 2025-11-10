using UnityEngine;

[CreateAssetMenu(fileName = "new_ScorePlayer", menuName = "Scriptable Object/new Score player")]
public class ScoreScriptableObject : ScriptableObject
{
    [SerializeField] private int _totalScore;

    public int TotalScore
    {
        get => _totalScore;
        set => _totalScore = value;
    }

    public void ResetScore()
    {
        _totalScore = 0;
    }
}