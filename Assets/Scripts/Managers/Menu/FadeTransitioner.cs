using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransitioner : MonoBehaviour
{
    public static FadeTransitioner Instance { get; private set; }

    [SerializeField] private Image _imageToFade;

    [SerializeField] private float _maxDuration;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeIn(Action onFinish = null)
    {
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 0f, onFinish));
    }

    public void FadeOut(Action onFinish = null)
    {
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 1f, onFinish));
    }

    private IEnumerator FadeRoutine(Image imageToFade, float startAlpha, float targetAlpha, Action onFinish = null)
    {
        float alphaDistance = Mathf.Abs(targetAlpha - startAlpha);

        if (alphaDistance <= Mathf.Epsilon)
            yield break;

        float fadeDuration = _maxDuration * alphaDistance;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            Color color = imageToFade.color;
            color.a = currentAlpha;
            imageToFade.color = color;

            yield return null;
        }

        Color finalColor = imageToFade.color;
        finalColor.a = targetAlpha;
        imageToFade.color = finalColor;

        if (onFinish != null)
        {
            onFinish();
        }
    }
}
