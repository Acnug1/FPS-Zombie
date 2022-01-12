using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Footsteps))]

public class MoveToPlayerState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _zombieScream;
    [SerializeField] private float _stepDelay;
    [SerializeField] private LayerMask _ignoreLayer;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AudioSource _audioSource;
    private Footsteps _footSteps;
    private float _lastStepTime;
    private float _pivotOffset = 0.5f;
    private RaycastHit hit;
    private Ray ray;
    private const string Concrete = "Concrete";
    private const string Metal = "Metal";
    private const string Wood = "Wood";
    private const string Dirt = "Dirt";
    private const string Sand = "Sand";
    private const string Cloth = "Cloth";
    private const string Softbody = "Softbody";

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _footSteps = GetComponent<Footsteps>();
    }

    private void OnEnable()
    {
        _navMeshAgent.speed = _speed;
        _navMeshAgent.isStopped = false;
        _animator.SetBool(AnimatorZombieController.Params.Run, true);
        _audioSource.PlayOneShot(_zombieScream, Settings.Volume);
    }

    private void OnDisable()
    {
        _animator.SetBool(AnimatorZombieController.Params.Run, false);
        _navMeshAgent.isStopped = true;
    }

    private void Update()
    {
        if (Target != null && Enemy.Health > 0)
        {
            _navMeshAgent.destination = Target.transform.position;
            GetStep();

            _lastStepTime -= Time.deltaTime;
        }
        else
        if (!_navMeshAgent.isStopped)
        {
            _navMeshAgent.isStopped = true;
            _animator.StopPlayback();
        }
    }

    private void GetStep()
    {
        if (_lastStepTime <= 0)
        {
            ray = new Ray(transform.position + new Vector3(0, _pivotOffset, 0), -transform.up);

            if (Physics.Raycast(ray, out hit, 1f, ~_ignoreLayer))
            {
                switch (hit.transform.tag) // берем тег поверхности
                {
                    case Concrete:
                        _footSteps.PlayStep(Footsteps.StepsOn.Concrete, Settings.Volume);
                        break;
                    case Metal:
                        _footSteps.PlayStep(Footsteps.StepsOn.Metal, Settings.Volume);
                        break;
                    case Wood:
                        _footSteps.PlayStep(Footsteps.StepsOn.Wood, Settings.Volume);
                        break;
                    case Sand:
                        _footSteps.PlayStep(Footsteps.StepsOn.Sand, Settings.Volume);
                        break;
                    case Dirt:
                    case Cloth:
                    case Softbody:
                    default:
                        _footSteps.PlayStep(Footsteps.StepsOn.Ground, Settings.Volume);
                        break;
                }
            }

            _lastStepTime = _stepDelay;
        }
    }
}
