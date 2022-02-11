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

    public static void LevelUp(UnitCharacter unit, int amount)
    {
        Stats toLevelStats = unit.stats;
        foreach (Stat s in toLevelStats.stats)
        {
            s.baseValue += Mathf.FloorToInt(s.growth * amount);
        }
        toLevelStats[StatEnum.LVL].baseValue += amount;
        toLevelStats[StatEnum.HP].baseValue = toLevelStats[StatEnum.MaxHP].baseValue;
        toLevelStats[StatEnum.MP].baseValue = toLevelStats[StatEnum.MaxMP].baseValue;

        unit.UpdateStat();
    }

    public static int GetExpCurveValue(int level)
    {
        int value = 0;
        for (int i = 1; i < level; i++)
        {
            value += i * 1000;
        }
        return value;
    }

    public static void CheckLevelUp(UnitCharacter unit)
    {
        int required = GetExpCurveValue(unit.stats[StatEnum.LVL].baseValue);
        if (unit.experience >= required)
        {
            LevelUp(unit, 1);
        }
    }
}
