using System;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public static Builder Instance { get; private set; }

    public CellsData cellForPlaceData;

    public event EventHandler OnBuilderEnable;
    public event EventHandler OnBuilderDisable;

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

    public event EventHandler<OnCellDataUpdateArgs> OnCellDataUpdate;
    public class OnCellDataUpdateArgs: EventArgs
    {
        public CellsData newCellData;
    }

    private void OnEnable()
    {
        OnBuilderEnable?.Invoke(this, new EventArgs());
        cellForPlaceData = null;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnGameWin += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    public void UpdateCellForPlace(CellsData cellsData)
    {
        cellForPlaceData = cellsData;
        OnCellDataUpdate.Invoke(this, new OnCellDataUpdateArgs
        {
            newCellData = cellsData
        });
    }

    public void BuildByPosition(Vector3Int gridPosition, Vector3 worldPosition)
    {
        if (cellForPlaceData == null) return;

        if(GameManager.Instance.GetGameStats().countOfAntibody >= cellForPlaceData.price) 
        { 
            OnBuildCell.Invoke(this, new OnBuildCellArgs 
            { 
                cellsData = cellForPlaceData, 
                gridPosition = gridPosition, 
                worldPosition = worldPosition
            });

            GameManager.Instance.TriggerOnCellSet(cellForPlaceData.price);
        }

    }

    public void ClickOnBuilder(Vector3 worldMousePosition)
    {
        OnClick.Invoke(this, new OnClickArgs { worldMousePosition = worldMousePosition });
    }


    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }

    private void OnDisable()
    {
        OnBuilderDisable?.Invoke(this, new EventArgs());
    }


}
