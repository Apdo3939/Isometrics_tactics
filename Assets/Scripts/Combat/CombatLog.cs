using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatLog
{
    public static void CheckActive()
    {
        foreach (UnitCharacter uc in StateMachineController.instance.units)
        {
            if (uc.GetStat(StatEnum.HP) <= 0) { uc.active = false; }
            else { uc.active = true; }
        }
    }

    public static bool IsOver()
    {
        int activeAlliances = 0;
        for (int i = 0; i < MapLoader.instance.alliances.Count; i++)
        {
            activeAlliances += CheckAlliance(MapLoader.instance.alliances[i]);
        }
        if (activeAlliances > 1) { return false; }
        return true;
    }

    public static int CheckAlliance(Alliance alliance)
    {
        for (int i = 0; i < alliance.units.Count; i++)
        {
            UnitCharacter uc = alliance.units[i];
            if (uc.active)
            {
                return 1;
            }
        }
        return 0;
    }
}
