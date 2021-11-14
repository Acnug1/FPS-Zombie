using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Bonfire : MonoBehaviour
{
    [SerializeField] private AudioClip _bonfireSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = Settings.Volume;
        _audioSource.clip = _bonfireSound;
        _audioSource.Play();
    }

    private void Update()
    {
        if (_audioSource.volume != Settings.Volume)
            _audioSource.volume = Settings.Volume;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Inflammable inflammable))
            inflammable.OnStayFire();
        else
        if (other.transform.root.TryGetComponent(out Inflammable inflammableEnemy) && other.transform.root.TryGetComponent(out Enemy enemy)) 
        {
            inflammableEnemy.OnStayFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inflammable inflammable))
            inflammable.OnExitFire();
        else
        if (other.transform.root.TryGetComponent(out Inflammable inflammableEnemy) && other.transform.root.TryGetComponent(out Enemy enemy)) 
        {
            inflammableEnemy.OnExitFire();
        }
    }
}
