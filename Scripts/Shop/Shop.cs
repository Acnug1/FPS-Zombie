using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _errorSound;
    [SerializeField] private WeaponsStorage _weaponsStorage;

    private AudioSource _audioSource;

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

    private void OnSellButtonClick(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _audioSource.PlayOneShot(_buySound, Settings.Volume);
            _player.BuyWeapon(weapon);
            weapon.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
        else
            _audioSource.PlayOneShot(_errorSound, Settings.Volume);
    }
}