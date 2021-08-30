using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private EndPoint _endPoint;

    private SpawnTrigger[] _spawnTriggers;

    private void Awake()
    {
        _endPoint.gameObject.SetActive(false);
        _spawnTriggers = GetComponentsInChildren<SpawnTrigger>();
    }

    private void OnEnable()
    {
        foreach (var spawnTrigger in _spawnTriggers)
        {
            spawnTrigger.Reached += OnSpawnTriggerReached;
        }
    }

    private void OnDisable()
    {
        foreach (var spawnTrigger in _spawnTriggers)
        {
            spawnTrigger.Reached -= OnSpawnTriggerReached;
        }
    }

    private void OnSpawnTriggerReached()
    {
        foreach (var spawnTrigger in _spawnTriggers)
        {
            if (!spawnTrigger.IsReached)
                return;
        }

        _endPoint.gameObject.SetActive(true);
    }
}
