using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHighscoreHandler : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private RectTransform _content;
    [SerializeField] private float _baseScrollSpeed = 1f;
    private bool _isScrolling = false;
    private float _direction = 1f;

    private void Start()
    {
        StartScrolling();
    }

    public void StartScrolling()
    {
        if (!_isScrolling)
        {
            _isScrolling = true;
            StartCoroutine(ScrollCoroutine());
        }
    }

    public void StopScrolling()
    {
        if (_isScrolling)
        {
            _isScrolling = false;
            StopCoroutine(ScrollCoroutine());
        }
    }

    private IEnumerator ScrollCoroutine()
    {
        float targetValue = 1f;
        while (_isScrolling)
        {
            float contentHeight = _content.rect.height;
            float viewportHeight = _scrollbar.GetComponentInParent<ScrollRect>().viewport.rect.height;

            float scrollSpeed = _baseScrollSpeed * (viewportHeight / contentHeight);

            targetValue += _direction * scrollSpeed * Time.deltaTime;

            if (targetValue >= 1f)
            {
                targetValue = 1f;
                _direction = -1f;
            }
            else if (targetValue <= 0f)
            {
                targetValue = 0f;
                _direction = 1f;
            }

            _scrollbar.value = targetValue;

            yield return null;
        }
    }
}
