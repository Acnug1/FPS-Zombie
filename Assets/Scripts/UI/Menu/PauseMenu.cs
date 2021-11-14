using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] private Player _player;
    [SerializeField] private OpticalSight _opticalSight;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Shop _shop;
    [SerializeField] private Button _firstSelectedPauseMenuButton;
    [SerializeField] private Button _firstSelectedShopButton;

    private bool _isZoom;
    private PlayerInput _input;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _shop.gameObject.SetActive(false);
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _opticalSight.ZoomStarted += OnZoomStarted;
        _shop.WeaponSold += OnWeaponSold;

        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.OpenMenu.performed += ctx => OpenPanel(_pauseMenu);
        _input.Player.OpenShop.performed += ctx => OpenPanel(_shop.gameObject);
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _opticalSight.ZoomStarted -= OnZoomStarted;
        _shop.WeaponSold -= OnWeaponSold;

        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.OpenMenu.performed -= ctx => OpenPanel(_pauseMenu);
        _input.Player.OpenShop.performed -= ctx => OpenPanel(_shop.gameObject);
    }

    private void OnPlayerDied()
    {
        if (!_pauseMenu.activeSelf)
            OpenPanel(_pauseMenu);
    }

    private void OnZoomStarted(bool isZoom)
    {
        _isZoom = isZoom;
    }

    public void OpenPanel(GameObject panel)
    {
        if (_isZoom || panel.TryGetComponent(out Shop shop) && _player.CurrentHealth <= 0 || _shop.gameObject.activeSelf || _pauseMenu.activeSelf)
            PlayErrorSound();
        else
        {
            PlayClickSound();

            SetTouchActive(false);

            panel.SetActive(true);
            Time.timeScale = 0;

            SelectFirstButton();
        }
    }

    private void OnWeaponSold()
    {
        SelectFirstButton();
    }

    protected override void SelectFirstButton()
    {
        // очистим выбранный объект в EventSystem
        EventSystem.current.SetSelectedGameObject(null);

        // установим новый выбранный объект
        if (_shop.gameObject.activeSelf)
            EventSystem.current.SetSelectedGameObject(_firstSelectedShopButton.gameObject);
        else if (_pauseMenu.activeSelf)
            EventSystem.current.SetSelectedGameObject(_firstSelectedPauseMenuButton.gameObject);
    }

    public void ClosePanel(GameObject panel)
    {
        if (_player.CurrentHealth > 0)
        {
            PlayClickSound();

            SetTouchActive(true);

            panel.SetActive(false);
            Time.timeScale = 1;
        }
        else
            PlayErrorSound();
    }
}
