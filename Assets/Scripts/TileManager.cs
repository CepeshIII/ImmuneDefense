using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap tilemap;
    [SerializeField] private InputBinding inputAction;


    private void OnEnable()
    {
        //tilemap = GetComponent<Tilemap>();
    }

    public void SetTile(Tile tile, Vector3Int position)
    {
        tilemap.SetTile(position, tile);
    }
}
