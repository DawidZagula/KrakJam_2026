using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Cached Refereces")]
    [SerializeField] private DifficultySelectionUI _difficultySelectionUI;

    [Header("Configuration")]
    [SerializeField] private string _difficultyDescriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _difficultySelectionUI.DisplayDifficultyDescriptionText(_difficultyDescriptionText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _difficultySelectionUI.DisplayDifficultyDescriptionText(string.Empty);
    
    }
}
