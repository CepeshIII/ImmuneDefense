using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private IPathSource pathForMove;
    [SerializeField] private Transform parent;

    private float spawnDelay = 0;
    private float spawnTimer = 0;

    private int enemyForSpawnCount = 0;

    private IPathSource pathSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathSource = GetComponent<IPathSource>();
        PathMoverInstantiate();
    }

    public void Update()
    {
        if (enemyForSpawnCount <= 0) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            enemyForSpawnCount--;
            PathMoverInstantiate();
            spawnTimer = spawnDelay;
        }
    }

    public void Spawn(int count, float spawnDelay)
    {
        this.spawnDelay = spawnDelay;
        spawnTimer = spawnDelay;

        enemyForSpawnCount = count;
    }

    private void PathMoverInstantiate()
    {
        var prefab = GetRandomPrefab();
        if (prefab == null) return;

        var pathMover = Instantiate(prefab, transform.position, Quaternion.identity, parent);
        if(pathMover.TryGetComponent<MoveByPath>(out var moveByPath))
        {
            moveByPath.SetPath(pathSource.GetPath());
        }
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
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
