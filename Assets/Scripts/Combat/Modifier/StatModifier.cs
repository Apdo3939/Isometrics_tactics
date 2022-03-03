using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : Modifier
{
    public StatEnum stat;
    protected override void Modify(object args)
    {
        Stat stat = (Stat)args;
        stat.currentValue += (int)value;
    }
    public override void Activate(UnitCharacter unity)
    {
        base.Activate(unity);
        host.stats[stat].statModifiers += Modify;
        host.UpdateStat(stat);
    }

    public override void Deactivate()
    {
        host.stats[stat].statModifiers -= Modify;
        host.UpdateStat(stat);
    }

}
