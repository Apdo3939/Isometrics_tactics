using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : SkillEffect
{
    public List<string> statusNames;

    public override int Predict(UnitCharacter target)
    {
        return 0;
    }

    public override void Apply(UnitCharacter target)
    {
        Transform holder = target.transform.Find("Status");
        foreach (string s in statusNames)
        {
            SeekAndDestroy(s, holder);
        }
    }

    void SeekAndDestroy(string statusName, Transform holder)
    {
        Transform status = holder.Find(statusName);
        if (status != null)
        {
            Destroy(status.gameObject);
        }
    }
}
