using System.Collections.Generic;
using UnityEngine;

public class Tile_Manager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridLength = 10;
    [SerializeField] private int gridHeight = 0;
    [SerializeField] private Transform groundFloorsParent;

    public Dictionary<Vector3Int, Tile> tileMap;

    float tileSize = 3f;



    Tile tile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        tileMap = new Dictionary<Vector3Int, Tile>();


        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {
                Vector3 tilePos = new Vector3(x * tileSize, gridHeight, z * tileSize);
                GameObject tileGameObject = Instantiate(tilePrefab, tilePos, Quaternion.identity, groundFloorsParent);

                Tile tile = tileGameObject.GetComponent<Tile>();
                //intializes position to tile. Makes said tile walkable.

                Vector3Int gridPos = new Vector3Int(x, 0, z);
                tile.Initialize(gridPos, true);
                tileMap.Add(gridPos, tile);


            }
        }

                foreach (var KeyValuePair in tileMap)
        {
            Vector3Int pos = KeyValuePair.Key;
            Tile tile = KeyValuePair.Value;

            Vector3Int[] directions = new Vector3Int[]
            {
                //Right, Left, Up, Down
                new Vector3Int(1, 0, 0),   
                new Vector3Int(-1, 0, 0),  
                new Vector3Int(0, 0, 1),   
                new Vector3Int(0, 0, -1), 
                //Diagnoals  
                new Vector3Int( 1, 0,  1),  
                new Vector3Int(-1, 0,  1),  
                new Vector3Int( 1, 0, -1),  
                new Vector3Int(-1, 0, -1),  
            };

            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighborPos = pos + dir;

                if (tileMap.TryGetValue(neighborPos, out Tile neighbor))
                {
                    if (neighbor.isWalkable) 
                        tile.neighbors.Add(neighbor);
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
