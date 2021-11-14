using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponItem _template;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _errorSound;
    [SerializeField] private WeaponsStorage _weaponsStorage;
    [SerializeField] private AdSettings _adSettings;

    private AudioSource _audioSource;

    public event UnityAction WeaponSold;

    private void OnEnable()
    {
        _adSettings.OnRewarded += OnRewardedVideoFinished;
    }

    private void OnDisable()
    {
        _adSettings.OnRewarded -= OnRewardedVideoFinished;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        foreach (var weapon in _weaponsStorage.Weapons)
        {
            AddItem(weapon);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(weapon);
        view.name = _template.name + (_itemContainer.childCount);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView weaponView, WeaponItem view)
    {
        TrySellWeapon(weapon, weaponView, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView weaponView, WeaponItem view)
    {
        if (weaponView.Price <= _player.Money)
        {
            _audioSource.PlayOneShot(_buySound, Settings.Volume);
            _player.BuyWeapon(weapon, weaponView);
            weapon.Buy();
            view.SellButtonClick -= OnSellButtonClick;
            WeaponSold?.Invoke();
        }
        else
            _audioSource.PlayOneShot(_errorSound, Settings.Volume);
    }

    private void OnRewardedVideoFinished(double reward, string currencyName)
    {
        _player.AddMoney((int)reward);
    }
}