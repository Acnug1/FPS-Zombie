using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(LoadingScreen))]

public class NextLevelMenu : Menu
{
    [SerializeField] private AudioClip _levelFinishSound;
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private float _openMenuDelay;
    [SerializeField] private Button _firstSelectedButton;

    private LoadingScreen _loadingScreen;
    private int _newScene;

    private void Awake()
    {
        _loadingScreen = GetComponent<LoadingScreen>();
        _nextLevelMenu.SetActive(false);
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
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings) // если номер следующей сцены не больше, чем общее количество сцен
        {
            PlayClickSound();
            CurrentScene++; // проматываем уровень на следующий
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
        PlayEndLevelSound();

        SetTouchActive(false);

        _nextLevelMenu.SetActive(true);
        Time.timeScale = 0;

        SelectFirstButton();
    }

    protected override void SelectFirstButton()
    {
        // очистим выбранный объект в EventSystem
        EventSystem.current.SetSelectedGameObject(null);

        // установим новый выбранный объект
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton.gameObject);
    }

    private void PlayEndLevelSound()
    {
        AudioSource.PlayOneShot(_levelFinishSound, Settings.Volume);
    }
}