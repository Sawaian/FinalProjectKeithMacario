using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Vector3Int gridPos;
    public float hCost { get; set; }
    public float gCost { get; set; }
    public float fCost => gCost + hCost;

    public bool isWalkable;
    public List<Tile> neighbors;
    Renderer tileRenderer ;



    public Tile parentTile;
    public void Initialize(Vector3Int position, bool walkable)
    {
        gridPos = position;
        isWalkable = walkable;
    }

    void Awake()
    {
        neighbors = new List<Tile>();
        tileRenderer = GetComponent<Renderer>();
    }
}
