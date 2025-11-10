using TMPro;
using UnityEngine;

public class DisplayNameTeam : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameDisplay;
    public NameScriptableObject PlayerNameScriptableObject;
    public bool IsConfirmed { get; private set; }
    public bool IsInputActive { get; private set; }
    public string PlayerName => _currentName;

    private string _currentName = "";
    private const int _maxCharacters = 10;

    private void Update()
    {
        if (IsInputActive && !IsConfirmed)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b' && _currentName.Length > 0)
                {
                    _currentName = _currentName.Substring(0, _currentName.Length - 1);
                }
                else if ((c == '\n' || c == '\r') && _currentName.Length > 0)
                {
                    ConfirmName();
                    return;
                }
                else if (!char.IsControl(c) && _currentName.Length < _maxCharacters)
                {
                    _currentName += c;
                }
            }
            _playerNameDisplay.text = _currentName;
        }
    }

    private void ConfirmName()
    {
        Debug.Log("Nom confirmé : " + _currentName);
        _playerNameDisplay.text = _currentName;
        IsConfirmed = true;
        DeactivateInput();
    }

    public void ActivateInput()
    {
        if (!IsInputActive)
        {
            IsInputActive = true;
            _currentName = "";
            IsConfirmed = false;
            Debug.Log("Saisie activée pour le joueur actuel.");
        }
    }

    public void DeactivateInput()
    {
        IsInputActive = false;
        Debug.Log("Saisie désactivée pour le joueur.");
    }
}