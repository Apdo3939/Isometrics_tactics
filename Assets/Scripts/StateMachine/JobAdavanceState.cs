using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobAdavanceState : State
{
    public override void Enter()
    {
        machine.panelAdvanceJob.Show();
        inputs.OnMove += OnMove;
        inputs.OnFire += OnFire;
    }

    public override void Exit()
    {
        machine.panelAdvanceJob.Hide();
        inputs.OnMove -= OnMove;
        inputs.OnFire -= OnFire;
    }

    void OnMove(object sender, object args)
    {
        Vector3Int button = (Vector3Int)args;
        if (button == Vector3Int.left)
        {
            machine.panelAdvanceJob.SelectPrevious();
        }
        else if (button == Vector3Int.right)
        {
            machine.panelAdvanceJob.SelectNext();
        }
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            machine.panelAdvanceJob.JobChange();
            machine.ChangeTo<ChooseActionState>();
        }
    }
}
