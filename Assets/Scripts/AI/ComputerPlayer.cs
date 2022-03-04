using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    public static ComputerPlayer instance;
    UnitCharacter currentUnit { get { return Turn.unitCharacter; } }
    int alliance { get { return currentUnit.alliance; } }
    UnitCharacter nearestFoe;
    public AIPlan currentPlan;

    void Awake()
    {
        instance = this;
    }

    public AIPlan Evaluate()
    {
        AIPlan plan = new AIPlan();
        plan.movePos = currentUnit.tile.pos;

        currentPlan = plan;
        return plan;
    }
}
