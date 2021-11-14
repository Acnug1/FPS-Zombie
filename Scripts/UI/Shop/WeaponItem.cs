using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Weapon _weapon;
    private WeaponView _weaponView;

    public event UnityAction<Weapon, WeaponView, WeaponItem> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void Start()
    {
        TryLockItem();
    }

    private void TryLockItem()
    {
        if (_weapon.IsBuyed)
            _sellButton.interactable = false;
    }

    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _weaponView = _weapon.GetComponent<WeaponView>();

        _label.text = _weaponView.Label;
        _price.text = _weaponView.Price.ToString();
        _icon.sprite = _weaponView.Icon;
    }

    private void OnButtonClick()
    {
        if (!_weapon.IsBuyed)
            SellButtonClick?.Invoke(_weapon, _weaponView, this);
    }
}