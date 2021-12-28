using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State
{
    List<TileLogic> selectedTiles;
    public override void Enter()
    {
        base.Enter();
        if (Turn.skill.GetComponentInChildren<SkillRange>().directionOriented)
        { inputs.OnMove += ChangeDirection; }
        else { inputs.OnMove += OnMoveTileSelector; }
        inputs.OnFire += OnFire;

        selectedTiles = Turn.skill.GetTargets();
        board.SelectTiles(selectedTiles, Turn.unitCharacter.alliance);
    }

    public override void Exit()
    {
        base.Exit();
        if (Turn.skill.GetComponentInChildren<SkillRange>().directionOriented)
        { inputs.OnMove -= ChangeDirection; }
        else { inputs.OnMove -= OnMoveTileSelector; }
        inputs.OnFire -= OnFire;

        board.DeSelectTiles(selectedTiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            if (Turn.skill.ValidateTarget(selectedTiles))
            {
                Turn.targets = selectedTiles;
                machine.ChangeTo<PerformSkillState>();
            }
            else
            {
                Debug.Log("No unit");
            }
        }
        if (button == 2) { machine.ChangeTo<SkillSelectionState>(); }
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
}
