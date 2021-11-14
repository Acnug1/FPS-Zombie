using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class WaypointMovementState : State
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform[] _points;
    private int _currentPoint;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (Enemy.Health > 0)
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.isStopped = false;
            _animator.Play(AnimatorZombieController.States.Walk);
        }
    }

    private void OnDisable()
    {
        _navMeshAgent.isStopped = true;
    }

    private void Start()
    {
        _points = new Transform[Enemy.Path.childCount];

        for (int i = 0; i < Enemy.Path.childCount; i++)
        {
            _points[i] = Enemy.Path.GetChild(i);
        }
    }

    private void Update()
    {
        if (Enemy.Health > 0)
            MoveToPoints();
        else
        {
            _navMeshAgent.isStopped = true;
            _animator.StopPlayback();
        }
    }

    private void MoveToPoints()
    {
        Transform target = _points[_currentPoint];

        _navMeshAgent.destination = target.position;

        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }
    }
}
