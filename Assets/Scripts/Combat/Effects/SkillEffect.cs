using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : MonoBehaviour
{
    public abstract int Predict(UnitCharacter target);
    public abstract void Apply(UnitCharacter target);
}
