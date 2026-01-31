using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private int score = 0;
    [SerializeField] private float scoreInterval = 0.5f;
    [SerializeField] private int pointsPerTick = 1;

    [Header("Score Multiplier")]
    [SerializeField] private float scoreAccumulationModifier = 0.7f;
    [SerializeField] private int pointMultiplierBetweenLevels = 2;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    public event Action<int> OnScoreChanged;

    private int levelScoreCounter = 0;
    public int CurrentScore => score;

    public bool IsCounting { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject); // odkomentuj jeśli potrzeba przechodzić między scenami
    }

    private void Start()
    {
        OnScoreChanged += UpdateScoreText;
        OnScoreChanged?.Invoke(score); // odświeżanie UI na starcie

        EnvironmentLevelManager.Instance.OnLevelChanged += UpdateScoreAccumulation;
    }

    private void OnDestroy()
    {
        OnScoreChanged -= UpdateScoreText;
        EnvironmentLevelManager.Instance.OnLevelChanged -= UpdateScoreAccumulation;
    }

    // --- API dla reszty gry ---

    public int GetScore()
    {
        return score;
    }

    public void StartScoring()
    {
        if (IsCounting) return;

        IsCounting = true;
        InvokeRepeating(nameof(TickScore), scoreInterval, scoreInterval);
    }

    public void StopScoring()
    {
        if (!IsCounting) return;

        IsCounting = false;
        CancelInvoke(nameof(TickScore));
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    //Naliczanie dodatkowych punktów. W taki sposób, bo tak.
    public void UpdateScoreAccumulation(object sender, EnvironmentLevelManager.OnLevelChangedEventArgs e)
    {
        pointsPerTick++;

        levelScoreCounter++;
        if (pointMultiplierBetweenLevels != 0)
        {
            if (levelScoreCounter % pointMultiplierBetweenLevels == 0)
            {
                double bonus = levelScoreCounter * 10 * scoreAccumulationModifier;
                score += (int)Math.Ceiling(bonus);
            }
        }

    }

    public void ResetScoreAccumulation()
    {
        scoreInterval = 1f;
    }

    // --- Wewnętrzna logika ---

    private void TickScore()
    {
        score += pointsPerTick;
        OnScoreChanged?.Invoke(score);
    }

    private void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
            scoreText.text = newScore.ToString();
    }
}

