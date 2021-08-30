using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Enemies _enemies;

    private int _health;
    private int _reward;
    private float _bodyDestroyTime;
    private AudioClip _hitEnemy;
    private AudioClip _deathEnemy;
    private Player _target;
    private Transform _path;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isEnemyDead;
    private int _maxHealth;

    public int Health => _health;
    public int Reward => _reward;
    public Player Target => _target;
    public Transform Path => _path;

    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _health = _enemies.Health;
        _reward = _enemies.Reward;
        _bodyDestroyTime = _enemies.BodyDestroyTime;
        _hitEnemy = _enemies.HitEnemy;
        _deathEnemy = _enemies.DeathEnemy;

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _maxHealth = _health;

        DisableRagdoll();
    }

    public void Init(Player target, Transform path)
    {
        _target = target;
        _path = path;
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            _audioSource.PlayOneShot(_hitEnemy, Settings.Volume);
        }

        if (_health <= 0 && !_isEnemyDead)
        {
            _isEnemyDead = true;
            _audioSource.PlayOneShot(_deathEnemy, Settings.Volume);
            EnableRagdoll();
            Dying?.Invoke(this);

            Invoke(nameof(DeathEnemyHandler), _bodyDestroyTime);
        }
    }

    private void DeathEnemyHandler()
    {
        DestroyProjectors();
        DestroyParticles();
        gameObject.SetActive(false);
        PrepareToReturnInPool();
    }

    private void PrepareToReturnInPool()
    {
        DisableRagdoll();
        _health = _maxHealth;
        _isEnemyDead = false;
    }

    private void EnableRagdoll()
    {
        _animator.enabled = false;

        var zombieParts = GetComponentsInChildren<Rigidbody>();

        foreach (var zombiePart in zombieParts)
        {
            zombiePart.isKinematic = false;
        }
    }

    private void DisableRagdoll()
    {
        _animator.enabled = true;

        var zombieParts = GetComponentsInChildren<Rigidbody>();

        foreach (var zombiePart in zombieParts)
        {
            zombiePart.isKinematic = true;
        }
    }

    private void DestroyProjectors()
    {
        var projectors = GetComponentsInChildren<Projector>();

        foreach (var projector in projectors)
        {
            Destroy(projector.gameObject);
        }
    }

    private void DestroyParticles()
    {
        var particles = GetComponentsInChildren<ParticleSystem>();

        foreach (var particle in particles)
        {
            Destroy(particle.gameObject);
        }
    }
}
