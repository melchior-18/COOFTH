using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class SmokePropagator : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Tile SmokeFloor;
    [SerializeField] private Tile FloorTile;
    [SerializeField] private Tile EscapeRoute;
    
    [SerializeField] private List<Vector2Int> allPositions = new List<Vector2Int>();

    [SerializeField] private float lastUpdate = -Mathf.Infinity;

    private GameObject smoke;

    private GameObject player;
    private Vector2 playerPos;

    private float tileWidth;

    private Vector2 posEscapeRouteTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var pos in floorTileMap.cellBounds.allPositionsWithin)
        {
            Vector2Int localPlace = new Vector2Int(pos.x, pos.y);
            allPositions.Add(localPlace);
        }

        smoke = GameObject.Find("Smoke");

        player = GameObject.Find("Idle_0");

        tileWidth = floorTileMap.cellSize.x;
        foreach (var pos in wallTileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int pos3D = new Vector3Int(pos.x, pos.y, 0);
            if (wallTileMap.GetTile(pos3D).name == EscapeRoute.name)
            {
                posEscapeRouteTile = new Vector2Int(pos.x, pos.y);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdate > .3f)
        {
            lastUpdate = Time.time;

            player.GetComponent<charact�recontroleur>().IsBreathing = true;

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
                    if (floorTileMap.GetTile(pos3D).name == FloorTile.name)
                    {
                        print("and floor tile");
                        floorTileMap.SetTile(pos3D, SmokeFloor);
                    }
                }

                playerPos = player.transform.position;
                if ((playerPos - pos).sqrMagnitude < tileWidth * 1.4f && floorTileMap.GetTile(pos3D) == SmokeFloor)
                {
                    player.GetComponent<charact�recontroleur>().IsBreathing = false;
                    player.GetComponent<charact�recontroleur>().moveSpeed = 3;
                }
            }
        }
        //EscapeRoute  [SerializeField] private Tile EscapeRoute;
        if ((playerPos - posEscapeRouteTile).sqrMagnitude < tileWidth * 0.9f ) // calcul la distance entre le joueur et la tuile de gagner
        {
            SceneManager.LoadScene("victory"); // si on touche la tuile d'escape alors on gagne et on arrive au menu de victoire.
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