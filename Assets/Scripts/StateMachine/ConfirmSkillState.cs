using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSkillState : State
{
    public override void Enter()
    {
        base.Enter();
        inputs.OnFire += OnFire;
        Turn.targets = Turn.skill.GetArea();
        board.SelectTiles(Turn.targets, Turn.unitCharacter.alliance);
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnFire -= OnFire;
        board.DeSelectTiles(Turn.targets);
    }

    public void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            if (Turn.skill.ValidateTarget(Turn.targets))
            {
                machine.ChangeTo<PerformSkillState>();
            }
            else
            {
                Debug.Log("No unit");
            }
        }
        else if (button == 2)
        {
            machine.ChangeTo<SkillTargetState>();
        }
    }
}
