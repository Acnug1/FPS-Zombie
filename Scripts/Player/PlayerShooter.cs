using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(AudioSource))]

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;

    private PlayerInput _input;
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private float _elapsedTime;
    private Coroutine coroutineShooting;
    private Animator _animator;
    private bool _onReload;
    private AudioSource _audioSource;

    private void Awake()
    {
        _input = new PlayerInput();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void Start()
    {
        ReloadWeapon(_currentWeapon);
    }

    private void OnEnable()
    {
        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.Reload.performed += ctx => ReloadWeapon(_currentWeapon);

        _input.Player.Click.performed += ctx => // есть 3 вида события started, performed и canceled
        {
            if (ctx.interaction is PressInteraction) // если наш interaction является PressInteraction (is - вернет bool). Сравниваем наш interaction
                ShootWeapon(); // вызываем метод  Shoot
        };

        _input.Player.Click.canceled += ctx =>
        {
            if (ctx.interaction is PressInteraction)
                StopShooting();
        };
    }

    private void OnDisable()
    {
        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.Reload.performed -= ctx => ReloadWeapon(_currentWeapon);

        _input.Player.Click.performed -= ctx => // есть 3 вида события started, performed и canceled
        {
            if (ctx.interaction is PressInteraction) // если наш interaction является PressInteraction (is - вернет bool). Сравниваем наш interaction
                ShootWeapon(); // вызываем метод Shoot 
        };

        _input.Player.Click.canceled -= ctx =>
        {
            if (ctx.interaction is PressInteraction)
                StopShooting();
        };
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_onReload == true && _elapsedTime >= _currentWeapon.ReloadDelay)
        {
            _onReload = false;
        }
    }

    private void ShootWeapon()
    {
        if (_player.CurrentHealth > 0 && _onReload == false)
            coroutineShooting = StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_currentWeapon.ShootDelay);

        while (true)
        {
            if (_currentWeapon.CurrentAmmunition > 0)
            {
                _animator.SetTrigger("Shoot");
                _audioSource.PlayOneShot(_currentWeapon.ShotSound);
                _currentWeapon.Shoot();
            }
            else
                _audioSource.PlayOneShot(_currentWeapon.NoAmmoShotSound);

            yield return waitForSeconds;
        }
    }

    private void StopShooting()
    {
        if (coroutineShooting != null)
        {
            StopCoroutine(coroutineShooting);
        }
    }

    private void ReloadWeapon(Weapon currentWeapon)
    {
        if (_player.CurrentHealth > 0 && _onReload == false)
        {
            _elapsedTime = 0;
            _onReload = true;

            _audioSource.PlayOneShot(_currentWeapon.ReloadSound);
            _animator.SetTrigger("Reload");

            currentWeapon.ReloadWeapon();
        }
    }

    public void BuyWeapon(Weapon weapon, Goods good)
    {
        //    _player.SpendMoney(good.Price);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _animator = _currentWeapon.GetComponent<Animator>();
    }
}
