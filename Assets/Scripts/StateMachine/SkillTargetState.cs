using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State
{
    List<TileLogic> selectedTiles;
    bool directionOriented;

    public override void Enter()
    {
        base.Enter();

        selectedTiles = Turn.skill.GetTargets();
        board.SelectTiles(selectedTiles, Turn.unitCharacter.alliance);

        directionOriented = Turn.skill.GetComponentInChildren<SkillRange>().IsDirectionOriented();
        if (Turn.unitCharacter.playerType == PlayerType.Human)
        {
            if (directionOriented)
            {
                inputs.OnMove += ChangeDirection;
            }
            else
            {
                inputs.OnMove += OnMoveTileSelector;
            }
            inputs.OnFire += OnFire;
        }
        else
        {
            StartCoroutine(ComputerTargeting());
        }

    }

    public override void Exit()
    {
        base.Exit();

        if (directionOriented)
        {
            inputs.OnMove -= ChangeDirection;
        }
        else
        {
            inputs.OnMove -= OnMoveTileSelector;
        }

        inputs.OnFire -= OnFire;

        board.DeSelectTiles(selectedTiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1 && (directionOriented || selectedTiles.Contains(Selector.instance.tile)))
        {
            Turn.targets = selectedTiles;
            machine.ChangeTo<ConfirmSkillState>();
        }
        else if (button == 2)
        {
            if (Turn.isItem != null)
            {
                machine.ChangeTo<ItemSelectState>();
            }
            else
            {
                machine.ChangeTo<SkillSelectionState>();
            }

        }
    }

    void ChangeDirection(object sender, object args)
    {
        Vector3Int pos = (Vector3Int)args;

        string dir = Turn.unitCharacter.tile.GetDirection(Turn.unitCharacter.tile.pos + pos);

        if (Turn.unitCharacter.direction != dir)
        {
            board.DeSelectTiles(selectedTiles);
            Turn.unitCharacter.direction = dir;
            Turn.unitCharacter.GetComponent<AnimationController>().Idle();
            selectedTiles = Turn.skill.GetTargets();
            board.SelectTiles(selectedTiles, Turn.unitCharacter.alliance);
        }
    }

    IEnumerator ComputerTargeting()
    {
        SkillRange sr = Turn.skill.GetComponentInChildren<SkillRange>();
        AIPlan plan = ComputerPlayer.instance.currentPlan;

        if (sr.IsDirectionOriented())
        {
            switch (plan.direction)
            {
                case "North":
                    ChangeDirection(null, Vector3Int.up);
                    break;
                case "South":
                    ChangeDirection(null, Vector3Int.down);
                    break;
                case "East":
                    ChangeDirection(null, Vector3Int.right);
                    break;
                default:
                    ChangeDirection(null, Vector3Int.left);
                    break;
            }
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            //Selector.instance.tile = Turn.unitCharacter.tile;
            yield return StartCoroutine(AIMoveSelector(plan.skillTargetPos));
        }
        yield return new WaitForSeconds(0.5f);
        Turn.targets = selectedTiles;
        machine.ChangeTo<ConfirmSkillState>();
    }
}
