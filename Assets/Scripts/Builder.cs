using System;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public static Builder Instance { get; private set; }

    public CellsData cellForPlaceData;

    public event EventHandler<OnClickArgs> OnClick;
    public class OnClickArgs : EventArgs
    {
        public Vector3 worldMousePosition;
    }

    public event EventHandler<OnBuildCellArgs> OnBuildCell;
    public class OnBuildCellArgs: EventArgs
    {
        public CellsData cellsData;
        public Vector3Int gridPosition;
        public Vector3 worldPosition;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void UpdateCellForPlace(CellsData cellsData)
    {
        cellForPlaceData = cellsData;
    }

    public void BuildByPosition(Vector3Int gridPosition, Vector3 worldPosition)
    {
        if (cellForPlaceData == null) return;

        OnBuildCell.Invoke(this, new OnBuildCellArgs 
        { 
            cellsData = cellForPlaceData, 
            gridPosition = gridPosition, 
            worldPosition = worldPosition
        });
    }

    public void ClickOnBuilder(Vector3 worldMousePosition)
    {
        OnClick.Invoke(this, new OnClickArgs { worldMousePosition = worldMousePosition });
    }
}
