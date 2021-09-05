using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundSettings : MonoBehaviour
{
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _noAmmoShotSound;

    public AudioClip ReloadSound => _reloadSound;
    public AudioClip ShotSound => _shotSound;
    public AudioClip NoAmmoShotSound => _noAmmoShotSound;
}
