using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : Item
{
    public override void Use(UnitCharacter unit)
    {
        Debug.Log("Consumido!");
    }
}
