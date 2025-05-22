using System.Collections.Generic;
using UnityEngine;

public class CellsSpawner : MonoBehaviour
{
    [SerializeField] Dictionary<int, GameObject> prefabsDictionary;
    [SerializeField] GameObject testPrefab;
    [SerializeField] Transform parent;

    private void Start()
    {
        Builder.Instance.OnBuildCell += Builder_OnBuildCell;
    }

    private void Builder_OnBuildCell(object sender, Builder.OnBuildCellArgs e)
    {
        SpawnCell(e.cellsData.prefab, e.worldPosition);
    }

    public void SpawnCell(GameObject prefab, Vector3 position)
    {
        //if(prefabsDictionary.TryGetValue(idCells, out var prefab))
        //{
        //    Instantiate(prefab, position, Quaternion.identity, parent);
        //}
        Instantiate(prefab, position, Quaternion.identity, parent);

        
    }
}
