using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MultiplicativeType
{
    Attacker,
    Defender,
}
public class MultiplicativeModifier : Modifier
{
    public MultiplicativeType type;
    public ModifierCondition condition;

    protected override void Modify(object args)
    {
        MultiplicativeForms forms = (MultiplicativeForms)args;
        if (TypeCheck() && (condition == null || condition.Validate(forms)))
        {
            forms.currentValue = +(int)value;
        }
    }

    public override void Activate(UnitCharacter unit)
    {
        base.Activate(unit);
        host.stats.multiplicativeModifier += Modify;
    }

    public override void Deactivate()
    {
        host.stats.multiplicativeModifier -= Modify;
    }

    bool TypeCheck()
    {
        if (type == MultiplicativeType.Attacker)
        {
            if (Turn.unitCharacter == host)
            {
                return true;
            }
        }
        else
        {
            if (Turn.targets.Contains(host.tile))
            {
                return true;
            }
        }
        return false;
    }
}
