using System.Collections.Generic;
using UnityEngine;

public class Tile_Manager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridLength = 10;
    [SerializeField] private int gridHeight = 0;
    [SerializeField] private Transform groundFloorsParent;

    Dictionary<Vector3Int, Tile> tileMap;

    float tileSize = 3f;



    Tile tile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {
                Vector3 tilePos = new Vector3(x * tileSize, gridHeight, z * tileSize);
                GameObject tileGameObject = Instantiate(tilePrefab, tilePos, Quaternion.identity, groundFloorsParent);

                Tile tile = tileGameObject.GetComponent<Tile>();
                //intializes position to tile. Makes said tile walkable.
                tile.Initialize(new Vector3Int(x, 0, z), true);


            }
        }

                foreach (var kvp in tileMap)
        {
            Vector3Int pos = kvp.Key;
            Tile tile = kvp.Value;

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
