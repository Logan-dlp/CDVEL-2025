using UnityEngine;
using TMPro;

namespace Players
{
    using UI;
    
    public class DisplayPlayerScores : MonoBehaviour, IDisplay
    {
        [SerializeField] private PlayerTag _playerTag;
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            Refresh();
        }

        public void Refresh()
        {
            int score = PlayerPrefs.GetInt(_playerTag.ToString(), 0);
            _text.text = score.ToString();
        }
    }
}