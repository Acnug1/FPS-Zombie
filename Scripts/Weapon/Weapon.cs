using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponView))]
[RequireComponent(typeof(WeaponSoundSettings))]

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private bool _isMeleeWeapon = false;
    [SerializeField] private bool _isBuyed;
    [SerializeField] private int _ammunition;
    [SerializeField] private int _numberWeaponClips;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private float _changeWeaponTime;
    [SerializeField] protected Bullet Bullet;

    private int _currentAmmunition;

    public event UnityAction<int, int> AmmunitionChanged;

    public bool IsMeleeWeapon => _isMeleeWeapon;
    public bool IsBuyed => _isBuyed;
    public float ShootDelay => _shootDelay;
    public float ReloadDelay => _reloadDelay;
    public float ChangeWeaponTime => _changeWeaponTime;
    public int CurrentAmmunition => _currentAmmunition;
    public int Ammunition => _ammunition;
    public int NumberWeaponClips => _numberWeaponClips;
    public Bullet CurrentBullet => Bullet;

    private void OnValidate()
    {
        if (_numberWeaponClips < 0)
            _numberWeaponClips = 0;
    }

    private void Start()
    {
        if (!_isMeleeWeapon)
            _numberWeaponClips += 1;
        else
            _currentAmmunition = _ammunition;
    }

    public abstract void Shoot(Transform shootPoint, int shootsCount, bool playerMoves);

    public string GetClassName()
    {
        return name;
    }

    protected void ChangeAmmunition()
    {
        _currentAmmunition--;
        AmmunitionChanged?.Invoke(_currentAmmunition, _numberWeaponClips);
    }

    public void ShowWeaponInfo()
    {
        AmmunitionChanged?.Invoke(_currentAmmunition, _numberWeaponClips);
    }

    public void ReloadWeapon()
    {
        if (_numberWeaponClips > 0 && _currentAmmunition != _ammunition)
        {
            _numberWeaponClips--;
            _currentAmmunition = _ammunition;
            AmmunitionChanged?.Invoke(_currentAmmunition, _numberWeaponClips);
        }
    }

    public void ReplenishAmmunition(int ammoCount)
    {
        _numberWeaponClips += ammoCount;
        ShowWeaponInfo();
    }

    public void Buy()
    {
        _isBuyed = true;
    }

    public void ResetBuy()
    {
        _isBuyed = false;
    }
}
