using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : Item
{
    public override void Use(UnitCharacter unit)
    {
        foreach (Modifier m in GetComponents<Modifier>())
        {
            m.Activate(unit);
        }
    }
}
