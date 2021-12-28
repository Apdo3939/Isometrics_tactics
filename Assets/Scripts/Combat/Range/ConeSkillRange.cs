using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSkillRange : SkillRange
{
    void Awake()
    {
        directionOriented = true;
    }

    public override List<TileLogic> GetTilesInRange(Board board)
    {
        UnitCharacter unit = Turn.unitCharacter;
        Vector3Int pos = unit.tile.pos;
        List<TileLogic> retValue = new List<TileLogic>();
        int dir = (unit.direction == "North" || unit.direction == "East") ? 1 : -1;
        int side = 1;

        if (unit.direction == "North" || unit.direction == "South")
        {
            for (int y = 1; y <= range; y++)
            {
                int min = -(side / 2);
                int max = (side / 2);
                for (int x = min; x <= max; x++)
                {
                    Vector3Int next = new Vector3Int(pos.x + x, pos.y + (y * dir), 0);
                    TileLogic tile = Board.GetTile(next);
                    if (ValidTile(tile))
                    {
                        retValue.Add(tile);
                    }
                }
                side += 2;
            }
        }
        else
        {

            for (int x = 1; x <= range; x++)
            {
                int min = -(side / 2);
                int max = (side / 2);
                for (int y = min; y <= max; y++)
                {
                    Vector3Int next = new Vector3Int(pos.x + (x * dir), pos.y + y, 0);
                    TileLogic tile = Board.GetTile(next);
                    if (ValidTile(tile))
                    {
                        retValue.Add(tile);
                    }
                }
                side += 2;
            }

        }
        return retValue;
    }

    bool ValidTile(TileLogic t)
    {
        return t != null && Mathf.Abs(t.floor.height - Turn.unitCharacter.tile.floor.height) <= verticalRange;
    }
}
