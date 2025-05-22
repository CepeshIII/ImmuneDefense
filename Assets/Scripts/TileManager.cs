using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Builder;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<Tile> availableForBuildingTiles;


    private void OnEnable()
    {
        //tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        Builder.Instance.OnClick += Builder_OnClick;
        Builder.Instance.OnBuildCell += Builder_OnBuildCell; ;
    }

    private void Builder_OnBuildCell(object sender, OnBuildCellArgs e)
    {
        SetTile(e.cellsData.tile, e.gridPosition);
    }

    private void Builder_OnClick(object sender, Builder.OnClickArgs e)
    {
        var gridPosition = tilemap.WorldToCell(e.worldMousePosition);
        gridPosition.z = 0;

        CheckIfTileAvailable(gridPosition);
    }

    private void CheckIfTileAvailable(Vector3Int gridPosition)
    {
        if (tilemap.HasTile(gridPosition))
        {
            var currentTile = tilemap.GetTile(gridPosition);
            foreach (var tile in availableForBuildingTiles) 
            {
                if(currentTile.Equals(tile))
                {
                    Builder.Instance.BuildByPosition(gridPosition, tilemap.CellToWorld(gridPosition));
                }
            }
        }
    }

    public void SetTile(Tile tile, Vector3Int position)
    {
        tilemap.SetTile(position, tile);
    }
}
