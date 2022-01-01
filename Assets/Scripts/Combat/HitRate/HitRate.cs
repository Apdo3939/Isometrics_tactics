using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitRateType
{
    Attack,
    InflictStatus,
    Full
}

public class HitRate : MonoBehaviour
{
    public HitRateType type;
    [Header("In %")]
    public float baseBonusChance;
    public float hitScore;
    public float missScore;

    public bool TryToHit(UnitCharacter unit)
    {
        hitScore = 0;
        missScore = 0;
        switch (type)
        {
            case HitRateType.Full:
                return true;
            case HitRateType.Attack:
                hitScore = Turn.unitCharacter.GetStat(StatEnum.ACC);
                missScore = Turn.unitCharacter.GetStat(StatEnum.EVD);
                break;
            case HitRateType.InflictStatus:
                hitScore = Turn.unitCharacter.GetStat(StatEnum.ACC);
                missScore = Turn.unitCharacter.GetStat(StatEnum.RES);
                break;
        }
        float chance = (50 - (missScore - hitScore));
        float roll = Random.Range(0, 100 - baseBonusChance);
        chance += roll + baseBonusChance;
        if (chance >= 100)
            return true;
        return false;

    }

}
