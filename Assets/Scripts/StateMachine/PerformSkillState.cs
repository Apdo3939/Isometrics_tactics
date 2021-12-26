using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformSkillState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PerformSequence());
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator PerformSequence()
    {
        yield return null;
        Turn.targets = Turn.skill.GetTargets();

        yield return null;
        Turn.unitCharacter.direction = Turn.unitCharacter.tile.GetDirection(machine.selectedTile);
        Turn.unitCharacter.animationController.Idle();
        Turn.unitCharacter.animationController.Attack();
        Turn.skill.Effect();

        yield return null;
        CombatLog.CheckActive();

        yield return new WaitForSeconds(1.5f);
        if (CombatLog.IsOver())
        {
            Debug.Log("Game Over");
        }
        else
        {
            machine.ChangeTo<TurnEndState>();
        }
    }
}
