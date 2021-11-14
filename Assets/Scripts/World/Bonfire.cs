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
        if (other.TryGetComponent(out Inflammable inflammable)) // если коллайдер, который вошел в триггер имеет компонент Inflammable (который можно поджечь)
            inflammable.OnStayFire(); // то вызываем у него метод OnEnterFire() (сообщаем что он вошел в костер)
        else
        if (other.transform.root.TryGetComponent(out Inflammable inflammableEnemy) && other.transform.root.TryGetComponent(out Enemy enemy)) 
        {
            inflammableEnemy.OnStayFire(); // то вызываем у него метод OnEnterFire() (сообщаем что он вошел в костер)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inflammable inflammable)) // если коллайдер, который вышел из триггера имеет компонент Inflammable
            inflammable.OnExitFire(); // то вызываем у него метод OnEnterFire() (сообщаем что он вошел в костер)
        else
        if (other.transform.root.TryGetComponent(out Inflammable inflammableEnemy) && other.transform.root.TryGetComponent(out Enemy enemy)) 
        {
            inflammableEnemy.OnExitFire(); // то вызываем у него метод OnExitFire() (сообщаем что он вышел из костра)
        }
    }
}
