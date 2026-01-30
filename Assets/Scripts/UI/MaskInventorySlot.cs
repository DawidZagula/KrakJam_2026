using UnityEngine;
using UnityEngine.UI;

public class MaskInventorySlot : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private Image _highlightImage;

    [Header("Configuration")]
    [SerializeField] private Image _maskImage;
    [SerializeField] private Mask _mask;

    public void Select()
    {
        _highlightImage.enabled = true;

        PlayerMask.Instance.SetPlayerMask(_mask);
    }

    public void Deselect()
    {
        _highlightImage.enabled = false;
    }
}
