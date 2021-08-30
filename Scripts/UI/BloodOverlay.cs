using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class BloodOverlay : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void OnEnable()
    {
        _player.DamageApplied += OnDamageApplied;
    }

    private void OnDisable()
    {
        _player.DamageApplied -= OnDamageApplied;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDamageApplied()
    {
        _animator.Play(AnimatorBloodOverlay.States.BloodOverlayAlpha);
    }
}
