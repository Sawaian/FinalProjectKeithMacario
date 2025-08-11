using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private List<Vector3> path;
    private int currentTargetIndex = 0;

    public Tile characterTile;
    private Tile_Manager tileManager;
    private Vector3Int gridPos;

    private Rigidbody rb;
    public Animator animator;

    [Header("Animation")]
    [SerializeField] string speedParam = "Speed";
    [SerializeField] float idle = 0.05f;

    private float tileSize = 3f;
    private Vector3Int lastTargetGridPos = Vector3Int.one * int.MinValue;

    public void SetPath(List<Vector3> newPath) { path = newPath; currentTargetIndex = 0; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.freezeRotation = true;
        if (animator) animator.applyRootMotion = false;
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

        if (tileManager && tileManager.tileMap.ContainsKey(gridPos))
            characterTile = tileManager.tileMap[gridPos];

        var pathfinder = GetComponent<Enemy_Pathfinding>();
        if (pathfinder && pathfinder.targetObject)
        {
            Vector3 t = pathfinder.targetObject.transform.position;
            var tgtGrid = new Vector3Int(Mathf.RoundToInt(t.x / tileSize), 0, Mathf.RoundToInt(t.z / tileSize));
            if (tgtGrid != lastTargetGridPos && tileManager && tileManager.tileMap.TryGetValue(tgtGrid, out Tile tile))
            {
                pathfinder.FindPath(gameObject, tile);
                lastTargetGridPos = tgtGrid;
            }
        }
    }


    void FixedUpdate()
    {
     
        Vector3 positionBeforeMovement = rb.position;

        if (path == null || currentTargetIndex >= path.Count)
        {
            if (animator) animator.SetFloat(speedParam, 0f);
            return;
        }

        Vector3 pos = rb.position;
        Vector3 target = path[currentTargetIndex];
        Vector3 toTarget = target - pos; toTarget.y = 0f;

        if (toTarget.sqrMagnitude > 0.0001f)
        {
            Vector3 step = toTarget.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(pos + step);
        }

        if (Vector3.Distance(rb.position, target) < 0.1f)
            currentTargetIndex++;
            
            
//animation isn't working. Need to figure out WHY!?

        float frameSpeed = (rb.position - positionBeforeMovement).magnitude / Time.fixedDeltaTime;
        float norm = Mathf.Clamp01(frameSpeed / Mathf.Max(moveSpeed, 0.0001f));
        if (norm < idle) norm = 0f;
        if (animator) animator.SetFloat(speedParam, norm);
    }
}