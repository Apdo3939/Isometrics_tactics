using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModConditionJob : ModifierCondition
{
    public string jobName;
    public override bool Validate(object arg1)
    {
        MultiplicativeForms forms = (MultiplicativeForms)arg1;
        if (forms.otherUnit.job.name == jobName)
        {
            return true;
        }
        return false;
    }
}
