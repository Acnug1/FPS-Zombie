using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Create New Enemy", order = 51)]
public class Enemies : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private float _bodyDestroyTime;
    [SerializeField] private AudioClip _hitEnemy;
    [SerializeField] private AudioClip _deathEnemy;

    public int Health => _health;
    public int Reward => _reward;
    public float BodyDestroyTime => _bodyDestroyTime;
    public AudioClip HitEnemy => _hitEnemy;
    public AudioClip DeathEnemy => _deathEnemy;
}
