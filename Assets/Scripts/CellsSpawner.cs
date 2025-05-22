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
        SpawnCell(e.dataCellsForPlace.cellsId, e.position);
    }

    public void SpawnCell(int idCells, Vector3 position)
    {
        //if(prefabsDictionary.TryGetValue(idCells, out var prefab))
        //{
        //    Instantiate(prefab, position, Quaternion.identity, parent);
        //}
        Instantiate(testPrefab, position, Quaternion.identity, parent);

        
    }
}
