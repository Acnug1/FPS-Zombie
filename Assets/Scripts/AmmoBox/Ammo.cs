using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoCount;

    private Animator _animator;

    public int AmmoCount => _ammoCount;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenAmmoBox()
    {
        _animator.SetTrigger(AnimatorAmmoBox.Params.TakeIt);
        _ammoCount = 0;
    }
}
