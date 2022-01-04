using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    public StatEnum stat;
    public float value;
    UnitCharacter host;

    public virtual void Activate(UnitCharacter unit)
    {
        host = unit;
        host.stats[stat].modifiers += Modify;
        host.UpdateStat(stat);
    }
    public virtual void Deactivate()
    {
        host.stats[stat].modifiers -= Modify;
        host.UpdateStat(stat);
    }

    protected abstract void Modify(Stat stat);
}
