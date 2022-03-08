using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ScriptableObject
{
    public List<Stat> stats;
    public List<Skill> skills;
    public string spriteModel;
    public int advanceAtLevel;
    public List<Job> advancesTo;
    public Sprite portrait;
    [TextArea]
    public string description;
    public GameObject AISkillPicks;

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

    public static bool CanAdvance(UnitCharacter unit)
    {
        if (unit.GetStat(StatEnum.LVL) >= unit.job.advanceAtLevel)
        {
            return true;
        }
        return false;
    }

    public static void Employ(UnitCharacter unit, Job job, int level)
    {
        unit.job = job;
        unit.spriteModel = job.spriteModel;
        SetStats(unit.stats, job);
        unit.UpdateStat();

        Job.LevelUp(unit, level - 1);

        Skillbook skillbook = unit.GetComponentInChildren<Skillbook>();
        skillbook.skills = new List<Skill>();
        skillbook.skills.AddRange(job.skills);
    }

    static void SetStats(Stats stats, Job job)
    {
        stats.stats = new List<Stat>();

        for (int i = 0; i < job.stats.Count; i++)
        {
            Stat stat = new Stat();
            stat.baseValue = job.stats[i].baseValue;
            stat.currentValue = job.stats[i].currentValue;
            stat.growth = job.stats[i].growth;
            stat.type = job.stats[i].type;
            stats.stats.Add(stat);
        }

        stats.stats[(int)StatEnum.HP].baseValue = stats.stats[(int)StatEnum.MaxHP].baseValue;
        stats.stats[(int)StatEnum.MP].baseValue = stats.stats[(int)StatEnum.MaxMP].baseValue;
    }
}
