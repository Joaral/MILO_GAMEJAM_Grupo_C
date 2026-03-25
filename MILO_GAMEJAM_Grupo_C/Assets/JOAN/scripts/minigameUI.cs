using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class minigameUI : MonoBehaviour
{
    public minigame minigame;

    [Header("UI Settings")]
    public Image ObjectScale;
    public Image lastObject;
    public GameObject minigamePanel;

    private Vector3 originalPosition;

    public bool isFirst;

    private bool isAnimating = false;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = ObjectScale.transform.localScale;
        originalPosition = ObjectScale.rectTransform.position;
    }

    void Update()
    {
        if (!minigame.IsPlaying())
        {
            minigamePanel.SetActive(false);
            return;
        }

        minigamePanel.SetActive(true);
        UpdateBar();
    }

    void UpdateBar()
    {
        float value = minigame.GetChargeValue();
        ObjectScale.transform.localScale = initialScale * value;
    }
    public void SaveLast()
    {
        // Copiar escala
        lastObject.transform.localScale = ObjectScale.transform.localScale;

        // Copiar sprite
        lastObject.sprite = ObjectScale.sprite;

        //ObjectScale.sprite = sprites[Random.Range(0, sprites.Length)];

        // Resetear el actual
        ObjectScale.transform.localScale = Vector3.zero;
    }
    public void AnimateToLast()
    {
        if (isAnimating) return;

        StopAllCoroutines();
        StartCoroutine(MoveToLast());
    }

    IEnumerator MoveToLast()
    {
        isAnimating = true;

        RectTransform current = ObjectScale.rectTransform;
        RectTransform target = lastObject.rectTransform;

        Vector3 startPos = originalPosition;
        Vector3 endPos = target.position;

        Vector3 startScale = current.localScale;
        Vector3 endScale = target.localScale; // opcional

        float duration = 0.25f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            // suavizado (ease out)
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            current.position = Vector3.Lerp(startPos, endPos, t);
            current.localScale = Vector3.Lerp(startScale, endScale, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Asegurar valores finales
        current.position = endPos;

        // Guardar resultado en lastObject
        lastObject.sprite = ObjectScale.sprite;
        target.localScale = startScale;

        // Resetear actual
        current.localScale = Vector3.zero;

        // IMPORTANTE: devolver el actual a su sitio original
        current.position = originalPosition;

        isAnimating = false;
    }

    public Vector3 GetInitialScale()
    {
        return initialScale;
    }
}
