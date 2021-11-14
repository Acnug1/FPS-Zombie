using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private float _speedRotation;

    private float _lastAttackTime;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool(AnimatorZombieController.Params.Attack, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(AnimatorZombieController.Params.Attack, false);
    }

    private void Update()
    {
        if (Target != null && Enemy.Health > 0)
        {
            if (_lastAttackTime <= 0)
            {
                Attack(Target);
                _lastAttackTime = _delay;
            }

            LookAtTarget(Target);

            _lastAttackTime -= Time.deltaTime;
        }
        else
            _animator.StopPlayback();
    }

    private void Attack(Player target)
    {
        target.ApplyDamage(_damage);
    }

    private void LookAtTarget(Player target)
    {
        Vector3 lookDirection = target.transform.position - transform.position; // получаем направление куда смотреть врагу
        Quaternion rotation = Quaternion.LookRotation(lookDirection); // задаем поворот взгляда для врага
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedRotation * Time.deltaTime); // плавно поворачиваем взгляд к Quaternion rotation 
    }
}