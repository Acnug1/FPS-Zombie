using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(LoadingScreen))]

public class NextLevelMenu : Menu
{
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private float _openMenuDelay;
    [SerializeField] private Button _firstSelectedButton;

    private LoadingScreen _loadingScreen;
    private int _newScene;

    private void Awake()
    {
        _nextLevelMenu.SetActive(false);
        _loadingScreen = GetComponent<LoadingScreen>();
    }

    private void OnEnable()
    {
        _endPoint.Reached += OnEndPointReached;
    }

    private void OnDisable()
    {
        _endPoint.Reached -= OnEndPointReached;
    }

    public void LoadNextLevel()
    {
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings)
        {
            PlayClickSound();
            CurrentScene++;
            _newScene = CurrentScene;
            Time.timeScale = 1;
            _loadingScreen.LoadScene(_newScene);
        }
        else
            PlayErrorSound();
    }

    private void OnEndPointReached(Player player)
    {
        Invoke(nameof(OpenMenu), _openMenuDelay);
    }

    private void OpenMenu()
    {
        PlayClickSound();

        SetTouchActive(false);

        _nextLevelMenu.SetActive(true);
        Time.timeScale = 0;

        SelectFirstButton();
    }

    protected override void SelectFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(_firstSelectedButton.gameObject);
    }
}