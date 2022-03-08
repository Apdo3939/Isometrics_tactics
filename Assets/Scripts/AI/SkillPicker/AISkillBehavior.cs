using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISkillBehavior : MonoBehaviour
{
    int index;
    public List<SkillPicker> pickers;

    void Awake()
    {
        pickers = new List<SkillPicker>();
        pickers.AddRange(
            GetComponent<UnitCharacter>().job.AISkillPicks.
            GetComponents<SkillPicker>()
        );
    }

    public void Pick(AIPlan plan)
    {
        pickers[index].Pick(plan);
        index++;

        if (index >= pickers.Count)
        {
            index = 0;
        }
    }
}
