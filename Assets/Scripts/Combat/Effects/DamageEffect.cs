using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Magical,
}
public class DamageEffect : SkillEffect
{
    public DamageType damageType;
    [Header("Not in %")]
    public float baseDamageMultiplier = 1;
    public float randomness = 0.2f;
    public float gotHitDelay = 0.1f;

    public override int Predict(UnitCharacter target)
    {
        float attackerScore = 0;
        float defenderScore = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                attackerScore += Turn.unitCharacter.GetStat(StatEnum.ATK);
                attackerScore += target.GetStat(StatEnum.DEF);
                break;
            case DamageType.Magical:
                attackerScore += Turn.unitCharacter.GetStat(StatEnum.MATK);
                attackerScore += target.GetStat(StatEnum.MDEF);
                break;
        }
        float calculation = (attackerScore - (defenderScore / 2)) * baseDamageMultiplier;
        calculation = Mathf.Clamp(calculation, 0, 999);
        return (int)calculation;
    }

    public override void Apply(UnitCharacter target)
    {
        int damage = Predict(target);
        int currentHP = target.GetStat(StatEnum.HP);
        float roll = Random.Range(1 - randomness, 1 + randomness);
        int finalDamage = (int)(damage * roll);
        target.SetStat(StatEnum.HP, -Mathf.Clamp(finalDamage, 0, currentHP));

        /*if(!isPrimary)
            return;*/

        if (target.GetStat(StatEnum.HP) <= 0)
        {
            target.animationController.Death(gotHitDelay);
        }
        else
        {
            target.animationController.Idle();
            target.animationController.GotHit(gotHitDelay);
        }

    }
}
