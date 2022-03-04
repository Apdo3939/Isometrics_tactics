using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseActionState : State
{
    public override void Enter()
    {
        MoveSelector(Turn.unitCharacter.tile);
        base.Enter();

        if (Turn.unitCharacter.playerType == PlayerType.Human)
        {
            index = 0;
            inputs.OnMove += OnMove;
            inputs.OnFire += OnFire;
            machine.chooseActionPanel.MoveTo("Show");
            currentUISelector = machine.chooseActionSelector;
            ChangeUISelector(machine.chooseActionButons);
            CheckActions();
        }
        else
        {
            Debug.Log("Computer playing!!!");
            StartCoroutine(ComputerChooseAction());
        }

    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMove;
        inputs.OnFire -= OnFire;
        machine.chooseActionPanel.MoveTo("Hide");
    }

    void OnMove(object sender, object args)
    {
        Vector3Int button = (Vector3Int)args;
        if (button == Vector3Int.left || button == Vector3Int.down)
        {
            index--;
            ChangeUISelector(machine.chooseActionButons);
        }
        else if (button == Vector3Int.right || button == Vector3Int.up)
        {
            index++;
            ChangeUISelector(machine.chooseActionButons);
        }
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            Debug.Log("button 1 --- ChooseActionState");
            ActionsButton();
        }
        else if (button == 2)
        {
            Debug.Log("button 2 --- ChooseActionState");
            machine.ChangeTo<RoamState>();
        }
    }

    void CheckActions()
    {
        PaintButton(machine.chooseActionButons[0], Turn.hasMoved);
        PaintButton(machine.chooseActionButons[1], Turn.hasActed);
        PaintButton(machine.chooseActionButons[2], Turn.hasActed);
    }

    void PaintButton(Image image, bool check)
    {
        if (check) { image.color = Color.gray; }
        else { image.color = Color.white; }
    }

    void ActionsButton()
    {
        switch (index)
        {
            case 0:
                if (!Turn.hasMoved) { machine.ChangeTo<MoveSelectionState>(); }
                break;
            case 1:
                if (!Turn.hasActed) { machine.ChangeTo<SkillSelectionState>(); }
                break;
            case 2:
                machine.ChangeTo<ItemSelectState>();
                break;
            case 3:
                machine.ChangeTo<StatusState>();
                break;
            case 4:
                Debug.Log("WaitState");
                machine.ChangeTo<TurnEndState>();
                break;
            default:
                break;
        }

    }

    //AI codes below

    IEnumerator ComputerChooseAction()
    {
        AIPlan plan = ComputerPlayer.instance.currentPlan;
        if (plan == null)
        {
            plan = ComputerPlayer.instance.Evaluate();
            Turn.skill = plan.skill;
        }

        yield return new WaitForSeconds(1f);

        if (Turn.hasMoved == false && plan.movePos != Turn.unitCharacter.tile.pos)
        {
            machine.ChangeTo<MoveSelectionState>();
        }
        else if (Turn.hasActed == false && Turn.skill != null)
        {
            machine.ChangeTo<SkillTargetState>();
        }
        else
        {
            machine.ChangeTo<TurnEndState>();
        }
    }
}
