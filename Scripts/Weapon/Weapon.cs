using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private int _ammunition;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _noAmmoShotSound;
    [SerializeField] protected Bullet Bullet;

    private int _currentAmmunition;

    public event UnityAction<int> AmmunitionChanged;

    public bool IsBuyed => _isBuyed;
    public float ShootDelay => _shootDelay;
    public float ReloadDelay => _reloadDelay;
    public int CurrentAmmunition => _currentAmmunition;
    public AudioClip ReloadSound => _reloadSound;
    public AudioClip ShotSound => _shotSound;
    public AudioClip NoAmmoShotSound => _noAmmoShotSound;

    public abstract void Shoot();

    protected void ChangeAmmunition()
    {
        _currentAmmunition--;
        AmmunitionChanged?.Invoke(_currentAmmunition);
    }

    public void ReloadWeapon()
    {
        _currentAmmunition = _ammunition;
        AmmunitionChanged?.Invoke(_currentAmmunition);
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}
