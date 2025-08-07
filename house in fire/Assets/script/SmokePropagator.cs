using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class SmokePropagator : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Tile SmokeFloor;

    [SerializeField] private List<Vector2Int> allPositions = new List<Vector2Int>();

    [SerializeField] private float lastUpdate = -Mathf.Infinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var pos in floorTileMap.cellBounds.allPositionsWithin)
        {
            Vector2Int localPlace = new Vector2Int(pos.x, pos.y);
            allPositions.Add(localPlace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdate > 3f)
        {
            lastUpdate = Time.time;

            foreach (var pos in allPositions)
            {
                Vector3Int pos3D = new Vector3Int(pos.x, pos.y, 0);

                int compteurFumee = 0;

                Vector3Int[] directions = new Vector3Int[]
                {
                    Vector3Int.up,
                    Vector3Int.down,
                    Vector3Int.left,
                    Vector3Int.right,
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(-1, 1, 0),
                    new Vector3Int(1, -1, 0),
                    new Vector3Int(-1, -1, 0),
                };

                foreach (var dir in directions)
                {
                    Vector3Int neighborPos = pos3D + dir;

                    if (!wallTileMap.GetTile(neighborPos) && floorTileMap.GetTile(neighborPos) == SmokeFloor)
                    {
                        compteurFumee++;
                    }
                }

                if (Random.Range(0, 8) < (compteurFumee))
                {
                    floorTileMap.SetTile(pos3D, SmokeFloor);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var pos in allPositions)
        {
            Gizmos.DrawSphere(new Vector3(pos.x + 0.5f,pos.y + 0.5f, 0), 0.2f);
        }
    }
}