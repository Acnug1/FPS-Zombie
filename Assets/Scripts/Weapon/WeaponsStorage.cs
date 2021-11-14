using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsStorage : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    public List<Weapon> Weapons => _weapons;
}
