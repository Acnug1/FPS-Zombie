using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Bullets _bullets;

    private int _damage;
    private float _force;
    private float _maxDistance;
    private ParticleSystem _bloodEffect;
    private ParticleSystem _decalHitConcrete;
    private ParticleSystem _decalHitMetal;
    private ParticleSystem _decalHitWood;
    private ParticleSystem _decalHitDirt;
    private ParticleSystem _decalHitSand;
    private ParticleSystem _decalHitSoftBody;
    private AudioClip _enemyHitSound;
    private AudioClip _hitSound;
    private bool _isSmallArms;
    private LayerMask _ignoreLayer;
    private Transform[] _bloodHoles;
    private Transform[] _bloodSplatters;
    private ParticleSystem _decalHitWall;
    private float _floatInFrontOfWall = 0.01f;
    private RaycastHit _hit;
    private Ray _ray;
    private float _projectorDistance = 0.15f;
    private const string Concrete = "Concrete";
    private const string Metal = "Metal";
    private const string Wood = "Wood";
    private const string Dirt = "Dirt";
    private const string Sand = "Sand";
    private const string Cloth = "Cloth";
    private const string Softbody = "Softbody";
    private const string Head = "Head";
    private const string Arm = "Arm";
    private const string Foot = "Foot";
    private const string Body = "Body";

    public Bullets Bullets => _bullets;

    private void Start()
    {
        _damage = _bullets.Damage;
        _force = _bullets.Force;
        _maxDistance = _bullets.MaxDistance;
        _bloodEffect = _bullets.BloodEffect;
        _decalHitConcrete = _bullets.DecalHitConcrete;
        _decalHitMetal = _bullets.DecalHitMetal;
        _decalHitWood = _bullets.DecalHitWood;
        _decalHitDirt = _bullets.DecalHitDirt;
        _decalHitSand = _bullets.DecalHitSand;
        _decalHitSoftBody = _bullets.DecalHitSoftBody;
        _enemyHitSound = _bullets.EnemtHitSound;
        _hitSound = _bullets.HitSound;
        _isSmallArms = _bullets.IsSmallArms;
        _ignoreLayer = _bullets.IgnoreLayer;
        _bloodHoles = _bullets.BloodHoles;
        _bloodSplatters = _bullets.BloodSplatters;
    }

    private void FixedUpdate()
    {
        CheckBulletHit();
    }

    private void CheckBulletHit()
    {
        _ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(_ray, out _hit, _maxDistance, ~_ignoreLayer))
        {
            HitHandler();
        }

        Destroy(gameObject);
    }

    private void HitHandler()
    {
        if (_hit.transform.root.TryGetComponent(out Enemy enemy))
        {
            if (_bloodEffect)
            {
                ParticleSystem bloodEffect = Instantiate(_bloodEffect, _hit.point, Quaternion.LookRotation(_hit.normal), enemy.transform);

                if (bloodEffect.TryGetComponent(out AudioSource audioSource) && _enemyHitSound)
                    audioSource.PlayOneShot(_enemyHitSound, Settings.Volume);
            }

            if (_bloodHoles != null)
            {
                var randomPrefabNumber = Random.Range(0, _bloodHoles.Length);
                Instantiate(_bloodHoles[randomPrefabNumber], _hit.point - _ray.direction * _projectorDistance, transform.rotation, _hit.transform);
            }

            if (_bloodSplatters != null)
            {
                var randomPrefabNumber = Random.Range(0, _bloodSplatters.Length);
                Instantiate(_bloodSplatters[randomPrefabNumber], _hit.point, transform.rotation);
            }

            if (enemy.Health > 0)
            {
                if (_isSmallArms)
                {
                    if (_hit.distance >= _maxDistance / 2)
                        enemy.TakeDamage(ReturnEnemyBodyPartHitDamage(_damage) / 3);
                    else if (_hit.distance >= _maxDistance / 3)
                        enemy.TakeDamage(ReturnEnemyBodyPartHitDamage(_damage) / 2);
                    else
                        enemy.TakeDamage(ReturnEnemyBodyPartHitDamage(_damage));
                }
                else
                    enemy.TakeDamage(ReturnEnemyBodyPartHitDamage(_damage));
            }
        }
        else
        if (_hit.collider.TryGetComponent(out Explosion explosion))
        {
            explosion.ExplodeWithDelay();

            if (_decalHitMetal)
                _decalHitWall = _decalHitMetal;
        }
        else
        {
            switch (_hit.transform.tag)
            {
                case Concrete:
                    if (_decalHitConcrete)
                        _decalHitWall = _decalHitConcrete;
                    break;
                case Metal:
                    if (_decalHitMetal)
                        _decalHitWall = _decalHitMetal;
                    break;
                case Wood:
                    if (_decalHitWood)
                        _decalHitWall = _decalHitWood;
                    break;
                case Dirt:
                    if (_decalHitDirt)
                        _decalHitWall = _decalHitDirt;
                    break;
                case Sand:
                    if (_decalHitSand)
                        _decalHitWall = _decalHitSand;
                    break;
                case Cloth:
                    break;
                case Softbody:
                default:
                    if (_decalHitSoftBody)
                        _decalHitWall = _decalHitSoftBody;
                    break;
            }
        }

        RenderHitOnSurface();

        if (_hit.collider.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddForceAtPosition(transform.forward * _force, _hit.point);
    }

    private void RenderHitOnSurface()
    {
        if (_decalHitWall)
        {
            ParticleSystem decalHitWall;

            if (!_hit.collider.gameObject.isStatic)
            {
                var hitScaleX = _hit.transform.localScale.x;
                var hitScaleY = _hit.transform.localScale.y;
                var hitScaleZ = _hit.transform.localScale.z;

                decalHitWall = Instantiate(_decalHitWall, _hit.point + _hit.normal * _floatInFrontOfWall, Quaternion.LookRotation(_hit.normal), _hit.transform);

                var decalScaleX = decalHitWall.transform.localScale.x;
                var decalScaleY = decalHitWall.transform.localScale.y;
                var decalScaleZ = decalHitWall.transform.localScale.z;

                Vector3 newDecalScale = new Vector3(decalScaleX / hitScaleX, decalScaleY / hitScaleY, decalScaleZ / hitScaleZ);
                decalHitWall.transform.localScale = newDecalScale;
            }
            else
            {
                decalHitWall = Instantiate(_decalHitWall, _hit.point + _hit.normal * _floatInFrontOfWall, Quaternion.LookRotation(_hit.normal));
            }

            if (decalHitWall.TryGetComponent(out AudioSource audioSource) && _hitSound)
                audioSource.PlayOneShot(_hitSound, Settings.Volume);
        }
    }

    private int ReturnEnemyBodyPartHitDamage(int damage)
    {
        switch (_hit.transform.tag)
        {
            case Head:
                damage *= 2;
                break;
            case Arm:
            case Foot:
                damage /= 2;
                break;
            case Body:
            default:
                break;
        }

        return damage;
    }
}