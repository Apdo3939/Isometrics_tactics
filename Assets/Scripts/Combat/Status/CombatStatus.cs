using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStatus : MonoBehaviour
{
    public UnitCharacter unit;

    [ContextMenu("Activate all")]
    void ActivateAll()
    {
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach (Modifier m in modifiers)
        {
            m.Activate(unit);
        }
    }

    [ContextMenu("Deactivate all")]
    void DeactivateAll()
    {
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach (Modifier m in modifiers)
        {
            m.Deactivate();
        }
    }
}
