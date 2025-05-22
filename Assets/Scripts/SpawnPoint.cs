using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private IPathSource pathForMove;
    [SerializeField] private Transform parent;
    
    private IPathSource pathSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathSource = GetComponent<IPathSource>();
        PathMoverInstantiate();
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
}
