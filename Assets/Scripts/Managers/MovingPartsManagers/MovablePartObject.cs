using UnityEngine;

public class MovablePartObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetRandomSprite(MovableParts partType)
    {
        _spriteRenderer.sprite = MovablePartVisualsRepository.Instance.GetRandomPartSprite(partType);

        _spriteRenderer.sortingOrder = GetSortingOrder(partType);
    }

    public void SetEdgeSprite()
    {
        _spriteRenderer.sprite = MovablePartVisualsRepository.Instance.GetEdgeSprite();

        _spriteRenderer.sortingOrder = GetSortingOrder(MovableParts.Edge);
    }

    private int GetSortingOrder(MovableParts movablePartType)
    {
        switch(movablePartType)
        {
            default:
            case MovableParts.Floor:

                return 1;

            case MovableParts.Background:

                return -1;

            case MovableParts.Foreground:

                return 4;

            case MovableParts.Edge:

                return 5;
        }
    }
}
