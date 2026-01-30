using UnityEngine;
using UnityEngine.UI;

public class FloorPartObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }
}
