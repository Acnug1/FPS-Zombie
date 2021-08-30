using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator.SetBool(AnimatorDoorController.Params.IsOpen, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _animator.SetBool(AnimatorDoorController.Params.IsOpen, true);
            _audioSource.PlayOneShot(_openDoorSound, Settings.Volume);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _animator.SetBool(AnimatorDoorController.Params.IsOpen, false);
            _audioSource.PlayOneShot(_closeDoorSound, Settings.Volume);
        }
    }
}
