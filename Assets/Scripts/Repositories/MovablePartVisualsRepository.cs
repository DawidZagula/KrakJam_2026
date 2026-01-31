using UnityEngine;

public class MovablePartVisualsRepository : MonoBehaviour
{
    public static MovablePartVisualsRepository Instance { get; private set; }

    [SerializeField] private Sprite[] _floorPartSprites;
    [SerializeField] private Sprite[] _backroundPartSprites;
    [SerializeField] private Sprite[] _foregroundPartSprites;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetRandomPartSprite(MovableParts randomPartType)
    {
        int randomIndex;

        switch (randomPartType)
        {
            default:
            case MovableParts.Floor:
                randomIndex = Random.Range(0, _floorPartSprites.Length);
                return _floorPartSprites[randomIndex];

            case MovableParts.Background:
                randomIndex = Random.Range(0, _backroundPartSprites.Length);
                return _backroundPartSprites[randomIndex];

            case MovableParts.Foreground:
                randomIndex = Random.Range(0, _foregroundPartSprites.Length);
                return (_foregroundPartSprites[randomIndex]);
        }
    }
}
