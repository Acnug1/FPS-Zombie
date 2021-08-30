using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _deathTime;
    [SerializeField] private float _deathPlayerSpeed;
    [SerializeField] private float _anglePlayerDeath;
    [SerializeField] private AudioClip _deathPlayer;
    [SerializeField] private AudioClip _hitPlayer;
    [SerializeField] private AudioClip _takeAmmo;
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private FirstAid _firstAid;
    [SerializeField] private int _firstAidKitCount;
    [SerializeField] private SavedData _savedData;

    private int _currentHealth;
    private int _money;
    private PlayerInput _input;
    private AudioSource _audioSource;
    private bool _isHealing;
    private Weapon _currentWeapon;
    private bool _isPlayerDead;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction Died;
    public event UnityAction<int> AmmoChanged;
    public event UnityAction<int> FirstAidKitChanged;
    public event UnityAction DamageApplied;

    public int FirstAidKitCount => _firstAidKitCount;
    public int CurrentHealth => _currentHealth;
    public int Money => _money;
    public bool IsHealing => _isHealing;

    private void Awake()
    {
        _input = new PlayerInput();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _firstAid.gameObject.SetActive(false);
        _currentHealth = _health;

        _money = _savedData.LoadMoneyData(this);
        _firstAidKitCount = _savedData.LoadFirstAidKitsData(this);
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Heal.performed += ctx => Heal();
        _playerShooter.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Heal.performed -= ctx => Heal();
        _playerShooter.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        DeathPlayerHandler();
    }

    private void DeathPlayerHandler()
    {
        if (_isPlayerDead)
        {
            float newZ = Mathf.LerpAngle(transform.localEulerAngles.z, _anglePlayerDeath, Time.deltaTime * _deathPlayerSpeed);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (!_isHealing)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damage;
                _audioSource.PlayOneShot(_hitPlayer, Settings.Volume);
            }

            if (_currentHealth < 0)
                _currentHealth = 0;

            HealthChanged?.Invoke(_currentHealth, _health);
            DamageApplied?.Invoke();

            if (_currentHealth == 0 && !_isPlayerDead)
            {
                _isPlayerDead = true;

                HidePlayerWeapon(true);
                _audioSource.PlayOneShot(_deathPlayer, Settings.Volume);
                Invoke(nameof(SendEventDeathPlayer), _deathTime);
            }
        }
    }

    private void SendEventDeathPlayer()
    {
        Died?.Invoke();
    }

    public void HidePlayerWeapon(bool isUseAnimation)
    {
        _playerShooter.HideCurrentWeapon(isUseAnimation);
    }

    public void AddMoney(int reward)
    {
        _money += reward;
        MoneyChanged?.Invoke(_money);
    }

    private void SpendMoney(int price)
    {
        _money -= price;
        MoneyChanged?.Invoke(_money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        SpendMoney(weapon.Price);
        _playerShooter.AddWeapon(weapon);
    }

    private void Heal()
    {
        if (_currentHealth != _health && _firstAidKitCount > 0 && !_isHealing
            && !_playerShooter.OnReload && !_playerShooter.IsWeaponChange 
            && _currentHealth > 0 && Time.timeScale > 0)
        {
            _isHealing = true;

            HidePlayerWeapon(false);
            _playerShooter.enabled = false;
            _firstAid.gameObject.SetActive(true);
            _audioSource.PlayOneShot(_firstAid.HealSound, Settings.Volume);

            _firstAidKitCount--;
            FirstAidKitChanged?.Invoke(_firstAidKitCount);

            StartCoroutine(HealingPlayer());
        }
    }

    private IEnumerator HealingPlayer()
    {
        var waitForSeconds = new WaitForSeconds(3.533f);
        yield return waitForSeconds;

        _currentHealth += _firstAid.HealLevel;

        if (_currentHealth > _health)
            _currentHealth = _health;

        HealthChanged?.Invoke(_currentHealth, _health);

        _firstAid.gameObject.SetActive(false);
        _playerShooter.enabled = true;
        _playerShooter.ShowCurrentWeapon();
        _isHealing = false;
    }

    private void OnWeaponChanged(Weapon currentWeapon)
    {
        _currentWeapon = currentWeapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ammo ammo) && _currentHealth > 0)
        {
            if (ammo.AmmoCount > 0 && _playerShooter.enabled && !_currentWeapon.IsMeleeWeapon)
            {
                AmmoChanged?.Invoke(ammo.AmmoCount);
                ammo.OpenAmmoBox();
                _audioSource.PlayOneShot(_takeAmmo, Settings.Volume);
            }
        }

        if (other.TryGetComponent(out FirstAidKit firstAidKit) && _currentHealth > 0)
        {
            if (firstAidKit.KitCount > 0)
            {
                _firstAidKitCount += firstAidKit.KitCount;
                FirstAidKitChanged?.Invoke(_firstAidKitCount);
                firstAidKit.OpenFirstAidBox();
                _audioSource.PlayOneShot(_takeAmmo, Settings.Volume);
            }
        }
    }
}
