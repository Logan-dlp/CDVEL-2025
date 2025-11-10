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

        [SerializeField] private KeyCode _skipKey = KeyCode.Percent;

        private TextMeshProUGUI _text;
        private Coroutine _hideCoroutine;
        private bool _isWaiting = false;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            Refresh();
        }

        private void Update()
        {
            if (!_isWaiting) return;

            if (Input.GetKeyDown(_skipKey))
                SkipToLeaderboard();
        }

        public void Refresh()
        {
            int score = PlayerPrefs.GetInt(_playerTag.ToString(), 0);
            _text.text = score.ToString();

            InvokeCallbacks(score, _playerTag);
        }

        private void InvokeCallbacks(int score, PlayerTag tag)
        {
            bool IsWinner(int s, PlayerTag t)
            {
                var scores = new int[Enum.GetValues(typeof(PlayerTag)).Length - 2];
                int j = 0;
                for (int i = 1; i < Enum.GetValues(typeof(PlayerTag)).Length; i++)
                {
                    if ((PlayerTag)i != t)
                    {
                        scores[j] = PlayerPrefs.GetInt(((PlayerTag)i).ToString());
                        ++j;
                    }
                }

                bool isWinner = true;
                foreach (int opponentScores in scores)
                {
                    if (opponentScores > s)
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
                StartHideCoroutineForList(_winObjects);
            }
            else
            {
                _OnGameOver?.Invoke();
                StartHideCoroutineForList(_loseObjects);
            }
        }

        private void StartHideCoroutineForList(List<GameObject> objects)
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }

            _hideCoroutine = StartCoroutine(HideObjectsThenShowLeaderboard(objects, _displayDuration));
        }

        private System.Collections.IEnumerator HideObjectsThenShowLeaderboard(List<GameObject> objects, float delay)
        {
            _isWaiting = true;

            float elapsed = 0f;
            while (elapsed < delay)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            foreach (var obj in objects)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            ShowLeaderboard();
            _isWaiting = false;
            _hideCoroutine = null;
        }

        private void SkipToLeaderboard()
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }

            foreach (var obj in _winObjects)
                if (obj != null) obj.SetActive(false);

            foreach (var obj in _loseObjects)
                if (obj != null) obj.SetActive(false);

            ShowLeaderboard();
            _isWaiting = false;
        }

        private void ShowLeaderboard()
        {
            if (_leaderboardUI != null)
                _leaderboardUI.SetActive(true);
        }
    }
}