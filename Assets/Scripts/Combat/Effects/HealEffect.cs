using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : SkillEffect
{
    [Header("Not in %")]
    public float baseDamageMultiplier = 1;
    public float randomness = 0.2f;
    public float gotHitDelay = 0.1f;

    public override void Apply(UnitCharacter target)
    {
        int initial = Predict(target);
        int currentHP = target.GetStat(StatEnum.HP);
        float roll = Random.Range(1 - randomness, 1 + randomness);
        int finalHeal = (int)(initial * roll);
        int heal = Mathf.Clamp(finalHeal, 0, target.GetStat(StatEnum.MaxHP) - currentHP);
        target.SetStat(StatEnum.HP, heal);
    }

    public override int Predict(UnitCharacter target)
    {
        float attackerScore = 0;
        attackerScore += Turn.unitCharacter.GetStat(StatEnum.MATK) * baseDamageMultiplier;

        return (int)attackerScore;
    }
}
