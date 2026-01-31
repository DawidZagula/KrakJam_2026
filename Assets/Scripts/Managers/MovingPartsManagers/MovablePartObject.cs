using UnityEngine;
using UnityEngine.UI;

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
    }

}
