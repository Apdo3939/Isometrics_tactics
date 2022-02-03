using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusEffect : SkillEffect
{
    public CombatStatus status;
    public string statusName;
    public int statusValue;
    public int duration;

    public override void Apply(UnitCharacter target)
    {
        Transform holder = target.transform.Find("Status");
        Transform stack = holder.Find(statusName);
        if (stack != null)
        {
            Stack(stack);
        }
        else
        {
            CreateNew(holder, target);
        }

    }

    void CreateNew(Transform holder, UnitCharacter target)
    {
        CombatStatus instantiatedStatus = Instantiate(status, holder.position, Quaternion.identity, holder);
        instantiatedStatus.name = statusName;
        instantiatedStatus.SetModifiersValue(statusValue);
        instantiatedStatus.unit = target;
        instantiatedStatus.duration = duration;
        instantiatedStatus.Effect();

    }

    void Stack(Transform stack)
    {
        CombatStatus stackStatus = stack.GetComponent<CombatStatus>();
        stackStatus.Stack(duration, statusValue);
    }

    public override int Predict(UnitCharacter target)
    {
        return 0;
    }
}
