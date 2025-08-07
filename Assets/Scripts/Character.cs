using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private List<Vector3> path;
    public Tile characterTile;
    private Tile_Manager tileManager;
    private int currentTargetIndex = 0;
    private Vector3Int gridPos;

    private float tileSize = 3f;
    private Vector3Int lastTargetGridPos = Vector3Int.one * int.MinValue; 
    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        currentTargetIndex = 0;
    }

    void Start()
    {
        tileManager = Object.FindFirstObjectByType<Tile_Manager>();
    }

    void Update()
    {
       
        int x = Mathf.RoundToInt(transform.position.x / tileSize);
        int z = Mathf.RoundToInt(transform.position.z / tileSize);
        gridPos = new Vector3Int(x, 0, z);

        if (tileManager.tileMap.ContainsKey(gridPos))
            characterTile = tileManager.tileMap[gridPos];

        // update path cause it was standing still
        Enemy_Pathfinding pathfinder = GetComponent<Enemy_Pathfinding>();
        if (pathfinder != null && pathfinder.targetObject != null)
        {
            Vector3 targetPos = pathfinder.targetObject.transform.position;
            int targetX = Mathf.RoundToInt(targetPos.x / tileSize);
            int targetZ = Mathf.RoundToInt(targetPos.z / tileSize);
            Vector3Int currentTargetGridPos = new Vector3Int(targetX, 0, targetZ);

            if (currentTargetGridPos != lastTargetGridPos)
            {
                if (tileManager.tileMap.TryGetValue(currentTargetGridPos, out Tile targetTile))
                {
                    pathfinder.FindPath(gameObject, targetTile);
                    lastTargetGridPos = currentTargetGridPos;
                }
            }
        }

    
        if (path == null || currentTargetIndex >= path.Count)
            return;

        Vector3 target = path[currentTargetIndex];
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.1f)
            currentTargetIndex++;
    }
}
