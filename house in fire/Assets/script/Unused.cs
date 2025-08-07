using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileColorChanger : MonoBehaviour
{
    public Tilemap tilemap;
    public Color targetColor = Color.grey;
    public float delayBetweenTiles = 0.2f;

    private HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

    void Update()
    {
        // Clique gauche = changement de couleur à partir de la cellule cliquée
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ChangeColorFromPosition(mouseWorldPos);
        }
    }

    public void ChangeColorFromPosition(Vector2 worldPos)
    {
        Vector3Int cellPos = tilemap.WorldToCell((Vector3)worldPos);
        visited.Clear(); // reset pour une nouvelle propagation
        Color originalColor = tilemap.GetColor(cellPos);
        StartCoroutine(ChangeTileColorRecursive(cellPos, originalColor, 0f));
    }

    IEnumerator ChangeTileColorRecursive(Vector3Int position, Color originalColor, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!tilemap.HasTile(position)) yield break;
        if (visited.Contains(position)) yield break;

        Color currentColor = tilemap.GetColor(position);
        if (currentColor != originalColor) yield break;

        tilemap.SetColor(position, targetColor);
        visited.Add(position);

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (var dir in directions)
        {
            Vector3Int neighbor = position + dir;
            if (tilemap.HasTile(neighbor) && !visited.Contains(neighbor))
            {
                StartCoroutine(ChangeTileColorRecursive(neighbor, originalColor, delayBetweenTiles));
            }
        }
    }
}