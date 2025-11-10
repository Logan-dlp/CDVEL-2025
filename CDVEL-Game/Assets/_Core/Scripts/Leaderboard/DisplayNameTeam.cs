using TMPro;
using UnityEngine;

public class DisplayNameTeam : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameDisplay;
    public NameScriptableObject playerNameScriptableObject;
    private string currentName = "";
    public bool IsConfirmed { get; private set; }
    public bool IsInputActive { get; private set; }
    public string PlayerName => currentName;

    private const int maxCharacters = 10;

    private void Update()
    {
        if (IsInputActive && !IsConfirmed)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b' && currentName.Length > 0)
                {
                    currentName = currentName.Substring(0, currentName.Length - 1);
                }
                else if ((c == '\n' || c == '\r') && currentName.Length > 0)
                {
                    ConfirmName();
                    return;
                }
                else if (!char.IsControl(c) && currentName.Length < maxCharacters)
                {
                    currentName += c;
                }
            }
            playerNameDisplay.text = currentName;
        }
    }

    private void ConfirmName()
    {
        Debug.Log("Nom confirmé : " + currentName);
        playerNameDisplay.text = currentName;
        IsConfirmed = true;
        DeactivateInput();
    }

    public void ActivateInput()
    {
        if (!IsInputActive)
        {
            IsInputActive = true;
            currentName = "";
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