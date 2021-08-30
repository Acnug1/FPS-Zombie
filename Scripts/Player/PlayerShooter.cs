using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(OpticalSight))]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Autoshot))]

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons = new List<Weapon>();
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AudioClip _weaponSelect;
    [SerializeField] private float _returnTimeCamera;
    [SerializeField] private Vector3 _backRotationAngleCamera;
    [SerializeField] private SavedData _savedData;
    [SerializeField] private WeaponsStorage _weaponsStorage;

    private PlayerInput _input;
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private float _elapsedTime;
    private float _timeBetweenShots;
    private Coroutine coroutineShooting;
    private Animator _animator;
    private bool _onReload;
    private AudioSource _audioSource;
    private int _shootsCount;
    private OpticalSight _opticalSight;
    private bool _isZoom;
    private bool _isShooting;
    private bool _isWeaponChange;
    private Camera _playerCamera;
    private Autoshot _autoshot;

    public event UnityAction<Weapon> WeaponChanged;

    public bool OnReload => _onReload;
    public bool IsWeaponChange => _isWeaponChange;
    public Transform ShootPoint => _shootPoint;

    private void Awake()
    {
        _input = new PlayerInput();
        _opticalSight = GetComponent<OpticalSight>();
        _playerCamera = GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        _autoshot = GetComponent<Autoshot>();
        _audioSource.playOnAwake = false;

        GetLoadingWeapons();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.NextWeapon.performed += ctx => NextWeapon();

        _input.Player.PreviousWeapon.performed += ctx => PreviousWeapon();

        _input.Player.Reload.performed += ctx => StartReload();

        _input.Player.Click.performed += ctx =>
        {
            if (ctx.interaction is PressInteraction)
                ShootWeapon();
        };

        _input.Player.Click.canceled += ctx =>
        {
            if (ctx.interaction is PressInteraction)
                StopShooting();
        };

        _opticalSight.ZoomStarted += OnZoomStarted;

        _player.AmmoChanged += OnCurrentWeaponAmmoChanged;

        _autoshot.TargetSpotted += OnTargetSpotted;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.NextWeapon.performed -= ctx => NextWeapon();

        _input.Player.PreviousWeapon.performed -= ctx => PreviousWeapon();

        _input.Player.Reload.performed -= ctx => StartReload();

        _input.Player.Click.performed -= ctx =>
        {
            if (ctx.interaction is PressInteraction)
                ShootWeapon();
        };

        _input.Player.Click.canceled -= ctx =>
        {
            if (ctx.interaction is PressInteraction)
                StopShooting();
        };

        _opticalSight.ZoomStarted -= OnZoomStarted;

        _player.AmmoChanged -= OnCurrentWeaponAmmoChanged;

        _autoshot.TargetSpotted -= OnTargetSpotted;
    }

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void GetLoadingWeapons()
    {
        var weaponsName = _savedData.LoadWeaponsData();

        foreach (var weapon in _weaponsStorage.Weapons)
        {
            if (weaponsName.Contains(weapon.GetClassName()) && !_weapons.Contains(weapon))
            {
                AddWeapon(weapon);
                weapon.Buy();
            }
            else if (_weapons.Contains(weapon))
                weapon.Buy();
            else
                weapon.ResetBuy();

            weapon.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _timeBetweenShots += Time.deltaTime;

        if (!_isShooting && _currentWeapon.CurrentAmmunition <= 0 && _currentWeapon.NumberWeaponClips > 0 && !_currentWeapon.IsMeleeWeapon)
            StartReload();

        ReloadWeapon();

        TakeSelectedWeapon();

        ReturnAngleCamera(_playerController.RotationCamera);
    }

    private void ReturnAngleCamera(Vector3 startRotationCamera)
    {
        float newX = Mathf.LerpAngle(_playerCamera.transform.localEulerAngles.x, startRotationCamera.x, Time.deltaTime * _returnTimeCamera);
        float newY = Mathf.LerpAngle(_playerCamera.transform.localEulerAngles.y, startRotationCamera.y, Time.deltaTime * _returnTimeCamera);
        float newZ = Mathf.LerpAngle(_playerCamera.transform.localEulerAngles.z, startRotationCamera.z, Time.deltaTime * _returnTimeCamera);

        _playerCamera.transform.localEulerAngles = new Vector3(newX, newY, newZ);
    }

    private void OnTargetSpotted(bool isTargetSpotted)
    {
        if (isTargetSpotted && _isShooting)
            return;

        if (isTargetSpotted)
            ShootWeapon();
        else
            StopShooting();
    }

    private void ShootWeapon()
    {
        if (_player.CurrentHealth > 0 && !_onReload && !_isWeaponChange && !_playerController.IsSprint && !_isShooting && Time.timeScale > 0 && !_player.IsHealing)
        {
            if (_timeBetweenShots < _currentWeapon.ShootDelay)
                return;

            _timeBetweenShots = 0;
            _isShooting = true;

            coroutineShooting = StartCoroutine(Shooting());
        }
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_currentWeapon.ShootDelay);

        while (!_onReload && _player.CurrentHealth > 0 && !_player.IsHealing)
        {
            if (_playerController.IsSprint)
                break;

            if (_currentWeapon.IsMeleeWeapon)
            {
                _animator.SetTrigger(AnimatorPlayerShooter.Params.Strike);

                _audioSource.PlayOneShot(_currentWeapon.ShotSound, Settings.Volume);
                StartCoroutine(StrikeDelay());
            }
            else if (_currentWeapon.CurrentAmmunition > 0)
            {
                _shootsCount++;

                if (_isZoom && (!_playerController.PlayerMoves || _playerController.InAir))
                    _animator.SetTrigger(AnimatorPlayerShooter.Params.ShootAim);
                else
                    _animator.SetTrigger(AnimatorPlayerShooter.Params.Shoot);

                _audioSource.PlayOneShot(_currentWeapon.ShotSound, Settings.Volume);
                _currentWeapon.Shoot(_shootPoint, _shootsCount, _playerController.PlayerMoves);

                if (_playerCamera.transform.localEulerAngles.x >= 271 || _playerCamera.transform.localEulerAngles.x <= 90 && _playerCamera.transform.localEulerAngles.x >= 0)
                    _playerCamera.transform.localEulerAngles += _backRotationAngleCamera;
            }
            else
                _audioSource.PlayOneShot(_currentWeapon.NoAmmoShotSound, Settings.Volume);

            yield return waitForSeconds;
        }

        StopShooting();
    }

    private IEnumerator StrikeDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_currentWeapon.ShootDelay / 4);
        yield return waitForSeconds;

        _currentWeapon.Shoot(_shootPoint, _shootsCount, _playerController.PlayerMoves);
    }

    private void StopShooting()
    {
        if (coroutineShooting != null)
        {
            StopCoroutine(coroutineShooting);
        }

        _shootsCount = 0;
        _isShooting = false;
    }

    private void StartReload()
    {
        if (_player.CurrentHealth > 0 && !_onReload && !_isWeaponChange && !_currentWeapon.IsMeleeWeapon && Time.timeScale > 0)
        {
            _elapsedTime = 0;
            _onReload = true;

            _audioSource.PlayOneShot(_currentWeapon.ReloadSound, Settings.Volume);
            _animator.SetTrigger(AnimatorPlayerShooter.Params.Reload);
        }
    }

    private void ReloadWeapon()
    {
        if (_onReload == true && _elapsedTime >= _currentWeapon.ReloadDelay)
        {
            _currentWeapon.ReloadWeapon();
            _onReload = false;
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }

    public List<string> GetWeaponsName()
    {
        var weaponsName = new List<string>();

        foreach (var weapon in _weapons)
        {
            weaponsName.Add(weapon.GetClassName());
        }

        return weaponsName;
    }

    public void NextWeapon()
    {
        if (!_onReload && !_isShooting && !_isWeaponChange && _player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            HideCurrentWeapon(false);

            if (_currentWeaponNumber == _weapons.Count - 1)
                _currentWeaponNumber = 0;
            else
                _currentWeaponNumber++;

            ChangeWeapon(_weapons[_currentWeaponNumber]);
        }
    }

    public void PreviousWeapon()
    {
        if (!_onReload && !_isShooting && !_isWeaponChange && _player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            HideCurrentWeapon(false);

            if (_currentWeaponNumber == 0)
                _currentWeaponNumber = _weapons.Count - 1;
            else
                _currentWeaponNumber--;

            ChangeWeapon(_weapons[_currentWeaponNumber]);
        }
    }

    public void HideCurrentWeapon(bool isUseAnimation)
    {
        if (_isZoom)
            _opticalSight.Zoom();

        if (isUseAnimation)
            _animator.SetTrigger(AnimatorPlayerShooter.Params.Hide);
        else
            _currentWeapon.gameObject.SetActive(false);
    }

    public void ShowCurrentWeapon()
    {
        _elapsedTime = 0;
        _isWeaponChange = true;

        _currentWeapon.gameObject.SetActive(true);
        _audioSource.PlayOneShot(_weaponSelect, Settings.Volume);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _elapsedTime = 0;
        _isWeaponChange = true;

        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);

        _animator = _currentWeapon.GetComponent<Animator>();
        WeaponChanged?.Invoke(_currentWeapon);

        _audioSource.PlayOneShot(_weaponSelect, Settings.Volume);
        _timeBetweenShots = _currentWeapon.ShootDelay;
    }

    private void TakeSelectedWeapon()
    {
        if (_isWeaponChange && _elapsedTime >= _currentWeapon.ChangeWeaponTime)
        {
            _currentWeapon.ShowWeaponInfo();
            _isWeaponChange = false;
        }
    }

    private void OnCurrentWeaponAmmoChanged(int ammoCount)
    {
        _currentWeapon.ReplenishAmmunition(ammoCount);
    }

    private void OnZoomStarted(bool isZoom)
    {
        _isZoom = isZoom;

        if (_currentWeapon.IsMeleeWeapon || _player.CurrentHealth <= 0)
            return;

        if (_isZoom)
            _animator.SetBool(AnimatorPlayerShooter.Params.IdleAim, true);
        else
            _animator.SetBool(AnimatorPlayerShooter.Params.IdleAim, false);
    }
}
