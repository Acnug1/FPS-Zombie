using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PlayerShooter))]

public class OpticalSight : MonoBehaviour
{
    [SerializeField] private float _targetMouseSensitivity;
    [SerializeField] private float _targetFOV;
    [SerializeField] private float _speedSight;
    [SerializeField] private PlayerController _playerController;

    private Camera _playerCamera;
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
        _input.Enable();

        _input.Player.Aim.performed += ctx => Zoom();
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Aim.performed -= ctx => Zoom();
    }

    private void Start()
    {
        _playerCamera = GetComponent<Camera>();
        _defaultFOV = _playerCamera.fieldOfView;
        _mouseSensitivityDefault = _playerController.RotateSpeed;

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
        _zoomLevel = _defaultFOV;
        _mouseSensitivity = _mouseSensitivityDefault;
    }

    public void Zoom()
    {
        if (Time.timeScale <= 0)
            return;

        if (!_zoomStart)
        {
            _zoomStart = true;

            if (_isSniperRifle)
            {
                _zoomLevel = _targetFOV / 2;
                _mouseSensitivity = _targetMouseSensitivity / 2;
            }
            else
            {
                _zoomLevel = _targetFOV;
                _mouseSensitivity = _targetMouseSensitivity;
            }

            ZoomStarted?.Invoke(_zoomStart);
        }
        else
        {
            _zoomStart = false;
            ZoomDefault();
            ZoomStarted?.Invoke(_zoomStart);
        }

        _playerController.RotateSpeedChange(_mouseSensitivity);
    }

    public bool TryGetSniperRifle(bool isSniperRifle)
    {
        return _isSniperRifle = isSniperRifle;
    }
}
