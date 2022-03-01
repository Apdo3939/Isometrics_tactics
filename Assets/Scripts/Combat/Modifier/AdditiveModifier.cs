using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveModifier : Modifier
{
    protected override void Modify(object args)
    {
        Stat stat = (Stat)args;
        stat.currentValue += (int)value;
    }
    public override void Activate(UnitCharacter unity)
    {
        base.Activate(unity);
        host.stats[stat].additiveModifiers += Modify;
        host.UpdateStat(stat);
    }

    public override void Deactivate()
    {
        host.stats[stat].additiveModifiers -= Modify;
        host.UpdateStat(stat);
    }

}
