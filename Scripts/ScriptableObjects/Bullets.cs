using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullets/Create New Bullet", order = 51)]
public class Bullets : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _force;
    [SerializeField] private float _maxDistance;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private ParticleSystem _decalHitConcrete;
    [SerializeField] private ParticleSystem _decalHitMetal;
    [SerializeField] private ParticleSystem _decalHitWood;
    [SerializeField] private ParticleSystem _decalHitDirt;
    [SerializeField] private ParticleSystem _decalHitSand;
    [SerializeField] private ParticleSystem _decalHitSoftBody;
    [SerializeField] private AudioClip _enemyHitSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private bool _isSmallArms;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private Transform[] _bloodHoles;
    [SerializeField] private Transform[] _bloodSplatters;

    public int Damage => _damage;
    public float Force => _force;
    public float MaxDistance => _maxDistance;
    public ParticleSystem BloodEffect => _bloodEffect;
    public ParticleSystem DecalHitConcrete => _decalHitConcrete;
    public ParticleSystem DecalHitMetal => _decalHitMetal;
    public ParticleSystem DecalHitWood => _decalHitWood;
    public ParticleSystem DecalHitDirt => _decalHitDirt;
    public ParticleSystem DecalHitSand => _decalHitSand;
    public ParticleSystem DecalHitSoftBody => _decalHitSoftBody;
    public AudioClip EnemtHitSound => _enemyHitSound;
    public AudioClip HitSound => _hitSound;
    public bool IsSmallArms => _isSmallArms;
    public LayerMask IgnoreLayer => _ignoreLayer;
    public Transform[] BloodHoles => _bloodHoles;
    public Transform[] BloodSplatters => _bloodSplatters;
}
