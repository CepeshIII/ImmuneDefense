using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CellsData", menuName = "ScriptableObjects/CellsData", order = 1)]
[Serializable]
public class CellsData: ScriptableObject
{
    public Tile tile;
    public GameObject prefab;
    public Sprite menuSprite;
}
