using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : EnemyPool
{
    [SerializeField] private Transform _path;
    [SerializeField] Enemy[] _templateEnemy;
    [SerializeField] float _delay;
    [SerializeField] int _countEnemies;

    private Player _player;
    private float _timeAfterLastSpawn;
    private int _spawnedEnemy;

    private void Start()
    {
        Initialize(_templateEnemy);
    }

    public void InitPlayer(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        SetWave();
    }

    private void SetWave()
    {
        if (_countEnemies > _spawnedEnemy)
        {
            _timeAfterLastSpawn += Time.deltaTime;

            if (_timeAfterLastSpawn >= _delay)
            {
                if (TryGetEnemy(out Enemy enemy))
                {
                    _timeAfterLastSpawn = 0;

                    SetEnemy(enemy, SpawnPoint.position);
                    _spawnedEnemy++;
                }
            }
        }
    }

    private void SetEnemy(Enemy enemy, Vector3 spawnPoint)
    {
        enemy.transform.position = spawnPoint;
        enemy.gameObject.SetActive(true);
        enemy.Init(_player, _path);
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}