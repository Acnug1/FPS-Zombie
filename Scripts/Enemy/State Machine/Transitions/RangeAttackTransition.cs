using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackTransition : Transition
{
    [SerializeField] private float _rangeAttack;
    [SerializeField] private float _rangeSpread;

    private void Start()
    {
        _rangeAttack += Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        if (Target != null && Enemy.Health > 0)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) > _rangeAttack)
            {
                NeedTransit = true;
            }
        }
    }
}
