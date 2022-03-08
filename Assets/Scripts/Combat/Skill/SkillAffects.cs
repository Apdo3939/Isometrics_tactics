using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillAffectsType
{
    Default,
    AllyOnly,
    EnemyOnly,
}

public class SkillAffects : MonoBehaviour
{
    public SkillAffectsType skillAffectsType;

    public bool IsTarget(UnitCharacter unitCharacter)
    {
        switch (skillAffectsType)
        {
            case SkillAffectsType.AllyOnly:
                return IsAlly(unitCharacter);
            case SkillAffectsType.EnemyOnly:
                return IsEnemy(unitCharacter);
            default:
                return true;
        }

    }

    bool IsAlly(UnitCharacter unitCharacter)
    {
        if (unitCharacter.alliance == Turn.unitCharacter.alliance)
        {
            return true;
        }
        return false;
    }

    bool IsEnemy(UnitCharacter unitCharacter)
    {
        if (unitCharacter.alliance != Turn.unitCharacter.alliance)
        {
            return true;
        }
        return false;
    }
}
