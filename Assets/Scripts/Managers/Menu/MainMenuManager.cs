using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private Transform _mainMenuButtonContainer;
    [SerializeField] private Transform _creditsContainer;
    [SerializeField] private GameObject _quitConfirmContainer;
    [SerializeField] private Transform _difficultySelectionContainer;
    [Space]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _openCreditsMenuButton;
    [SerializeField] private Button _openQuitConfirmMenuButton;
    [Space]
    [SerializeField] private Button _backFromCreditsButton;
    [SerializeField] private Button _backFromQuitConfirmMenuButton;
    [SerializeField] private Button _backFromDifficultySelectionButton;
    [Space]
    [SerializeField] private Button _quitButton;


    private void Start()
    {
        AssignButtons();
    }

    private void AssignButtons()
    {
        //_startGameButton.onClick.AddListener(() =>
        //{
        //    SceneLoader.Instance.ProcessLoadScene(Scenes.GameScene, true);
        //});

        _startGameButton.onClick.AddListener(() =>
        {
            _mainMenuButtonContainer.gameObject.SetActive(false);
            _difficultySelectionContainer.gameObject.SetActive(true);
        });

        _backFromDifficultySelectionButton.onClick.AddListener(() =>
        {
            _difficultySelectionContainer.gameObject.SetActive(false);
            _mainMenuButtonContainer.gameObject.SetActive(true);
        });

        _openCreditsMenuButton.onClick.AddListener(() =>
        {
            _mainMenuButtonContainer.gameObject.SetActive(false);
            _creditsContainer.gameObject.SetActive(true);
        });

        _openQuitConfirmMenuButton.onClick.AddListener(() =>
        {
            _mainMenuButtonContainer.gameObject.SetActive(false);
            _quitConfirmContainer.gameObject.SetActive(true);
        });

        _backFromCreditsButton.onClick.AddListener(() =>
        {
            _creditsContainer.gameObject.SetActive(false);
            _mainMenuButtonContainer.gameObject.SetActive(true);
        });

        _backFromQuitConfirmMenuButton.onClick.AddListener(() =>
        {
            _quitConfirmContainer.gameObject.SetActive(false);
            _mainMenuButtonContainer.gameObject.SetActive(true);
        });

        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
