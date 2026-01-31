using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private int score = 0;
    [SerializeField] private float scoreInterval = 1f;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        // Singleton (opcjonalny, ale praktyczny)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Podpinamy event do funkcji aktualizującej tekst
        OnScoreChanged += UpdateScoreText;

        InvokeRepeating(nameof(IncreaseScore), scoreInterval, scoreInterval);
    }

    private void OnDestroy()
    {
        OnScoreChanged -= UpdateScoreText;
    }

    private void IncreaseScore()
    {
        score++;
        OnScoreChanged?.Invoke(score);
    }

    private void UpdateScoreText(int newScore)
    {
        if (scoreText == null) return;

        scoreText.text = newScore.ToString();
    }
}
