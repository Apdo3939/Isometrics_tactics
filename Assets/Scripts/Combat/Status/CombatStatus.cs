using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatStatus : MonoBehaviour
{
    public UnitCharacter unit;
    public int duration;
    public abstract void Effect();
    protected virtual void OnDisable()
    {
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach (Modifier m in modifiers)
        {
            m.Deactivate();
        }
    }
}
