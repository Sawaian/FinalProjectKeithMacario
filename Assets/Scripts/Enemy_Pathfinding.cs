using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Enemy_Pathfinding : MonoBehaviour
{
    public GameObject targetObject;
    private Tile destinationTile;
    private Tile_Manager tileManager;
    Character character;
    List<Tile> openSet = new List<Tile>();
    HashSet<Tile> closedSet = new HashSet<Tile>();

    //coroutines. M
    private IEnumerator Start()
    {
        character = GetComponent<Character>();
        tileManager = Object.FindFirstObjectByType<Tile_Manager>();

        while (character.characterTile == null)
            yield return null;

        if (targetObject != null)
        {
            Vector3 targetPos = targetObject.transform.position;

            int x = Mathf.RoundToInt(targetPos.x / 3f);
            int z = Mathf.RoundToInt(targetPos.z / 3f);
            Vector3Int gridPos = new Vector3Int(x, 0, z);

            if (!tileManager.tileMap.TryGetValue(gridPos, out destinationTile))
            {
                Debug.LogWarning("Destination tile not found!");
                yield break;
            }

            if (!destinationTile.isWalkable)
            {
                Debug.LogWarning("Destination tile is not walkable!");
                yield break;
            }

            FindPath(character.gameObject, destinationTile);
        }
    }

    public void FindPath(GameObject characterObject, Tile destination)
    {
        Tile origin = characterObject.GetComponent<Character>().characterTile;

        openSet.Clear();
        closedSet.Clear();

        openSet.Add(origin);
        origin.gCost = 0;
        origin.hCost = Vector3.Distance(origin.transform.position, destination.transform.position);
        origin.parentTile = null;

        while (openSet.Count > 0)
        {
            Tile current = openSet[0];
            foreach (Tile tile in openSet)
            {
                if (tile.fCost < current.fCost ||
                   (tile.fCost == current.fCost && tile.hCost < current.hCost))
                {
                    current = tile;
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            //If destination reach walkback.
            if (current == destination)
            {
                List<Tile> finalPath = new List<Tile>();
                Tile currentTile = destination;

                while (currentTile != origin)
                {
                    finalPath.Add(currentTile);
                    currentTile = currentTile.parentTile;
                }

                finalPath.Add(origin);
                finalPath.Reverse();

                List<Vector3> pathPositions = new List<Vector3>();
                foreach (Tile tile in finalPath)
                {
                    pathPositions.Add(tile.transform.position);
                }

                characterObject.GetComponent<Character>().SetPath(pathPositions);
                Debug.Log("Path found. Final path length: " + pathPositions.Count);

                return;
            }

            foreach (Tile neighbor in current.neighbors)
            {
                if (closedSet.Contains(neighbor) || !neighbor.isWalkable)
                    continue;

                float tentativeGCost = current.gCost + Vector3.Distance(current.transform.position, neighbor.transform.position);

                if (tentativeGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = Vector3.Distance(neighbor.transform.position, destination.transform.position);
                    neighbor.parentTile = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }
}