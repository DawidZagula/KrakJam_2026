using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTransitioner : MonoBehaviour
{
    public static FadeTransitioner Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private Image _imageToFade;
    [SerializeField] private float _maxDuration;
    [SerializeField] private bool _shouldFadeInAtSceneLoad = true;
    [Space]
    [SerializeField] private bool _shouldSetDefaultFadeColour;
    [SerializeField] private Color _defaultFadeColour;


    private void Awake()
    {
        Instance = this;

        if (_shouldSetDefaultFadeColour)
        {
            SetFadeColor(_defaultFadeColour);
        }

        if (_shouldFadeInAtSceneLoad)
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        if (arg0.buildIndex == 0)
        {
            StartCoroutine(FadeInNextFrame());
        }
    }

    private IEnumerator FadeInNextFrame()
    {
        //waiting for the first frame to be rendered (and Canvas with it)
        yield return new WaitForEndOfFrame();
        FadeIn();
    }

    public void FadeIn(Action onFinish = null)
    {
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 0f, onFinish));
    }
    
    public void FadeIn(Color fadeColour, Action onFinish = null)
    {
        SetFadeColor(fadeColour);
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 0f, onFinish));
    }

    public void FadeIn(Color fadeColour, float maxDuration, Action onFinish = null)
    {
        SetFadeColor(fadeColour);
        SetMaxDuration(maxDuration);
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 0f, onFinish));
    }

    public void FadeOut(Action onFinish = null)
    {
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 1f, onFinish));
    }

    public void FadeOut(Color fadeColour, Action onFinish = null)
    {
        SetFadeColor(fadeColour);
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 1f, onFinish));
    }

    public void FadeOut(Color fadeColour, float maxDuration, Action onFinish = null)
    {
        SetFadeColor(fadeColour);
        SetMaxDuration(maxDuration);
        StartCoroutine(FadeRoutine(_imageToFade, _imageToFade.color.a, 1f, onFinish));
    }

    public void SetFadeImageToDefaultFaded()
    {
        Color defaultFaded = _defaultFadeColour;
        defaultFaded.a = 1f;
        
        _imageToFade.color = defaultFaded;
    }

    private void SetFadeColor(Color color)
    {
        Color current = _imageToFade.color;
        current.r = color.r;
        current.g = color.g;
        current.b = color.b;
        _imageToFade.color = current;
    }


    private void SetMaxDuration(float maxDuration)
    {
        _maxDuration = maxDuration;
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
