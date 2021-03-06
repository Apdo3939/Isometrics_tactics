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
    [Header("Not in %")]
    public float baseDamageMultiplier = 1;
    public float randomness = 0.2f;
    public DamageType damageType;
    public ElementalType elementalType;
    public float gotHitDelay = 0.1f;

    public override int Predict(UnitCharacter target)
    {
        float attackerScore = 0;
        float defenderScore = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                attackerScore += Turn.unitCharacter.GetStat(StatEnum.ATK);
                defenderScore += target.GetStat(StatEnum.DEF);
                break;
            case DamageType.Magical:
                attackerScore += Turn.unitCharacter.GetStat(StatEnum.MATK);
                defenderScore += target.GetStat(StatEnum.MDEF);
                break;
        }

        float attackerFinal = GetBonus(Turn.unitCharacter, target, attackerScore);
        float defenderFinal = GetBonus(target, Turn.unitCharacter, defenderScore);

        float calculation = (attackerFinal - (defenderFinal / 2)) * baseDamageMultiplier;
        calculation = Mathf.Clamp(calculation, 0, 999);
        return (int)calculation;
    }

    public override void Apply(UnitCharacter target)
    {
        int damage = Predict(target);
        int currentHP = target.GetStat(StatEnum.HP);
        float roll = Random.Range(1 - randomness, 1 + randomness);
        int finalDamage = (int)(damage * roll);
        target.SetStat(StatEnum.HP, -finalDamage);

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

    float GetBonus(UnitCharacter thisUnit, UnitCharacter otherUnit, float initialScore)
    {
        if (thisUnit.stats.multiplicativeModifier == null)
        {
            return initialScore;
        }

        MultiplicativeForms forms = new MultiplicativeForms();
        forms.originalValue = (int)initialScore;
        forms.thisUnit = thisUnit;
        forms.otherUnit = otherUnit;
        forms.elementalType = elementalType;

        thisUnit.stats.multiplicativeModifier(forms);

        float bonus = forms.currentValue;
        float final = initialScore * (1 + (bonus / 100));

        Debug.LogFormat("Initial: {0} * bonus: {1}, Final = {2}", initialScore, bonus, final);

        return final;
    }
}
