using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusEffect : SkillEffect
{
    public CombatStatus status;
    public int duration;

    public override void Apply(UnitCharacter target)
    {
        Transform holder = target.transform.Find("Status");
        CombatStatus instantiatedStatus = Instantiate(status, holder.position, Quaternion.identity, holder);
        instantiatedStatus.name = instantiatedStatus.name.Replace("(Clone)", "");
        instantiatedStatus.unit = target;
        instantiatedStatus.duration = duration;
        instantiatedStatus.Effect();
    }

    public override int Predict(UnitCharacter target)
    {
        return 0;
    }
}
