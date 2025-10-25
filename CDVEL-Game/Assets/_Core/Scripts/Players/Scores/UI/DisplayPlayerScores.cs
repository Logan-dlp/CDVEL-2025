using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Players
{
    using UI;
    
    public class DisplayPlayerScores : MonoBehaviour, IDisplay
    {
        [SerializeField] private PlayerTag _playerTag;
        [SerializeField] private UnityEvent _OnGameWined;
        [SerializeField] private UnityEvent _OnGameOver;
        
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
            
            InvokeCallbacks(score, _playerTag);
        }

        private void InvokeCallbacks(int score, PlayerTag tag)
        {
            bool IsWinner(int score, PlayerTag tag)
            {
                var scores = new int[Enum.GetValues(typeof(PlayerTag)).Length - 2];
                // Starts with a 1 because the first one is "None".
                int j = 0;
                for (int i = 1; i < Enum.GetValues(typeof(PlayerTag)).Length; i++)
                {
                    if ((PlayerTag)i != tag)
                    {
                        scores[j] = PlayerPrefs.GetInt(((PlayerTag)i).ToString());
                        ++j;
                    }
                }

                bool isWinner = true;

                foreach (int opponentScores in scores)
                {
                    if (opponentScores > score)
                    {
                        isWinner = false;
                        break;
                    }
                }

                return isWinner;
            }

            if (IsWinner(score, tag))
                _OnGameWined?.Invoke();
            else
                _OnGameOver?.Invoke();
        }
    }
}