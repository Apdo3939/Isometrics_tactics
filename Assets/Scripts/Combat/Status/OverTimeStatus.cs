using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeStatus : CombatStatus
{
    public override void Effect()
    {
        unit.onTurnBegin += OverTimeEffect;
    }

    protected override void OnDisable()
    {
        unit.onTurnBegin -= OverTimeEffect;
    }

    void OverTimeEffect()
    {
        duration--;
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach (Modifier m in modifiers)
        {
            unit.SetStat(m.stat, (int)m.value);
        }

        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
