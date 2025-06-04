using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private IPathSource pathForMove;
    [SerializeField] private Transform parent;
    [SerializeField] private List<GameObject> spawnedEnemy;
    [SerializeField] private float spawnRadius = 1f;

    private float spawnTimer = 0;

    private EnemyWave currentEnemyWave;
    private IPathSource pathSource;



    private void Start()
    {
        pathSource = GetComponent<IPathSource>();
    }


    private void Update()
    {
        if(spawnedEnemy != null && spawnedEnemy.Count != 0) 
        {
            var isEveryoneKilled = true;

            foreach (var e in spawnedEnemy) 
            { 
                if(e != null)
                {
                    isEveryoneKilled = false;
                }
            }

            if (isEveryoneKilled) 
            {
                spawnedEnemy.Clear();
                EnemyWavesManager.Instance.TriggerOnWaveEnd();
            }
        }
    }


    private IEnumerator SpawnWave()
    {
        foreach (var enemyForSpawnData in currentEnemyWave.enemiesForSpawn)
        {
            for (int i = 0; i < enemyForSpawnData.numberOfEnemy; i++)
            {
                var enemiesPrefab = enemyForSpawnData.enemyPrefab;
                PathMoverInstantiate(enemiesPrefab);
                yield return new WaitForSeconds(spawnTimer);
            }
        }
    }


    public void SetWaveData(EnemyWave enemyWave, float spawnDelay)
    {
        StopAllCoroutines();
        spawnTimer = spawnDelay;
        currentEnemyWave = enemyWave;
        spawnedEnemy = new();

        StartCoroutine(SpawnWave());
    }


    private void PathMoverInstantiate(GameObject prefab)
    {
        //var prefab = GetRandomPrefab();
        if (prefab == null) return;

        var pathMover = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity, parent);
        spawnedEnemy.Add(pathMover);

        if (pathMover.TryGetComponent<IMoveByPath>(out var moveByPath))
        {
            moveByPath.SetPath(pathSource.GetPath());
        }
    }

    private Vector3 GetSpawnPosition() 
    {
        var distance = Random.value * spawnRadius;
        var angle = Random.Range(0f, 360f) * (Mathf.PI / 180f);
        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector3 offset = direction * distance;
        return offset + transform.position;
    }

    private GameObject GetRandomPrefab()
    {
        if (prefabs.Count > 0)
        {
            var index = Random.Range(0, prefabs.Count);
            return prefabs[index];
        }
        else
        {
            return null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}
