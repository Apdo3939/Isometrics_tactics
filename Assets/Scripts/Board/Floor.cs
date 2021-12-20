using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Floor : MonoBehaviour
{
    [HideInInspector]
    public TilemapRenderer tilemapRenderer;
    public int order { get { return tilemapRenderer.sortingOrder; } }
    public int orderContent;
    public Vector3Int minXY;
    public Vector3Int maxXY;
    [HideInInspector]
    public Tilemap tilemap;
    [HideInInspector]
    public Tilemap highlight;
    public int height;

    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemap = GetComponent<Tilemap>();
        highlight = transform.parent.Find("HighLight").GetComponent<Tilemap>();
    }

    public List<Vector3Int> LoadTiles()
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        for (int x = minXY.x; x <= maxXY.x; x++)
        {
            for (int y = minXY.y; y <= maxXY.y; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(currentPos)) { tiles.Add(currentPos); }
            }
        }
        return tiles;
    }
}
