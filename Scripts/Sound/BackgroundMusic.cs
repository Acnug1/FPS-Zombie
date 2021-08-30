using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private bool _isLooping;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = _isLooping;
        _audioSource.volume = Settings.Volume;
        _audioSource.clip = _backgroundMusic;
    }

    private void OnEnable()
    {
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }

    private void Update()
    {
        if (_audioSource.volume != Settings.Volume)
            _audioSource.volume = Settings.Volume;
    }
}
