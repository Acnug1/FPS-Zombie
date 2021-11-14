using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PlayerShooter))]

public class OpticalSight : MonoBehaviour
{
    [SerializeField] private float _targetMouseSensitivity; // чувствительность мыши в зуме
    [SerializeField] private float _targetFOV; // значение зума камеры при прицеливании
    [SerializeField] private float _speedSight; // скорость прицеливания
    [SerializeField] private PlayerController _playerController;

    private Camera _playerCamera; // камера игрока
    private bool _zoomStart;
    private float _zoomLevel;
    private float _defaultFOV;
    private float _mouseSensitivity;
    private float _mouseSensitivityDefault;
    private PlayerInput _input;
    private bool _isSniperRifle;

    public event UnityAction<bool> ZoomStarted;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.Aim.performed += ctx => Zoom();
    }

    private void OnDisable()
    {
        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.Aim.performed -= ctx => Zoom();
    }

    private void Start()
    {
        _playerCamera = GetComponent<Camera>();
        _defaultFOV = _playerCamera.fieldOfView; // значение зума камеры по умолчанию
        _mouseSensitivityDefault = _playerController.RotateSpeed; // значение чувствительности мыши по умолчанию

        if (_targetFOV > _defaultFOV)
            _targetFOV = _defaultFOV;

        if (_targetMouseSensitivity > _mouseSensitivityDefault)
            _targetMouseSensitivity = _mouseSensitivityDefault;

        ZoomDefault();
    }

    private void OnValidate()
    {
        if (_targetFOV < 20)
            _targetFOV = 20;

        if (_targetMouseSensitivity < 0.1f)
            _targetMouseSensitivity = 0.1f;

        if (_speedSight < 1)
            _speedSight = 1;
    }

    private void Update()
    {
        if (_playerCamera.fieldOfView != _zoomLevel)
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, _zoomLevel, _speedSight * Time.deltaTime);
    }

    private void ZoomDefault()
    {
        _zoomLevel = _defaultFOV; // делаем значения зума по умолчанию
        _mouseSensitivity = _mouseSensitivityDefault; // делаем значения чувствительности мыши по умолчанию
    }

    public void Zoom()
    {
        if (Time.timeScale <= 0)
            return;

        if (!_zoomStart) // если прицел не был включен ранее
        {
            // стартовые, зум и чувствительность мыши, после включения прицела
            _zoomStart = true; // включаем прицел

            if (_isSniperRifle)
            {
                _zoomLevel = _targetFOV / 2; // делаем зум прицела в 2 раза больше стандартного увеличения
                _mouseSensitivity = _targetMouseSensitivity / 2; // уменьшаем значение чувствительности мыши в 2 раза
            }
            else
            {
                _zoomLevel = _targetFOV; // делаем зум в прицеле
                _mouseSensitivity = _targetMouseSensitivity; // уменьшаем значение чувствительности мыши
            }

            ZoomStarted?.Invoke(_zoomStart);
        }
        else
        {
            _zoomStart = false;
            ZoomDefault();
            ZoomStarted?.Invoke(_zoomStart);
        }

        _playerController.RotateSpeedChange(_mouseSensitivity); // передаем значение чувствительности мыши в _playerController
    }

    public bool TryGetSniperRifle(bool isSniperRifle)
    {
        return _isSniperRifle = isSniperRifle;
    }
}
