using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _kitCount;

    private Animator _animator;

    public int KitCount => _kitCount;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenFirstAidBox()
    {
        _animator.SetTrigger(AnimatorAmmoBox.Params.TakeIt);
        _kitCount = 0;
    }
}
