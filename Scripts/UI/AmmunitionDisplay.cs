using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmunitionDisplay : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private TMP_Text _ammunitionDisplay;
    [SerializeField] private TMP_Text _weaponClipsDisplay;

    private void Awake()
    {
        _ammunitionDisplay.text = "0";
        _weaponClipsDisplay.text = "0";
    }

    private void OnEnable()
    {
        foreach (var weapon in _weapons)
        {
            weapon.AmmunitionChanged += OnAmmunitionChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var weapon in _weapons)
        {
            weapon.AmmunitionChanged -= OnAmmunitionChanged;
        }
    }

    private void OnAmmunitionChanged(int currentAmmunition, int numberWeaponClips)
    {
        _ammunitionDisplay.text = currentAmmunition.ToString();
        _weaponClipsDisplay.text = numberWeaponClips.ToString();
    }
}
