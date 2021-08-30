using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Player Target { get; private set; }
    protected Enemy Enemy { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Player target, Enemy enemy)
    {
        Target = target;
        Enemy = enemy;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}