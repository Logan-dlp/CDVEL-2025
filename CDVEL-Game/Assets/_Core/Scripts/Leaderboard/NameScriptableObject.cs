using UnityEngine;

[CreateAssetMenu(fileName = "new_NamePlayer", menuName = "Scriptable Object/new Name player")]
public class NameScriptableObject : ScriptableObject
{
    [SerializeField] private string _playerName;

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }
}