using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatModifierHandler(Stat stat);

[System.Serializable]
public class Stat
{
    public StatEnum type;
    public int currentValue;
    public int baseValue;
    public float growth;
    public StatModifierHandler statModifiers;
}
