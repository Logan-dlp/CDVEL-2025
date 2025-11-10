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

        [SerializeField] private float _displayDuration = 3f;
        [SerializeField] private List<GameObject> _winObjects = new List<GameObject>();
        [SerializeField] private List<GameObject> _loseObjects = new List<GameObject>();

        [SerializeField] private GameObject _leaderboardUI;

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
            {
                _OnGameWined?.Invoke();
                foreach (var winObj in _winObjects)
                {
                    if (winObj != null)
                        StartCoroutine(HideAfterDelay(winObj, _displayDuration));
                }
            }
            else
            {
                _OnGameOver?.Invoke();
                foreach (var loseObj in _loseObjects)
                {
                    if (loseObj != null)
                        StartCoroutine(HideAfterDelay(loseObj, _displayDuration));
                }
            }
        }

        private System.Collections.IEnumerator HideAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);

            if (_leaderboardUI != null)
            {
                _leaderboardUI.SetActive(true);
            }
        }
    }
}