using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndState : State
{
    public override void Enter()
    {
        base.Enter();
        CombatLog.CheckActive();
        if (CombatLog.IsOver())
        {
            Debug.Log("Game Over");
            return;
        }

        StartCoroutine(AddUnitDelay());
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator AddUnitDelay()
    {
        Turn.unitCharacter.chargeTime += 300;
        if (Turn.hasMoved) { Turn.unitCharacter.chargeTime += 100; }
        if (Turn.hasActed) { Turn.unitCharacter.chargeTime += 100; }

        Turn.unitCharacter.chargeTime -= Turn.unitCharacter.GetStat(StatEnum.SPEED);

        Turn.hasActed = Turn.hasMoved = false;

        //New code for AI
        Turn.skill = null;
        Turn.isItem = null;
        ComputerPlayer.instance.currentPlan = null;
        //End code for AI

        machine.units.Remove(Turn.unitCharacter);
        machine.units.Add(Turn.unitCharacter);
        yield return new WaitForSeconds(0.5f);

        machine.ChangeTo<TurnBeginState>();
    }


}
