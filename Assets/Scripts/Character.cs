using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Character : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 3f;
    private List<Vector3> path;
    public Tile characterTile;
    private Tile_Manager TileManager;
    private int currentTargetIndex = 0;
    private Vector3 gridPos;

    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        currentTargetIndex = 0;
    }

    void Update()
    {

        if (path == null || currentTargetIndex >= path.Count)
            return;

        Vector3 target = path[currentTargetIndex];
        Vector3 direction = (target - transform.position).normalized;
        float step = moveSpeed * Time.deltaTime;


        transform.position = Vector3.MoveTowards(transform.position, target, step);


        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentTargetIndex++;
        }

        float tileSize = 3f;

        int x = Mathf.RoundToInt(transform.position.x / tileSize);
        int z = Mathf.RoundToInt(transform.position.z / tileSize);
        Vector3Int gridPos = new Vector3Int(x, 0, z);
        
        characterTile = TileManager.tileMap[gridPos];

    }
}
