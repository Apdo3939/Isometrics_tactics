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
        AISkillBehavior aISkillBehavior = Turn.unitCharacter.GetComponent<AISkillBehavior>();
        if (aISkillBehavior == null)
        {
            aISkillBehavior = Turn.unitCharacter.gameObject.AddComponent<AISkillBehavior>();
        }

        MoveTowardOpponent(plan);

        currentPlan = plan;
        return plan;
    }

    void FindNearestFoe()
    {
        nearestFoe = null;
        Board.instance.Search(Turn.unitCharacter.tile, delegate (TileLogic arg1, TileLogic arg2)
        {
            if (nearestFoe == null && arg2.content != null)
            {

                UnitCharacter unit = arg2.content.GetComponent<UnitCharacter>();
                if (unit != null && currentUnit.alliance != unit.alliance)
                {
                    Stats stats = unit.stats;
                    if (stats[StatEnum.HP].currentValue > 0)
                    {
                        nearestFoe = unit;
                        return true;
                    }
                }
            }
            arg2.distance = arg1.distance + 1;
            return nearestFoe == null;
        });
    }

    List<TileLogic> GetMoveOptions()
    {
        return Board.instance.Search(Turn.unitCharacter.tile,
        Turn.unitCharacter.GetComponent<Movement>().ValidateMovement);
    }

    void MoveTowardOpponent(AIPlan plan)
    {

        List<TileLogic> moveOptions = GetMoveOptions();
        FindNearestFoe();

        if (nearestFoe != null)
        {

            TileLogic toCheck = nearestFoe.tile;

            while (toCheck != null)
            {
                if (moveOptions.Contains(toCheck))
                {
                    plan.movePos = toCheck.pos;
                    return;
                }
                toCheck = toCheck.prev;
            }
        }

        plan.movePos = Turn.unitCharacter.tile.pos;
    }
}
