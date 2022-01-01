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

    public bool TryToHit(UnitCharacter target)
    {
        float chance = Predict(target);
        float roll = Random.Range(0, 100 - baseBonusChance);
        chance += roll + baseBonusChance;
        if (chance >= 100)
            return true;
        return false;

    }

    public int Predict(UnitCharacter target)
    {
        hitScore = 0;
        missScore = 0;
        switch (type)
        {
            case HitRateType.Full:
                return 100;
            case HitRateType.Attack:
                hitScore = Turn.unitCharacter.GetStat(StatEnum.ACC);
                missScore = target.GetStat(StatEnum.EVD);
                break;
            case HitRateType.InflictStatus:
                hitScore = Turn.unitCharacter.GetStat(StatEnum.ACC);
                missScore = target.GetStat(StatEnum.RES);
                break;
        }
        float chance = (50 - (missScore - hitScore));
        return (int)chance;
    }

}
