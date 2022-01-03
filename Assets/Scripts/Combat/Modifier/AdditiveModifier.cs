using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveModifier : Modifier
{
    public override void Activate(UnitCharacter unity)
    {
        base.Activate(unity);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void Modify(Stat stat)
    {
        stat.currentValue += (int)value;
    }
}
