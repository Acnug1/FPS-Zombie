using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnTrigger : MonoBehaviour
{
    private Spawner[] _spawners;
    private bool _isReached;

    public event UnityAction Reached;

    public bool IsReached => _isReached;

    private void Start()
    {
        _spawners = GetComponentsInChildren<Spawner>();

        foreach (var spawner in _spawners)
        {
            spawner.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isReached)
            return;

        if (other.TryGetComponent(out Player player))
        {
            _isReached = true;
            Reached?.Invoke();

            foreach (var spawner in _spawners)
            {
                spawner.gameObject.SetActive(true);
                spawner.InitPlayer(player);
            }
        }
    }
}
