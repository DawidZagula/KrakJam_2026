using System.Collections.Generic;
using UnityEngine;

public class MovablePartVisualsRepository : MonoBehaviour
{
    public static MovablePartVisualsRepository Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private Sprite[] _floorPartSprites_TestLevel0;
    [SerializeField] private Sprite[] _backroundPartSprites_TestLevel0;
    [SerializeField] private Sprite[] _foregroundPartSprites_TestLevel0;
    [Space]
    [SerializeField] private Sprite[] _floorPartSprites_TestLevel1;
    [SerializeField] private Sprite[] _backroundPartSprites_TestLevel1;
    [SerializeField] private Sprite[] _foregroundPartSprites_TestLevel1;
    [Space]
    [SerializeField] private Sprite[] _floorPartSprites_TestLevel2;
    [SerializeField] private Sprite[] _backroundPartSprites_TestLevel2;
    [SerializeField] private Sprite[] _foregroundPartSprites_TestLevel2;

    private readonly List<Sprite> _currentLevelFloorPartSprites = new List<Sprite>();
    private readonly List<Sprite> _currentLevelBackgroundSprites = new List<Sprite>();
    private readonly List<Sprite> _currentLevelForegroundSprites = new List<Sprite>();

    private void Awake()
    {
        Instance = this;

        UpdateCurrentSpriteLists(EnvironmentLevel.TestLevel0);
    }   

    private void Start()
    {
        EnvironmentLevelManager.Instance.OnLevelChanged += EnvironmentLevelManager_OnLevelChanged;

    }

    private void OnDestroy()
    {
        EnvironmentLevelManager.Instance.OnLevelChanged -= EnvironmentLevelManager_OnLevelChanged;
    }

    private void EnvironmentLevelManager_OnLevelChanged(object sender, EnvironmentLevelManager.OnLevelChangedEventArgs e)
    {
        UpdateCurrentSpriteLists(e.NewEnvironmentLevel);
    }

    private void UpdateCurrentSpriteLists(EnvironmentLevel currentEnvironmentLevel)
    {
        _currentLevelFloorPartSprites.Clear();
        _currentLevelBackgroundSprites.Clear();
        _currentLevelForegroundSprites.Clear();

        switch (currentEnvironmentLevel)
        {
            default:
            case EnvironmentLevel.TestLevel0:

                foreach (Sprite floorSprite in _floorPartSprites_TestLevel0)
                {
                    _currentLevelFloorPartSprites.Add(floorSprite);
                }
                foreach (Sprite backgroundSprite  in _backroundPartSprites_TestLevel0)
                {
                    _currentLevelBackgroundSprites.Add(backgroundSprite);
                }
                foreach (Sprite foregroundSprite in _foregroundPartSprites_TestLevel0)
                {
                    _currentLevelForegroundSprites.Add(foregroundSprite);
                }

                break;

            case EnvironmentLevel.TestLevel1:
                foreach (Sprite floorSprite in _floorPartSprites_TestLevel1)
                {
                    _currentLevelFloorPartSprites.Add(floorSprite);
                }
                foreach (Sprite backgroundSprite in _backroundPartSprites_TestLevel1)
                {
                    _currentLevelBackgroundSprites.Add(backgroundSprite);
                }
                foreach (Sprite foregroundSprite in _foregroundPartSprites_TestLevel1)
                {
                    _currentLevelForegroundSprites.Add(foregroundSprite);
                }


                break;

            case EnvironmentLevel.TestLevel2:
                foreach (Sprite floorSprite in _floorPartSprites_TestLevel2)
                {
                    _currentLevelFloorPartSprites.Add(floorSprite);
                }
                foreach (Sprite backgroundSprite in _backroundPartSprites_TestLevel2)
                {
                    _currentLevelBackgroundSprites.Add(backgroundSprite);
                }
                foreach (Sprite foregroundSprite in _foregroundPartSprites_TestLevel2)
                {
                    _currentLevelForegroundSprites.Add(foregroundSprite);
                }


                break;
        }
    }

    public Sprite GetRandomPartSprite(MovableParts randomPartType)
    {
        int randomIndex;

        switch (randomPartType)
        {
            default:
            case MovableParts.Floor:
                randomIndex = Random.Range(0, _currentLevelFloorPartSprites.Count);
                return _currentLevelFloorPartSprites[randomIndex];

            case MovableParts.Background:
                randomIndex = Random.Range(0, _currentLevelBackgroundSprites.Count);
                return _currentLevelBackgroundSprites[randomIndex];

            case MovableParts.Foreground:
                randomIndex = Random.Range(0, _currentLevelForegroundSprites.Count);
                return (_currentLevelForegroundSprites[randomIndex]);
        }
    }
}
