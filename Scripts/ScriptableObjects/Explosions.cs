using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "Explosions/Create New Explosion", order = 51)]
public class Explosions : ScriptableObject
{
    [SerializeField] private int _damageForHitBox;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _explodeDelay;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private Transform[] _bloodSplatters;

    public int DamageForHitBox => _damageForHitBox;
    public float Radius => _radius;
    public float Force => _force;
    public float ExplodeDelay => _explodeDelay;
    public ParticleSystem ExplosionEffect => _explosionEffect;
    public AudioClip ExplosionSound => _explosionSound;
    public ParticleSystem BloodEffect => _bloodEffect;
    public Transform[] BloodSplatters => _bloodSplatters;
}
