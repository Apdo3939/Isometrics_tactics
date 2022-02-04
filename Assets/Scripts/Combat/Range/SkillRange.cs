using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillRange : MonoBehaviour
{
    public int range;
    public int verticalRange;

    public virtual bool IsDirectionOriented()
    {
        return false;
    }

    public virtual string GetDirection()
    {
        if (Turn.targets[0] == Turn.unitCharacter.tile)
        {
            return Turn.unitCharacter.direction;
        }
        return Turn.unitCharacter.tile.GetDirection(Turn.targets[0]);
    }

    public abstract List<TileLogic> GetTilesInRange();
}
