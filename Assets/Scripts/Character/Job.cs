using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ScriptableObject
{
    public List<Stat> stats;
    public List<Skill> skills;
    public string spriteModel;

    [ContextMenu("Init Stats")]
    public void InitStats()
    {
        stats = new List<Stat>();
        for (int i = 0; i <= (int)StatEnum.MOV; i++)
        {
            Stat temp = new Stat();
            temp.type = (StatEnum)i;
            stats.Add(temp);
        }
    }
}
