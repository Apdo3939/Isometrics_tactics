using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSkillRange : SkillRange
{
    public override bool IsDirectionOriented()
    {
        return true;
    }
    public override List<TileLogic> GetTilesInRange()
    {
        UnitCharacter unit = Turn.unitCharacter;
        Vector3Int pos = unit.tile.pos;
        List<TileLogic> retValue = new List<TileLogic>();
        //int dir = (unit.direction == "North" || unit.direction == "East") ? 1 : -1;
        int side = 1;

        for (int i = 1; i <= range; i++)
        {
            int min = -(side / 2);
            int max = (side / 2);
            for (int j = min; j <= max; j++)
            {
                Vector3Int next = GetNext(unit.direction, pos, i, j);
                TileLogic tile = Board.GetTile(next);
                if (ValidTile(tile))
                {
                    retValue.Add(tile);
                }
            }
            side += 2;
        }

        return retValue;
    }

    bool ValidTile(TileLogic t)
    {
        return t != null && Mathf.Abs(t.floor.height - Turn.unitCharacter.tile.floor.height) <= verticalRange;
    }

    Vector3Int GetNext(string orientation, Vector3Int pos, int a, int b)
    {
        Vector3Int next = Vector3Int.zero;

        switch (orientation)
        {
            case "North":
                next = new Vector3Int(pos.x + b, pos.y + a, 0);
                break;
            case "South":
                next = new Vector3Int(pos.x + b, pos.y - a, 0);
                break;
            case "East":
                next = new Vector3Int(pos.x + a, pos.y + b, 0);
                break;
            case "West":
                next = new Vector3Int(pos.x - a, pos.y + b, 0);
                break;
        }
        return next;
    }
}
