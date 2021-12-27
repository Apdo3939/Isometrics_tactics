using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSkillRange : SkillRange
{
    void Awake()
    {
        directionOriented = true;
    }
    public override List<TileLogic> GetTilesInRange(Board board)
    {
        UnitCharacter unit = Turn.unitCharacter;
        Vector3Int startPos = unit.tile.pos;
        Vector3Int direction;
        List<TileLogic> retValue = new List<TileLogic>();

        switch (unit.direction)
        {
            case "North":
                direction = new Vector3Int(0, 1, 0);
                break;
            case "South":
                direction = new Vector3Int(0, -1, 0);
                break;
            case "East":
                direction = new Vector3Int(1, 0, 0);
                break;
            default: //West
                direction = new Vector3Int(-1, 0, 0);
                break;
        }

        Vector3Int currentPos = startPos;

        for (int i = 1; i < range; i++)
        {
            currentPos += direction;
            TileLogic t = Board.GetTile(currentPos);

            if (t != null && Mathf.Abs(t.floor.height - unit.tile.floor.height) <= verticalRange)
            {
                retValue.Add(t);
            }
        }

        return retValue;
    }
}
