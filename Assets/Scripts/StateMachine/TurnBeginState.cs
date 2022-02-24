using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBeginState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(SelectUnity());
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator SelectUnity()
    {
        BreakDawn();
        machine.units.Sort((x, y) => x.chargeTime.CompareTo(y.chargeTime));
        Turn.unitCharacter = machine.units[0];

        yield return null;
        if (Turn.unitCharacter.onTurnBegin != null)
        {
            Turn.unitCharacter.onTurnBegin();
        }

        yield return null;
        if (Turn.unitCharacter.GetStat(StatEnum.HP) <= 0)
        {
            if (Turn.unitCharacter.active)
            {
                Turn.unitCharacter.animationController.Death();
            }

            Turn.unitCharacter.active = false;
            machine.ChangeTo<TurnEndState>();
        }
        else
        {
            if (Job.CanAdvance(Turn.unitCharacter))
            {
                machine.ChangeTo<JobAdavanceState>();
            }
            else
            {
                machine.ChangeTo<ChooseActionState>();
            }
        }

    }

    void BreakDawn()
    {
        for (int i = 0; i < machine.units.Count - 1; i++)
        {
            if (machine.units[i].chargeTime == machine.units[i + 1].chargeTime)
            { machine.units[i + 1].chargeTime += 1; }
        }
    }
}
