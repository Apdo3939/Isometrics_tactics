using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSkillRange : SkillRange
{
    bool SearchType(TileLogic from, TileLogic to)
    {
        to.distance = from.distance + 1;
        return (from.distance + 1) <= range &&
        Mathf.Abs(to.floor.height - Turn.unitCharacter.tile.floor.height) <= verticalRange;
    }
    public override List<TileLogic> GetTilesInRange()
    {
        return Board.instance.Search(Turn.unitCharacter.tile, SearchType);
    }

    public override string GetDirection()
    {
        return Turn.unitCharacter.direction;
    }
}
