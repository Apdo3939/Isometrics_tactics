using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSkillRange : SkillRange
{
    public override List<TileLogic> GetTilesInRange()
    {
        List<TileLogic> retValue = new List<TileLogic>();
        retValue.Add(Turn.unitCharacter.tile);
        return retValue;
    }

    public override string GetDirection()
    {
        return Turn.unitCharacter.direction;
    }
}
