using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Dictionary<Vector3Int, TileLogic> tiles;
    public List<Floor> floors;
    public static Board instance;
    [HideInInspector]
    public Grid grid;
    public List<Tile> highlights;
    public Vector3Int[] dirs = new Vector3Int[4]{
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    void Awake()
    {
        instance = this;
        tiles = new Dictionary<Vector3Int, TileLogic>();
        grid = GetComponent<Grid>();
    }

    public IEnumerator InitSequence(LoadState loadState)
    {
        yield return StartCoroutine(LoadFloors(loadState));
        yield return null;
        ShadowOrdering();
        yield return null;
    }

    IEnumerator LoadFloors(LoadState loadState)
    {
        for (int i = 0; i < floors.Count; i++)
        {
            List<Vector3Int> floorTiles = floors[i].LoadTiles();
            yield return null;
            for (int j = 0; j < floorTiles.Count; j++)
            {
                if (!tiles.ContainsKey(floorTiles[j]))
                {
                    CreateTile(floorTiles[j], floors[i]);
                }
            }
        }
    }

    void CreateTile(Vector3Int pos, Floor floor)
    {
        Vector3 worldPos = grid.CellToWorld(pos);
        worldPos.y += (floor.tilemap.tileAnchor.y / 2) - 0.5f;
        TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
        tiles.Add(pos, tileLogic);
    }

    void ShadowOrdering()
    {
        foreach (TileLogic t in tiles.Values)
        {
            int floorIndex = floors.IndexOf(t.floor);
            floorIndex = 2;

            if (floorIndex >= floors.Count || floorIndex < 0)
            {
                continue;
            }
            Floor floorToCheck = floors[floorIndex];
            Vector3Int pos = t.pos;
            IsNLCheck(floorToCheck, t, pos + Vector3Int.right);
            IsNLCheck(floorToCheck, t, pos + Vector3Int.up);
            IsNLCheck(floorToCheck, t, pos + Vector3Int.right + Vector3Int.up);
        }
    }

    void IsNLCheck(Floor floorToCheck, TileLogic t, Vector3Int pos)
    {
        if (floorToCheck.tilemap.HasTile(pos))
        {
            t.contentOrder = floorToCheck.order;
        }
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        TileLogic tile = null;
        instance.tiles.TryGetValue(pos, out tile);

        return tile;
    }

    public void SelectTiles(List<TileLogic> tiles, int allianceIndex)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].floor.highlight.SetTile(tiles[i].pos, highlights[allianceIndex]);
        }
    }

    public void DeSelectTiles(List<TileLogic> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].floor.highlight.SetTile(tiles[i].pos, null);
        }
    }

    public List<TileLogic> Search(TileLogic start)
    {
        List<TileLogic> tilesSearch = new List<TileLogic>();
        Movement m = Turn.unitCharacter.GetComponent<Movement>();

        tilesSearch.Add(start);
        ClearSearch();

        Queue<TileLogic> checkNext = new Queue<TileLogic>();
        Queue<TileLogic> checkNow = new Queue<TileLogic>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            TileLogic t = checkNow.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                TileLogic next = GetTile(t.pos + dirs[i]);
                int movStat = Turn.unitCharacter.GetStat(StatEnum.MOV);
                if (next == null || next.distance <= t.distance + 1 || t.distance + 1 > movStat || m.ValidateMovement(t, next))
                {
                    continue;
                }
                next.distance = t.distance + 1;
                next.prev = t;
                checkNext.Enqueue(next);
                tilesSearch.Add(next);
            }
            if (checkNow.Count == 0) { SwapReference(ref checkNow, ref checkNext); }
        }


        return tilesSearch;
    }

    void ClearSearch()
    {
        foreach (TileLogic t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    void SwapReference(ref Queue<TileLogic> checkNow, ref Queue<TileLogic> checkNext)
    {
        Queue<TileLogic> temp = checkNow;
        checkNow = checkNext;
        checkNext = temp;
    }

}
