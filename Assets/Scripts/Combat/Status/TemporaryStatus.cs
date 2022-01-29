using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryStatus : CombatStatus
{
    public override void Effect()
    {
        unit.onTurnBegin += DurationCounter;
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach (Modifier m in modifiers)
        {
            m.Activate(unit);
        }
    }

    void DurationCounter()
    {
        duration--;
        if (duration <= 0)
        {
            if (this.gameObject == null)
            {
                Destroy(this.gameObject);
            }

        }
    }
}
