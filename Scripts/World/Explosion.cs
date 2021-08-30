using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Explosions _explosions;

    private int _damageForHitBox;
    private float _radius;
    private float _force;
    private float _explodeDelay;
    private ParticleSystem _explosionEffect;
    private ParticleSystem _bloodEffect;
    private AudioClip _explosionSound;
    private Rigidbody _rigidbody;
    private bool _explosionDone;
    private Transform[] _bloodSplatters;

    private void Start()
    {
        _damageForHitBox = _explosions.DamageForHitBox;
        _radius = _explosions.Radius;
        _force = _explosions.Force;
        _explodeDelay = _explosions.ExplodeDelay;
        _explosionEffect = _explosions.ExplosionEffect;
        _bloodEffect = _explosions.BloodEffect;
        _explosionSound = _explosions.ExplosionSound;
        _bloodSplatters = _explosions.BloodSplatters;

        if (gameObject.TryGetComponent(out Grenade grenade))
            ExplodeWithDelay();
    }

    public void ExplodeWithDelay()
    {
        if (_explosionDone)
            return;

        _explosionDone = true;

        if (gameObject.TryGetComponent(out Renderer renderer))
            renderer.material.color = Color.red;

        Invoke(nameof(Explode), _explodeDelay);
    }

    private void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var overlappedCollider in overlappedColliders)
        {
            _rigidbody = overlappedCollider.attachedRigidbody;
            if (_rigidbody)
            {
                if (_rigidbody.transform.root.TryGetComponent(out Enemy enemy))
                {
                    if (_bloodEffect)
                        Instantiate(_bloodEffect, _rigidbody.position, Quaternion.LookRotation(_rigidbody.transform.eulerAngles));

                    if (_bloodSplatters != null)
                    {
                        var randomPrefabNumber = Random.Range(0, _bloodSplatters.Length);
                        Instantiate(_bloodSplatters[randomPrefabNumber], _rigidbody.position, Quaternion.LookRotation(_rigidbody.transform.eulerAngles));
                    }

                    if (enemy.Health > 0)
                        enemy.TakeDamage(_damageForHitBox);
                }
                else
                if (_rigidbody.TryGetComponent(out Player player) && player.CurrentHealth > 0)
                    player.ApplyDamage(_damageForHitBox);
                else
                if (_rigidbody.TryGetComponent(out Explosion explosion) && Vector3.Distance(transform.position, _rigidbody.transform.position) < _radius / 1.5f)
                    explosion.ExplodeWithDelay();

                _rigidbody.AddExplosionForce(_force, transform.position, _radius, 1f);
            }
        }

        if (_explosionEffect)
        {
            ParticleSystem explosionEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);

            if (explosionEffect.TryGetComponent(out AudioSource audioSource) && _explosionSound)
                audioSource.PlayOneShot(_explosionSound, Settings.Volume);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius / 1.5f);
    }
}
