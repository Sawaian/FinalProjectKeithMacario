using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class Enemy_Pathfinding : MonoBehaviour
{

    Character character;
    List<Tile> openSet = new List<Tile>();
    HashSet<Tile> closedSet = new HashSet<Tile>();



    private void Start()
    {

    }

    public void FindPath(GameObject character, Tile destination)
    {
        Tile origin = character.GetComponent<Character>().characterTile;

        Tile current = openSet[0];

        while (openSet.Count > 0)
        {
            foreach (Tile tile in openSet)
            {

                if (tile.fCost < current.fCost ||
            (tile.fCost == current.fCost && tile.hCost < current.hCost))
                {
                    current = tile;

                }


                openSet.Remove(current);
                closedSet.Add(current);

                if (current == destination)
                {
                    foreach (Tile neighbor in current.neighbors)
                    {
                        //CONTAINS! DUH! 
                        if (closedSet.Contains(neighbor))
                        {
                            continue;
                        }
                        float tentativeGCost = current.gCost + Vector3.Distance(current.transform.position, neighbor.transform.position);
                        if (tentativeGCost < neighbor.gCost || !openSet.Contains(neighbor))
                        {
                            neighbor.gCost = tentativeGCost;
                            neighbor.hCost = Vector3.Distance(neighbor.transform.position, destination.transform.position);
                            neighbor.parentTile = current;

                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                    return;
                }

            }
        }
    }
}
