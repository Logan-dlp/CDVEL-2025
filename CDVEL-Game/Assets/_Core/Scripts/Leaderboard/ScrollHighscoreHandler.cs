using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHighscoreHandler : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar; // Référence à votre scrollbar
    [SerializeField] private RectTransform content; // Référence à la RectTransform de votre contenu
    [SerializeField] private float baseScrollSpeed = 1f; // Vitesse de défilement de base
    private bool isScrolling = false;
    private float direction = 1f; // 1 pour défilement vers le haut, -1 pour défilement vers le bas

    private void Start()
    {
        StartScrolling();
    }

    public void StartScrolling()
    {
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollCoroutine());
        }
    }

    public void StopScrolling()
    {
        if (isScrolling)
        {
            isScrolling = false;
            StopCoroutine(ScrollCoroutine());
        }
    }

    private IEnumerator ScrollCoroutine()
    {
        float targetValue = 1f; // Valeur cible initiale (commence en haut)
        while (isScrolling)
        {
            // Obtenir la hauteur du contenu et la hauteur de la zone visible (viewport)
            float contentHeight = content.rect.height;
            float viewportHeight = scrollbar.GetComponentInParent<ScrollRect>().viewport.rect.height;

            // Calculer la vitesse de défilement en fonction de la taille du contenu
            float scrollSpeed = baseScrollSpeed * (viewportHeight / contentHeight);

            // Mettre à jour la valeur cible en ajoutant la vitesse ajustée
            targetValue += direction * scrollSpeed * Time.deltaTime;

            // Vérifier les limites et inverser la direction
            if (targetValue >= 1f)
            {
                targetValue = 1f; // Limite supérieure
                direction = -1f; // Changer de direction
            }
            else if (targetValue <= 0f)
            {
                targetValue = 0f; // Limite inférieure
                direction = 1f; // Changer de direction
            }

            // Appliquer la nouvelle valeur à la scrollbar
            scrollbar.value = targetValue;

            yield return null; // Attendre la prochaine image
        }
    }
}
