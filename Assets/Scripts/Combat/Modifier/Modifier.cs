using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    public StatEnum stat;
    public float value;
    protected UnitCharacter host;

    public virtual void Activate(UnitCharacter unit)
    {
        host = unit;
    }

    public abstract void Deactivate();

    protected abstract void Modify(object args);
}
