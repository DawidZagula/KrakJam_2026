using UnityEngine;
using UnityEngine.UI;

public class NotStartedStateManager : MonoBehaviour
{
    [Header("Cache References")]
    [SerializeField] private Transform _notStartedUIContainer;
    [SerializeField] private Button _startButton;

    private void Start()
    {

        FadeTransitioner.Instance.FadeIn();

        _startButton.onClick.AddListener(() =>
        {
            _notStartedUIContainer.gameObject.SetActive(false);

            GameStateManager.Instance.ChangeCurrentGameState(GameState.Playing);
        });
    }

}
