using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSkillRange : SkillRange
{
    public override List<TileLogic> GetTilesInRange(Board board)
    {
        return new List<TileLogic>(board.tiles.Values);
    }
}
