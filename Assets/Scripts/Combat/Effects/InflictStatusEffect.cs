using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusEffect : SkillEffect
{
    public GameObject status;
    public int duration;

    public override void Apply(UnitCharacter target)
    {
        Transform holder = target.transform.Find("Status");
        GameObject temp = Instantiate(status, holder.position, Quaternion.identity, holder);
        temp.name = temp.name.Replace("(Clone)", "");
    }

    public override int Predict(UnitCharacter target)
    {
        return 0;
    }
}
