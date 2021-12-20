using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(MovementSequence());
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator MovementSequence()
    {
        List<TileLogic> path = CreatePath();

        Movement movement = Turn.unitCharacter.GetComponent<Movement>();
        yield return StartCoroutine(movement.Move(path));
        Turn.unitCharacter.tile.content = null;
        Turn.unitCharacter.tile = machine.selectedTile;
        Turn.unitCharacter.tile.content = Turn.unitCharacter.gameObject;
        yield return new WaitForSeconds(0.5f);
        Turn.hasMoved = true;
        machine.ChangeTo<ChooseActionState>();
    }

    List<TileLogic> CreatePath()
    {
        List<TileLogic> path = new List<TileLogic>();
        TileLogic t = machine.selectedTile;
        while (t != Turn.unitCharacter.tile)
        {
            path.Add(t);
            t = t.prev;
        }
        path.Reverse();
        return path;
    }
}
