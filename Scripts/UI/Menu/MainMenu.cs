using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(LoadingScreen))]

public class MainMenu : Menu
{
    [SerializeField] private SwitchLevelButton _buttonTemplate;
    [SerializeField] private Transform _switchLevelButtonsContainer;
    [SerializeField] private Button _firstSelectedButton;

    private int _newScene;
    private LoadingScreen _loadingScreen;
    private int _levelsCount;
    private int _levelComplete;
    private const string LevelComplete = "LevelComplete";

    private void Awake()
    {
        _loadingScreen = GetComponent<LoadingScreen>();
        _levelsCount = SceneManager.sceneCountInBuildSettings;
        _levelComplete = PlayerPrefs.GetInt(LevelComplete, 0);

        for (int levelNumber = 1; levelNumber < _levelsCount; levelNumber++)
        {
            AddButton(levelNumber);
        }

        SelectFirstButton();
    }

    private void AddButton(int levelNumber)
    {
        var button = Instantiate(_buttonTemplate, _switchLevelButtonsContainer);
        button.SwitchLevelButtonClick += OnSwitchLevelButtonClick;
        button.Render(levelNumber);
        button.name = _buttonTemplate.name + (_switchLevelButtonsContainer.childCount);

        ShowOpenLevels(button, levelNumber);
    }

    private void ShowOpenLevels(SwitchLevelButton button, int levelNumber)
    {
        if (_levelComplete + 2 > levelNumber)
        {
            button.GetComponent<Button>().interactable = true;
        }
        else
            button.GetComponent<Button>().interactable = false;
    }

    protected override void SelectFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton.gameObject);
    }

    private void OnSwitchLevelButtonClick(int levelNumber, SwitchLevelButton button)
    {
        LoadTo(levelNumber, button);
    }

    private void LoadTo(int levelNumber, SwitchLevelButton button)
    {
        PlayClickSound();
        _loadingScreen.LoadScene(levelNumber);
        button.SwitchLevelButtonClick -= OnSwitchLevelButtonClick;
    }

    public void LoadNewGame()
    {
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings)
        {
            PlayClickSound();
            RemoveAllData();
            CurrentScene++;
            _newScene = CurrentScene;
            _loadingScreen.LoadScene(_newScene);
        }
        else
            PlayErrorSound();
    }

    public void RemoveAllData()
    {
        PlayerPrefs.DeleteAll();
        Settings.ChangeVolume(Settings.Volume);
    }
}