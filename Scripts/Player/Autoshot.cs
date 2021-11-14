using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerShooter))]

public class Autoshot : MonoBehaviour
{
    [SerializeField] private float _checkTime;

    private PlayerShooter _playerShooter;
    private Ray _ray;
    private Weapon _currentWeapon;
    private float _elapsedTime;
    private bool _isTargetSpotted;

    public event UnityAction<bool> TargetSpotted;

    private void Awake()
    {
        _playerShooter = GetComponent<PlayerShooter>();
    }

    private void OnEnable()
    {
        _playerShooter.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _playerShooter.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        if (_elapsedTime > _checkTime)
        {
            CheckTarget();

            _elapsedTime = 0;
        }

        _elapsedTime += Time.deltaTime;
    }

    private void CheckTarget()
    {
        if (_currentWeapon.CurrentBullet != null)
        {
            _ray = new Ray(_playerShooter.ShootPoint.position, _playerShooter.ShootPoint.forward);

            if (Physics.Raycast(_ray, out RaycastHit hit, _currentWeapon.CurrentBullet.Bullets.MaxDistance, ~_currentWeapon.CurrentBullet.Bullets.IgnoreLayer)
                && hit.transform.root.TryGetComponent(out Enemy enemy) && _currentWeapon.CurrentAmmunition > 0)
            {
                _isTargetSpotted = true;
                TargetSpotted?.Invoke(_isTargetSpotted);
            }
            else if (_isTargetSpotted)
            {
                _isTargetSpotted = false;
                TargetSpotted?.Invoke(_isTargetSpotted);
            }
        }
    }

    private void OnWeaponChanged(Weapon currentWeapon)
    {
        _currentWeapon = currentWeapon;
    }
}
