using System;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public static Builder Instance { get; private set; }

    public CellsData dataCellsForPlace;
    public TileManager tileManager;



    public event EventHandler<OnBuildCellArgs> OnBuildCell;
    public class OnBuildCellArgs: EventArgs
    {
        public CellsData dataCellsForPlace;
        public Vector3 position;
    }



    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    public void UpdateCellForPlace(CellsData cellsData)
    {
        dataCellsForPlace = cellsData;
    }


    public void BuildByPosition(Vector3 worldPosition)
    {
        var gridPosition = tileManager.tilemap.WorldToCell(worldPosition);
        gridPosition.z = 0;
        if (tileManager.tilemap.HasTile(gridPosition))
        {
            tileManager.SetTile(dataCellsForPlace.tile, gridPosition);
            OnBuildCell.Invoke(this, new OnBuildCellArgs 
            { 
                dataCellsForPlace = dataCellsForPlace, 
                position = tileManager.tilemap.CellToWorld(gridPosition)
            });
        }
    }
}
