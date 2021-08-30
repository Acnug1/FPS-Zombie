using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] protected Transform SpawnPoint;

    private List<Enemy> _pool = new List<Enemy>();
    private NavMeshHit closestHit;

    protected void Initialize(Enemy[] prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, prefab.Length);

            Enemy spawned = Instantiate(prefab[randomIndex], SpawnPoint.position, Quaternion.identity);

            if (NavMesh.SamplePosition(SpawnPoint.position, out closestHit, 4f, NavMesh.AllAreas))
            {
                SpawnPoint.position = closestHit.position;
            }

            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetEnemy(out Enemy result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}